using Automated.Utilities.AutomationAbstractions.Components;
using Automated.Utilities.Utilities;
using Automated.Application;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using Automated.Utilities.Reporting;

namespace Automated.Tests
{
    public class TestMain : TestCommons
    {
        AppCommons _applicationCommons;
        protected ExtentReports _extent;
        [SetUp]
        public void SetupTest()
        {
           //AutomatedLogger.Log("Main Test: Setup Tests");
            //commented by asmaa: Common_Setup();

            _applicationCommons = new AppCommons();
            _applicationCommons.Common_Setup();

            //Implement logic that has to run before executing each scenario
            //Here, within the test context - this is the place that you can get where you are running.

            string currentPath = TestContext.CurrentContext.WorkDirectory.Replace("bin\\Debug", "");
            currentPath = currentPath.Replace("Test.Automation", "?");
            currentPath = currentPath.Replace("Automated.Tests", "?");
            currentPath = currentPath.Remove(currentPath.LastIndexOf('?'));

            TestConfigs.PathOfCurrentContext = currentPath;
            string automationConf = currentPath + "automation.conf";

            //Setup the Test Commons
            TestCommons_Setup(automationConf);

            AutomatedLogger.Log("Main Test: Setup Tests");

            //TestReport.Report_init(automationConf+".dir");

            //Create new test log with its title in the final report
            TestReport.test = TestReport.extent.CreateTest(TestContext.CurrentContext.Test.Name);

        }

        [TearDown]
        public void TeardownTest()
        {
            //Close the Browser : EndTest
            AutomatedBrowser.TearDown();
            TestReport.GenerateReport();
           AutomatedLogger.Log("Main Test: Teardown Tests");
        }
    }//end class

}//namespace
