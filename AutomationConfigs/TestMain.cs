using Automated.Utilities.AutomationAbstractions.Components;
using Automated.Utilities.Utilities;
using NUnit.Framework;
using Automated.Utilities.Reporting;
using Automated.Utilities.AutomationAbstractions.CoreActions;
using System.Reflection;

namespace TAMM.Automation.Test
{
    public class TestMain
    {
        protected TestReport AutomationReport;

        /// <summary>
        /// Implement the logic that needs to run once before all the tests.
        /// </summary>
        [OneTimeSetUp]
        protected void OneTimeSetup()
        {
            AutomatedLogger.Log("TestMain: OneTimeSetup");

            //Read the Test Configs(including the ones in app.config)
            string applicationName = Assembly.GetExecutingAssembly().GetName().Name;

            InitializeConfigs(applicationName);

            //Initialize the report and attach the html reporter to it
            AutomationReport = new TestReport(TestConfigs.ReportingDirectory);
        }

        /// <summary>
        /// Implement logic that has to run before executing each scenario.
        /// </summary>
        [SetUp]
        public void SetupTest()
        {
            AutomatedLogger.Log("TestMain : SetupTest");

            //Create a new test entry for this test in the report
            AutomationReport.CreateTest();

            //Setup the Test Commons
            OpenApplication();

            //Simple Assert at the beginning of each test. It checks the loading of the entry page of the test.
            IsEntryPageLoaded();
        }

        //list your automated browsers that you are going to work on here
        public static AutomatedBrowser ActiveBrowser;

        public void InitializeConfigs(string nameOfTestingDirectory)
        {
            //TODO: Virtual
            //Get the current context path
            TestConfigs.SetCurrentContextPath(nameOfTestingDirectory);

            //Read the Test Configs (including the ones in app.config)    
            TestConfigs.Init();

            ////TODO: Virtual
            ////Read the automated app configs
            //TestConfigs.ReadApplicationConfigs();
        }

        /// <summary>
        /// Open the application url specified in the configs.
        /// Must be called after initializing the configs.
        /// </summary>
        public void OpenApplication()
        {
            AutomatedLogger.Log("OpenApplication: Go to the application url provided in the configs");

            //Start a new Browser : Initialize
            ActiveBrowser = new AutomatedBrowser(TestConfigs.Browser, isGridEnabled: false);

            //Open Website in the browser started by Selenium
            if (!string.IsNullOrEmpty(TestConfigs.Url))
            {
                AutomatedActions.NavigationActions.NavigateToUrl(TestConfigs.Url);
                AutomatedActions.WindowActions.Maximize();
            }

            AutomatedLogger.Log("Exiting OpenApplication");

        }//end method Common Setup


        public void IsEntryPageLoaded()
        {
            bool isPageLoaded = AutomatedActions.WaitActions.WaitForJSandJQueryToLoad();
            AutomationReport.AssertAndReportStatus(isPageLoaded, "Loading the test's entry page", "Entry page loaded", "Failed to load the entry page");
        }

        [TearDown]
        public void TeardownTest()
        {
            //Close the Browser
            AutomatedBrowser.TearDown();

            //Finalize and generate the report
            AutomationReport.GenerateTestReport();

            AutomatedLogger.Log("TestMain: TeardownTest");
        }
        
}//end class

}//namespace
