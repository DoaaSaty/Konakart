using System.Collections.Generic;
using Automated.Utilities.AutomationAbstractions.Components;
using Automated.Utilities.AutomationAbstractions.CoreActions;
using Automated.Utilities.Utilities.Parsers;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Automated.Utilities.Utilities;
using System.Linq;

using Automated.Application;

namespace Automated.Application.Pages
{
    public class HomePage
    {
        Dictionary<string, AutomatedElement> pageElements;
        private AutomatedElement _gameBtn, _rightSliderController, _leftSliderController, _slider, _gameItemsList,
            _shoppingMenuItem, _shoppingBagItemText, _addToCartItem;
        IWebDriver driver;
        IReadOnlyCollection<IWebElement> gamesList;
        int lastitemInGameList;
        public static string gameName;
    public HomePage(IWebDriver driver)
    {
    pageElements = ElementParser.Initialize_Page_Elements(ApplicationConfigs.ObjectRepository + @"Home\Home.json");

            _gameBtn = pageElements["gameBtn"];
             _rightSliderController = pageElements["rightSliderController"];
            _leftSliderController = pageElements["leftSliderController"];
            _slider = pageElements["slider"];
            _gameItemsList = pageElements["gameItemsList"];
            _shoppingMenuItem = pageElements["shoppingMenuItem"];
            _shoppingBagItemText = pageElements["shoppingBagItemText"];
            lastitemInGameList = 0;
            _addToCartItem = pageElements["addToCartItem"];
            gameName = string.Empty;
            driver = this.driver;

    }

        public void ClickOnGamesBtn ()
        {
            AutomatedActions.WaitActions.WaitForWebElementToBeClickable(_gameBtn, 120);
            AutomatedActions.ClickActions.ClickOnElement(_gameBtn);
        }
        public void MoveRightSliderToSpecificPrice(int x)
        {
            string scroll = string.Empty;
            //AutomatedActions.NavigationActions.ScrollDown(scroll);
            AutomatedActions.WaitActions.WaitForWebElementToBeClickable(_rightSliderController, 120);
            By by = By.XPath("//div[@id='price']/div[1]/div");
            bool elementExists = RetryingFinds(by);
            IWebElement rightslider = driver.FindElement(by); //driver.FindElement(By.XPath("//div[@id='price-range-slider']/div"));
            int width = int.Parse(rightslider.GetProperty("width"));
            Actions move = new Actions(driver);
            move.MoveToElement(rightslider, ((width * x) / 100), 0).Click();
            move.Build().Perform();
            AutomatedActions.WaitActions.Wait(5);
        }
        public static bool RetryingFinds(By by)
        {
            bool result = false;
            int attempts = 0;
            while (attempts < 3)
            {
                try
                {
                    AutomatedBrowser.WebDriverInstance.FindElements(by);
                    result = true;
                    break;
                }
                catch (StaleElementReferenceException e)
                {
                    AutomatedLogger.Log(e.Message);
                }
                attempts++;
            }
            return result;
        }
        public void SelectLastGameIsListAndAddItToCart()
        {

            AutomatedActions.WaitActions.WaitForWebElementToBeClickable(_gameItemsList, 120);
            gamesList = _gameItemsList.GetActualWebElements();
            lastitemInGameList = gamesList.Count;
            IWebElement element = driver.FindElement(By.XPath("//div[@class='items']/ul/li[" + lastitemInGameList + "]/div/div/div[2]/div/a"));

            element.Click();



            //lastitemInGameList = gamesList.Count - 1;

            //Actions action = new Actions(driver);
            //action.MoveToElement(element).Click().Build().Perform();
            //bool visisble = ElementUtil.isElementPresent(By.CssSelector("a[class='add-to-cart-button button small-rounded-corners']"), driver, 120);
            //element.FindElement(By.CssSelector("a[class='add-to-cart-button button small-rounded-corners']")).Click();
            //AutomatedActions.WaitActions.WaitForWebElementToBeClickable(_addToCartItem, 120);
            //gamesList = _addToCartItem.GetActualWebElements();
            //lastitemInGameList = gamesList.Count;
            //gamesList.ElementAt(lastitemInGameList).ClickUsingJavaScript();

        }
        public void HoverOnShoppingMenuItem()
        {
            AutomatedActions.WaitActions.WaitForWebElementToBeClickable(_shoppingMenuItem, 120);
            AutomatedActions.ClickActions.HoverOnElement(_shoppingMenuItem);
        }
        public bool VerifyGameIsAddedToShoppingBag()
        {
            bool gameAdded = false;
            string itemText = string.Empty;
            itemText = string.Empty;
            AutomatedActions.WaitActions.WaitForWebElementToBeClickable(_shoppingBagItemText, 120);
            itemText = AutomatedActions.ElementActions.GetTextOfElement(_shoppingBagItemText);
            if (itemText != null && itemText.Contains("1"))//  adding the first item from home page
                gameAdded = true;

            else if (itemText != null && itemText.Contains("3"))// adding two more items from game details which makes them 3
                gameAdded = true;

            return gameAdded; //nothing is addeda

        }
        public void ClickOnLastGameInList()
        {
            gameName = gamesList.ElementAt(lastitemInGameList).FindElement(By.XPath("//div[@class='item']/a")).Text;
            gamesList.ElementAt(lastitemInGameList).FindElement(By.CssSelector("img[class='item-img']")).Click();
            
        }
        public static IReadOnlyCollection<IWebElement> RetryingFindElements(By by)
        {
            IReadOnlyCollection<IWebElement> elements = null;
            int attempts = 0;
            while (attempts < 3)
            {
                try
                {
                    elements = AutomatedBrowser.WebDriverInstance.FindElements(by);
                    break;
                }
                catch (StaleElementReferenceException e)
                {
                    AutomatedLogger.Log(e.Message);
                }
                attempts++;
            }
            return elements;
        }
       

    }

}
