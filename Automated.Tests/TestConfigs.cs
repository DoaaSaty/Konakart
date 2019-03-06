using Automated.Utilities.Reporting;
using Automated.Utilities.Utilities;
using Automated.Utilities.Utilities.Parsers;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Linq;

namespace Automated.Tests
{
   public  class TestConfigs
    {

        //Application Settings
        public static string Url;
        public static string Browser;
        public static string PathOfCurrentContext;

        //Automation Root Directory
        public static string AutomationDirectory;

        //Files
        public static string TestDataFile;

        //Logs
        public static string LogFile;
        
        //Maximum number of retries if the test failed
       public const int MaxNumberOfRetries = 1;

        // UserID and Password
        public static string userID;
        public static string password;


        // Reporting Path URL
        public static string ReportUrl = ConfigurationManager.AppSettings["ReportUrl"];
        public static string ReportName = ConfigurationManager.AppSettings["ReportName"];


        // Initialize long and short time
        public static int LongTime;
        public static int ShortTime;
        public static int MedTime;

        //Reporting Directory
        public static string ReportingDirectory;

        public static JObject data;

        // filePath
        public static string filePath;

        public static bool IsTestConfigsInitialized = false;

        /// <summary>
        /// Read the provided test configurations
        /// </summary>
        public static void ReadConfigs()
        {
            //Application Settings
            AutomationDirectory = ConfigurationManager.AppSettings["AutomationDirectory"];

            Url = ConfigurationManager.AppSettings["Url"];
            Browser = ConfigurationManager.AppSettings["Browser"];


            //Files: Test Data, Messages, ...
            TestDataFile = AutomationDirectory +  ConfigurationManager.AppSettings["TestDataFile"];
            LogFile = AutomationDirectory + @"Logs\AutomatedTests.log";
 
        

            // short and long time
            ShortTime = int.Parse(ConfigurationManager.AppSettings["ShortTime"]);
            LongTime = int.Parse(ConfigurationManager.AppSettings["LongTime"]);
            MedTime = int.Parse(ConfigurationManager.AppSettings["MedTime"]);
            // MaxNumberOfRetries

            // FilePath of import feature
            filePath = ConfigurationManager.AppSettings["FilePath"];

        }//end method ReadConfigs

        /// <summary>
        /// Initialize the test configurations
        /// </summary>
        public static void Init()
        {
            if (!IsTestConfigsInitialized)
            {
                //Read the automated app configs
                ReadConfigs();

                //Initialize your configs: messages, test data, logger, ...
                ExcelDataParser.Init(TestConfigs.TestDataFile);
                AutomatedLogger.Init(LogFile);

                //Initialize the report here
                TestReport.Report_init(TestConfigs.ReportingDirectory);

                IsTestConfigsInitialized = true;

            }//endif

        }//end method


        /// <summary>
        /// Read the provided test configurations
        /// </summary>
        public static void ReadConfigs(string pathOfAutomationConfigs)
        {
            //Application Settings: load the Automation Directory Configs
            TextDataParser.LoadConfigurationValues(pathOfAutomationConfigs);

            //TODO: the below line is used to when the batch file is running.
            AutomationDirectory = TextDataParser.LoadAndGetValue("AutomationDirectory", pathOfAutomationConfigs + ".dir");
            AutomationDirectory = TextDataParser.GetValue("AutomationDirectory");
            Url = TextDataParser.GetValue("Url");
            Browser = TextDataParser.GetValue("Browser");
            //Files: Test Data, Messages, ...
            TestDataFile = AutomationDirectory + TextDataParser.GetValue("TestDataFile");
            LogFile = AutomationDirectory + TextDataParser.GetValue("LogFile");
            ReportingDirectory = AutomationDirectory + TextDataParser.GetValue("ReportingDirectory");
            userID = TextDataParser.GetValue("UserID");
            password = TextDataParser.GetValue("Password");
            //TODO: Add the report path here ya Asmaa

        }//end method ReadConfigs

        /// <summary>
        /// Initialize the test configurations
        /// </summary>
        public static void Init(string automationConf)
        {
            if (!IsTestConfigsInitialized)
            {
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
                TestReport.Report_init(TestConfigs.ReportingDirectory);

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
