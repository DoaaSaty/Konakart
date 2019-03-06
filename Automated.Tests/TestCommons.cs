using Automated.Application;
using Automated.Utilities.AutomationAbstractions;
using Automated.Utilities.AutomationAbstractions.Components;
using Automated.Utilities.AutomationAbstractions.CoreActions;
using Automated.Utilities.Utilities;
using Automated.Utilities.Utilities.Parsers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Automated.Tests
{
    public class TestCommons
    {
        //TODO: pass it as a parameter
        public IWebDriver _driver;
        //list your automated browsers that you are going to work on here
        public static AutomatedBrowser ActiveBrowser;
        
        public void TestCommons_Setup(string automationConfigs)
        {
            AutomatedLogger.Log("Entering the Common Setup method");

            //Read the Test Configs (including the ones in app.config)    
            TestConfigs.Init(automationConfigs);
            ApplicationConfigs.ReadConfigs(TestConfigs.PathOfCurrentContext);

            //Start a new Browser : Initialize
            ActiveBrowser = new AutomatedBrowser(TestConfigs.Browser, isGridEnabled: false);

            //Open Website in the browser started by Selenium
            if (!string.IsNullOrEmpty(TestConfigs.Url))
            {
                AutomatedActions.NavigationActions.NavigateToUrl(TestConfigs.Url);

                // Login
                //WindowsLogin();
                AutomatedActions.WindowActions.Maximize();
            }

          

            AutomatedLogger.Log("Exiting Common Setup method");

        }//end method Common Setup

        public void WindowsLogin()
        {
            IAlert alert = _driver.SwitchTo().Alert();

            alert.SetAuthenticationCredentials(TestConfigs.userID, TestConfigs.password);
            _driver.SwitchTo().Alert().Accept();
            _driver.SwitchTo().DefaultContent();
        }
    }//end class TestCommons

}//end namespace Automated.Tests
