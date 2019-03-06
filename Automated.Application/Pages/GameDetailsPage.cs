using System.Collections.Generic;
using Automated.Utilities.AutomationAbstractions.Components;
using Automated.Utilities.AutomationAbstractions.CoreActions;
using Automated.Utilities.Utilities.Parsers;
using OpenQA.Selenium;
using System.Linq;
using System.Collections;
using Automated.Application;

namespace Automated.Application.Pages
{
    public class GameDetailsPage
    {
        Dictionary<string, AutomatedElement> pageElements;
        private AutomatedElement _addToCartBtn, _gameName, _screeShots, _quantityDdl;
        IWebDriver _driver;
        

        public GameDetailsPage(IWebDriver driver)
        {
            _driver = driver;
            pageElements = ElementParser.Initialize_Page_Elements(ApplicationConfigs.ObjectRepository + @"GameDetails\GameDetails.json");
            _addToCartBtn = pageElements["addToCartBtn"];
            _gameName = pageElements["gameName"];
            _screeShots = pageElements["screeShots"];
            _quantityDdl = pageElements["quantityDdl"];
        }

        public void ClickOnAddToCartBtn()
        {
            AutomatedActions.WaitActions.WaitForWebElementToBeClickable(_addToCartBtn, 120);
            IReadOnlyCollection<IWebElement> addToCartBtns = _addToCartBtn.GetActualWebElements();
            addToCartBtns.ElementAt(0).Click();

        }
      public bool VerifyGameName()
        {
            AutomatedActions.WaitActions.WaitForWebElementToBeClickable(_gameName, 120);
            if (AutomatedActions.ElementActions.GetTextOfElement(_gameName).Equals(HomePage.gameName))
                return true;
            else return false;
               
        }

        public bool VerifyTheGameHasFourDifferentScreenshots()
        {
            ArrayList screenShotAttributes = new ArrayList();
            AutomatedActions.WaitActions.WaitForWebElementToBeClickable(_screeShots, 120);
            IReadOnlyCollection<IWebElement> screenShotsList = _screeShots.GetActualWebElements();
            for (int i = 0; i < screenShotsList.Count; i++)
            {
                // each screenshot has different rel attribute value, so checking on having 4 diff attributes
                // which maps to 4 diff screenshots
                if (!screenShotAttributes.Contains(screenShotsList.ElementAt(i).GetAttribute("rel")))
                    screenShotAttributes.Add(screenShotsList.ElementAt(i).GetAttribute("rel"));
            }
            // verifying the screenShotAttributes has 4 values which means we have 4 screenshots
            if (screenShotAttributes.Count == 4)
                return true;
            else return false;
        }
        public void SelectTwoItemsFromTheQuantityDdl(string quantity)
        {
            AutomatedActions.WaitActions.WaitForWebElementToBeClickable(_quantityDdl, 120);
            AutomatedActions.SelectActions.SelectOptionByText(_quantityDdl,quantity);
        }
    }

}
