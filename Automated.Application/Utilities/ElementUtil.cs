using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Threading;
using Automated.Utilities.AutomationAbstractions.CoreActions;
using Automated.Utilities.AutomationAbstractions.Components;
using System.Windows.Forms;
using System.Collections;
using System.Resources;
using System.Configuration;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using Automated.Utilities.Utilities;

namespace TAMM.Automation.Application.Utilities
{
    public static class ElementUtil
    {
        public static void SelectFromDynamicList(string value, IList<IWebElement> viewsElementsItems)
        {

            var viewsItemsValues = GetTextFromWebElements(viewsElementsItems);

            var viewsElementsNames = new Dictionary<IWebElement, string>();
            for (int i = 0; i < viewsElementsItems.Count; i++)
            {
                viewsElementsNames.Add(viewsElementsItems.ElementAt(i), viewsItemsValues[i]);
            }
            foreach (var view in viewsElementsNames)
            {
                if (view.Value.Equals(value))
                {
                    Thread.Sleep(2000);
                    view.Key.Click();
                    Thread.Sleep(2000);
                    break;
                }
            }
        }

        public static List<string> GetTextFromWebElements(IList<IWebElement> viewsElementsItems)
        {
            var textLst = new List<string>();
            foreach (var ele in viewsElementsItems)
            {
                textLst.Add(ele.Text);
            }

            return textLst;
        }

        public static void UploadFile(string filePath, AutomatedElement element)
        {
            AutomatedActions.ClickActions.ClickOnElement(element);
            Thread.Sleep(5000);
            SendKeys.SendWait(filePath);
            Thread.Sleep(5000);
            SendKeys.SendWait(@"{Enter}");
            Thread.Sleep(5000);
        }

        public static void UpdateResourceFile(string resourceKey, string value, string appConfigKeyForResourceFile)
        {
            var resx = new List<DictionaryEntry>();
            using (var reader = new ResXResourceReader(appConfigKeyForResourceFile))
            {
                resx = reader.Cast<DictionaryEntry>().ToList();
                var existingResource = resx.Where(r => r.Key.ToString() == resourceKey).FirstOrDefault();
                var modifiedResx = new DictionaryEntry() { Key = existingResource.Key, Value = value };
                resx.Remove(existingResource);  // REMOVING RESOURCE!
                resx.Add(modifiedResx);  // AND THEN ADDING RESOURCE!
            }
            using (var writer = new ResXResourceWriter(appConfigKeyForResourceFile))
            {
                resx.ForEach(r =>
                {
                    // Again Adding all resource to generate with final items
                    writer.AddResource(r.Key.ToString(), r.Value.ToString());
                });
                writer.Generate();
            }
        }

        public static bool WaitUntilWebElementsLoaded(this AutomatedElement ele, int elementCount, IWebDriver _driver, double waitingSec)
        {
            bool elementIsLoaded = false;
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(waitingSec));
            try
            {
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.Until<Boolean>((d) =>
                {
                    ReadOnlyCollection<IWebElement> elements = d.FindElements(ele.ByElement);
                    if (elements.Count >= elementCount)
                    {
                        elementIsLoaded = true;
                    }

                    return elementIsLoaded;
                });
            }
            catch (Exception e)
            {

                throw new Exception("Element Name: " + ele.Name + " - Element Locator: " + ele.ByElement.ToString() + " Not Exists" + "and Exception is " + e.Message);
            }
            return elementIsLoaded;

        }

        public static void WaitUntilWebElementWithNameLoaded(this AutomatedElement ele, string name, IWebDriver _driver, double waitingSec)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(waitingSec));
            try
            {
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.Until<Boolean>((d) =>
                {
                    ReadOnlyCollection<IWebElement> elements = d.FindElements(ele.ByElement);
                    foreach (var e in elements)
                        if (e.Text.Equals(name) && e.Displayed &&
                            e.Enabled)
                        {
                            return true;
                        }

                    return false;
                });
            }
            catch (Exception e)
            {

                throw new Exception("Element Name: " + ele.Name + " - Element Locator: " + ele.ByElement.ToString() + " Not Exists" + "and Exception is " + e.Message);
            }
        }
        public static void WaitUntilWebElementsLoaded(this ReadOnlyCollection<IWebElement> ele, int elementCount, IWebDriver _driver, double waitingSec)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(waitingSec));
            try
            {
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.Until<Boolean>((d) =>
                {
                    if (ele.Count >= elementCount)
                    {
                        return true;
                    }

                    return false;
                });
            }
            catch (Exception e)
            {

                throw new Exception("Element Name: " + ele.ToString() + " Not Exists" + "and Exception is " + e.Message);
            }
        }
        public static void WaitUntilWebElementEnabledAndDispalyed(this AutomatedElement ele, IWebDriver _driver, double waitingSec)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(waitingSec));

            try
            {
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.Until<IWebElement>((d) =>
                {
                    IWebElement element = d.FindElement(ele.ByElement);
                    if (element.Displayed &&
                        element.Enabled)
                    {
                        return element;
                    }

                    return null;
                });
            }
            catch (Exception e)
            {

                AutomatedLogger.Log("Element Name: " + ele.Name + " - Element Locator: " + ele.ByElement.ToString() + " Not Exists" + "and Exception is " + e.Message);
                throw;

            }
        }
        public static void WaitUntilWebElementEnabledAndDispalyed(this IWebElement element, IWebDriver _driver, double waitingSec)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(waitingSec));

            try
            {
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.Until<IWebElement>((d) =>
                {
                    if (element.Displayed &&
                        element.Enabled)
                    {
                        return element;
                    }

                    return null;
                });
            }
            catch (Exception e)
            {

                throw new Exception(element.ToString() + " Not Exists" + "and Exception is " + e.Message);
            }
        }
        public static void WaitUntilWebElementsEnabledAndDispalyed(this ReadOnlyCollection<IWebElement> elements, IWebDriver _driver, double waitingSec)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(waitingSec));

            try
            {
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.Until<IWebElement>((d) =>
                {
                    foreach (IWebElement ele in elements)
                    {
                        if (ele.Displayed &&
                          ele.Enabled)
                        {
                            return ele;
                        }
                    }
                    return null;
                });
            }
            catch (Exception e)
            {

                throw new Exception(elements.ToString() + " Not Exists" + "and Exception is " + e.Message);
            }
        }

        public static bool isElementPresent(By by, IWebDriver driver)
        {
            bool present;
            try
            {
                driver.FindElement(by);
                present = true;
            }
            catch (NoSuchElementException e)
            {
                present = false;

            }
            return present;
        }

        public static void WaitUntilElementNameIsDisplayed(this AutomatedElement ele, IWebDriver _driver, double waitingSec, string str)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(waitingSec));
            try
            {
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.Until<Boolean>((d) =>
                {
                    ReadOnlyCollection<IWebElement> elements = d.FindElements(ele.ByElement);
                    foreach (var elemen in elements)
                    {
                        if (elemen.Text.ToLower().Contains(str.ToLower()))
                            return true;
                    }

                    return false;
                });
            }
            catch (Exception e)
            {

                throw new Exception("Element Name: " + ele.Name + " - Element Locator: " + ele.ByElement.ToString() + " Not Exists");
            }
        }


        public static void WaitUntilWebElementChanged(this AutomatedElement ele, IWebDriver _driver, double waitingSec)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(waitingSec));

            try
            {
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.Until<IWebElement>((d) =>
                {
                    IWebElement element = d.FindElement(ele.ByElement);
                    if (d.FindElement(ele.ByElement) != null)
                    {
                        return element;
                    }

                    return null;
                });
            }
            catch (Exception e)
            {

                throw new Exception("Element Name: " + ele.Name + " - Element Locator: " + ele.ByElement.ToString() + " Not Exists" + "and Exception is " + e.Message);
            }
        }

        public static bool WaitUntilWebElementTextChanged(this AutomatedElement ele, IWebDriver _driver, double waitingSec)
        {
            bool tasksLoaded = false;
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(waitingSec));

            try
            {
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.Until<Boolean>((d) =>
                {
                    IWebElement element = d.FindElement(ele.ByElement);
                    if (element.Text != "No Data")
                    {
                        tasksLoaded = true;
                    }

                    return tasksLoaded;
                });
            }
            catch (Exception e)
            {

                throw new Exception("Element Name: " + ele.Name + " - Element Locator: " + ele.ByElement.ToString() + " Not Exists" + "and Exception is " + e.Message);
            }
            return tasksLoaded;
        }
        public static bool WaitUntilWebElementDisappear(By elementXPath, IWebDriver _driver, double waitingSec)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(waitingSec));
            bool pageCompletedLoading = false;
            try
            {
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.Until<Boolean>((d) =>
                {
                    IWebElement element = d.FindElement(elementXPath);
                    if (d.FindElement(elementXPath) == null)
                    {
                        pageCompletedLoading = true;
                    }

                    return pageCompletedLoading;
                });
            }
            catch (Exception e)
            {

                throw new Exception("Element with Xpath: " + elementXPath+  " Not Exists" + "and Exception is " + e.Message);
            }
            return pageCompletedLoading;
        }

        public static string waitForElementNotVisible(int timeOutInSeconds, IWebDriver driver, string elementXPath)
        {
            TimeSpan ts = TimeSpan.FromTicks(timeOutInSeconds);
            if ((driver == null) || (elementXPath == null) || string.IsNullOrEmpty(elementXPath))
            {

                return "Wrong usage of WaitforElementNotVisible()";
            }
            try
            {
                (new WebDriverWait(driver, ts)).Until(ExpectedConditions.InvisibilityOfElementLocated(By
                        .XPath(elementXPath)));
                return null;
            }
            catch (TimeoutException e)
            {
                return "Build your own errormessage...";
            }
        }

        public static void WaitTillElementDisappers(AutomatedElement ele, IWebDriver _driver, double waitingSec)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(waitingSec));
            try
            {
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.Until<Boolean>((d) =>
                {
                    if (!(AutomatedActions.ElementActions.IsDisplayed(ele)))
                        return true;
                    else
                        return false;
                });
            }
            catch (Exception e)
            {

                throw new Exception("Element Name: " + ele.ToString() + " still appears " + "and Exception is " + e.Message);
            }
        }

        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
            return driver.FindElement(by);
        }

    }

}
