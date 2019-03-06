using Automated.Utilities.Utilities;
using System.Configuration;
using System;
using Automated.Utilities.Utilities.Parsers;
using NUnit.Framework;

namespace TAMM.Automation.Test
{
    class TestConfigs
    {   
        //Application Settings
        public static string Url;
        public static string Browser;
        public static string BrokenLinksURLs;

        //Automation Configs
        public static string PathOfCurrentContext;
        public static string AutomationDirectory = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
        
        //Files
        public static string TestDataFile;

        //Logs
        public static string LogFile;
        
        //Maximum number of retries if the test failed
        public const int MaxNumberOfRetries = 1;
        public static bool IsTestConfigsInitialized = false;

        //Reporting Directory
        public static string ReportingDirectory;

        public static string SetCurrentContextPath(string nameOfTestingDirectory)
        {
            string currentPath = TestContext.CurrentContext.WorkDirectory.Replace("bin\\Debug", "");
            currentPath = currentPath.Replace(nameOfTestingDirectory, "?");
            currentPath = currentPath.Remove(currentPath.LastIndexOf('?'));
            PathOfCurrentContext = currentPath;
            return currentPath;
        }

        /// <summary>
        /// Read the configurations of the application under test
        /// </summary>
        public void ReadApplicationConfigs()
        {
        }//end method ReadApplicationConfigs

        /// <summary>
        /// Read the provided test configurations
        /// </summary>
        public static void ReadConfigs(string pathOfAutomationConfigs)
        {
            //Application Settings: load the Automation Directory Configs
            TextDataParser.LoadConfigurationValues(pathOfAutomationConfigs);

            AutomationDirectory = TextDataParser.LoadAndGetValue("AutomationDirectory", pathOfAutomationConfigs + ".dir");
            AutomationDirectory = TextDataParser.GetValue("AutomationDirectory");
            Url = TextDataParser.GetValue("Url");
            Browser = TextDataParser.GetValue("Browser");
            //Files: Test Data, Messages, ...
            TestDataFile = AutomationDirectory + TextDataParser.GetValue("TestDataFile");
            LogFile = AutomationDirectory + TextDataParser.GetValue("LogFile");
            ReportingDirectory = AutomationDirectory + TextDataParser.GetValue("ReportingDirectory");
           
        }//end method ReadConfigs

        /// <summary>
        /// Initialize the test configurations
        /// </summary>
        public static void Init()
        {
            if (!IsTestConfigsInitialized)
            {
                string automationConf = PathOfCurrentContext + "automation.conf";

                //Read the automated app configs
                ReadConfigs(automationConf);

                AutomatedLogger.Init(LogFile);
                AutomatedLogger.Log("Test that logging is working!");

                AutomatedLogger.Log("WebPath: " + automationConf);

                //Initialize your configs: messages, test data, logger, ...
                ExcelDataParser.Init(TestConfigs.TestDataFile);
                //TODO: this should be json data object
                //data = JsonDataParser.ParseJsonData(TestConfigs.TestDataFile);

                //Initialize the report here
                //TestReport.Initialize(TestConfigs.ReportingDirectory);

                IsTestConfigsInitialized = true;

            }//endif

        }//end method


        /// <summary>
        /// Check if you are testing remotely or on a localhost
        /// </summary>
        /// <returns></returns>
        public static bool IsRemoteTesting()
        {
            bool isRemote = !Url.Contains("localhost");
            return isRemote;
        }

    }
}
