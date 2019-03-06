using Automated.Utilities.AutomationAbstractions.Components;
using Automated.Utilities.Utilities.Parsers;
using AventStack.ExtentReports;
using NUnit.Framework;

using System;
using System.Reflection;
using Automated.Tests;
using Automated.Application.Pages.Home;

namespace Automated.Tests.Scripts
{
    public class GameTests : TestMain
    {
        private HomePage _homePage;
        
        private GameDetailsPage _gameDetailsPage;
        private ExtentTest _test;
        private GameDetailsColumns _gameDetailsColumns;
        [SetUp]
        public void Setup()
        {
            _homePage = new HomePage(AutomatedBrowser.WebDriverInstance);
            _gameDetailsPage = new GameDetailsPage(AutomatedBrowser.WebDriverInstance);
            _gameDetailsColumns = new GameDetailsColumns();
        }


        [Test, Order(1), Retry(TestConfigs.MaxNumberOfRetries)]

        public void BuyOnlineGame()
        {
            try
            {

                #region TestData
                var _testCaseName = MethodBase.GetCurrentMethod().Name;
                var _worksheetName = GetType().Name;
                int x = 20;
                var _quntity = ExcelDataParser.GetValueOf(GameDetailsColumns.quantity, _testCaseName, _worksheetName);
                
                #endregion

                _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name, "BuyOnlineGame");
                _test.Log(Status.Info, "Konakart home page is loaded");

                //Steps
                _homePage.ClickOnGamesBtn();
                // _homePage.MoveRightSliderToSpecificPrice(x);
                _homePage.SelectLastGameIsListAndAddItToCart();
                _homePage.HoverOnShoppingMenuItem();
                if (!_homePage.VerifyGameIsAddedToShoppingBag())
                    _test.Log(Status.Warning, "The item is not added to the shopping bag");
                _homePage.ClickOnLastGameInList();
                if(!_gameDetailsPage.VerifyGameName())
                    _test.Log(Status.Warning, "The name of the game in the details page isn't equal to the game in the home page");
                if(!_gameDetailsPage.VerifyTheGameHasFourDifferentScreenshots())
                    _test.Log(Status.Warning, "The game doesn't have 4 different screenshots");

                _gameDetailsPage.SelectTwoItemsFromTheQuantityDdl(_quntity);
                _gameDetailsPage.ClickOnAddToCartBtn();
                if (!_homePage.VerifyGameIsAddedToShoppingBag())
                    _test.Log(Status.Warning, "The items are not added to the shopping bag");


                //if (_FindPage.GetVcRequestStatus().Equals(RequestStatus.VcSubmittedStatus))
                //{
                //    _FindPage.OpenVcDetails();
                //    if (_FindPage.GetGsRequestStatus().Equals(RequestStatus.VcSubmittedStatus))
                //    {
                //        _test.Pass(TestContext.CurrentContext.Test.Name + "is passed ");
                //        Assert.IsTrue(true);
                //    }
                //}
                //_MenuHeaderPage.Logout();

            }
            catch (Exception e)
            {

                _test.Fail(TestContext.CurrentContext.Test.Name + " is failed ");
                _test.Log(Status.Fail, e.ToString());
                Assert.Fail(e.ToString());
            }

        }

       

    }
}
