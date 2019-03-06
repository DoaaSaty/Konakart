using Automated.Utilities.AutomationAbstractions.Components;
using Automated.Utilities.AutomationAbstractions.CoreActions;
using Automated.Utilities.Utilities;
using Automated.Utilities.Utilities.Parsers;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Reflection;
using TAMM.Automation.Application;
using TAMM.Automation.Application.Domain.Model;
using TAMM.Automation.Application.Pages;
using TAMM.Automation.Application.Pages.TAMM.MediaCenter;

namespace TAMM.Automation.Test
{
    public class TestCommons : TestMain
    {
        [OneTimeSetUp]
        public void ReadConfigs()
        {
            //Read Application Configs
            TammApplicationConfigs.ReadApplicationConfigs();
            TammApplicationConfigs.ReadConfigs();
            TammApplicationConfigs.ReadTammConfigs();
        }

        public void SwitchLanguage(string language)
        {
            if (language.Equals("arabic"))
            {
                AutomatedActions.NavigationActions.NavigateToUrl(TammApplicationConfigs.ArabicURL);
            }
        }

        public void SwitchLanguageMediaCenter(MediaCenterPage page, string language)
        {
            if (language == "arabic")
                page.NavigateToMediaCenter(TammApplicationConfigs.MediaCenterUrl_Ar);
            else
                page.NavigateToMediaCenter(TammApplicationConfigs.MediaCenterUrl_En);
        }

        public void AdminLogin()
        {
            AutomatedActions.NavigationActions.NavigateToUrl(TammApplicationConfigs.AdminUrl);
            Login();
        }

        public void Login()
        {
            LoginPage _LoginPage = new LoginPage(AutomatedBrowser.WebDriverInstance);

            //Prepare test data
            var _testCaseName = MethodBase.GetCurrentMethod().Name;
            var _worksheetName = "LoginTest";
            var _username = ExcelDataParser.GetValueOf(LoginPageColumns._userName, _testCaseName, _worksheetName);
            var _password = ExcelDataParser.GetValueOf(LoginPageColumns._password, _testCaseName, _worksheetName);

            //Steps
            _LoginPage.Login(_username, _password);
        }

    }//end class TestCommons

}//end namespace Automated.Tests
