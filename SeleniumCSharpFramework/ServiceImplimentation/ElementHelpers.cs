using CSharpUISelenium.DriverManager;
using CSharpUISelenium.ServiceImplimentation;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;

namespace CSharpUISelenium.Services
{
    public class ElementHelpers : Base, IElementServices
    {
        static internal IConfigurationRoot appSettingReader = new ConfigurationBuilder().AddJsonFile(@"appsettings.json", optional: true).Build();
        private static int MAXTIMEOUT = int.Parse(appSettingReader["Time-out"]);
        private static int RETRY = int.Parse(appSettingReader["RETRY"]);

        private IUtilServices _utilServices = new Utils();

        public ElementHelpers()
        {
            _utilServices = _utilServices ?? new Utils();
        }
        [Obsolete]
        public IAlert checkAlterIsPresent()
        {
            IAlert result = null;
            try
            {
                result = ExpectedConditions.AlertIsPresent().Invoke(driver);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

        [Obsolete]
        public IWebElement CheckelementIsPresentOnTheDOM(By by)
        {
            IWebElement result = null;
            try
            {
                WebDriverWait waitForElement = new WebDriverWait(driver, timeout: TimeSpan.FromSeconds(MAXTIMEOUT))
                {
                    PollingInterval = TimeSpan.FromMilliseconds(MAXTIMEOUT),
                };
                waitForElement.IgnoreExceptionTypes(typeof(NoSuchElementException));
                waitForElement.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
                result = waitForElement.Until(ExpectedConditions.ElementExists(by));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

        [Obsolete]
        public bool checkIfTheGivenTextIsPresentInTheSpecifiedBy(By by, string text)
        {
            bool result = false;
            try
            {
                var wait = new WebDriverWait(driver, timeout: TimeSpan.FromSeconds(MAXTIMEOUT))
                {
                    PollingInterval = TimeSpan.FromMilliseconds(MAXTIMEOUT),
                };
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
                result = wait.Until(ExpectedConditions.TextToBePresentInElementLocated(by, text));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

        [Obsolete]
        public bool CheckInvisibilityOfElementWithText(By by, string text)
        {
            bool result = false;
            try
            {
                var wait = new WebDriverWait(driver, timeout: TimeSpan.FromSeconds(MAXTIMEOUT))
                {
                    PollingInterval = TimeSpan.FromMilliseconds(MAXTIMEOUT),
                };
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
                result = wait.Until(ExpectedConditions.InvisibilityOfElementWithText(by, text));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

        public void ClearSendText(IWebElement ele, string text)
        {
            WaitForVisibilityOfAnElement(ele);
            WaitForAnElementClickable(ele);
            ele.Clear();
            ele.SendKeys(text);
        }

        [Obsolete]
        public void elementScrollToViewBy(By by)
        {
            IJavaScriptExecutor _js = (IJavaScriptExecutor)driver;
            try
            {
                _js.ExecuteScript("arguments[0].scrollIntoView(true);", WaitForElementToAppear(by));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        [Obsolete]
        public void elementScrollToViewEle(IWebElement ele)
        {
            IJavaScriptExecutor _js = (IJavaScriptExecutor)driver;
            try
            {
                _js.ExecuteScript("arguments[0].scrollIntoView(true);", WaitForVisibilityOfAnElement(ele));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        [Obsolete]
        public IWebElement WaitForAnElementClickableBy(By by)
        {
            IWebElement result = null;
            try
            {
                WebDriverWait waitForElement = new WebDriverWait(driver, timeout: TimeSpan.FromSeconds(MAXTIMEOUT))
                {
                    PollingInterval = TimeSpan.FromMilliseconds(MAXTIMEOUT),
                };
                waitForElement.IgnoreExceptionTypes(typeof(NoSuchElementException));
                waitForElement.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
                result = waitForElement.Until(ExpectedConditions.ElementToBeClickable(by));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

        [Obsolete]
        public IWebElement WaitForAnElementClickable(IWebElement ele)
        {
            IWebElement result = null;
            try
            {
                WebDriverWait waitForElement = new WebDriverWait(driver, timeout: TimeSpan.FromSeconds(MAXTIMEOUT))
                {
                    PollingInterval = TimeSpan.FromMilliseconds(MAXTIMEOUT),
                };
                waitForElement.IgnoreExceptionTypes(typeof(NoSuchElementException));
                waitForElement.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
                result = waitForElement.Until(ExpectedConditions.ElementToBeClickable(ele));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

        [Obsolete]
        public IWebElement WaitForVisibilityOfAnElementBy(By by)
        {
            IWebElement result = null;
            try
            {
                WebDriverWait waitForElement = new WebDriverWait(driver, timeout: TimeSpan.FromSeconds(MAXTIMEOUT))
                {
                    PollingInterval = TimeSpan.FromMilliseconds(MAXTIMEOUT),
                };
                waitForElement.IgnoreExceptionTypes(typeof(NoSuchElementException));
                waitForElement.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
                result = waitForElement.Until(ExpectedConditions.ElementIsVisible(by));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }
        [Obsolete]
        public IWebElement WaitForVisibilityOfAnElement(IWebElement webElement)
        {
            int i = 0;
            IWebElement result = null;
            while (i < RETRY)
            {
                try
                {
                    var wait = new WebDriverWait(driver, timeout: TimeSpan.FromSeconds(MAXTIMEOUT))
                    {
                        PollingInterval = TimeSpan.FromMilliseconds(MAXTIMEOUT),
                    };
                    wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                    wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
                    if (wait.Until(driver => webElement.Displayed))
                    {
                        result = webElement;
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Thread.Sleep(TimeSpan.FromSeconds(MAXTIMEOUT));
                i++;
            }

            return result;
        }

        [Obsolete]
        public bool WaitforElementIsNoLongerAttachedToTheDOM(IWebElement element)
        {
            bool result = false;
            try
            {
                WebDriverWait waitForElement = new WebDriverWait(driver, timeout: TimeSpan.FromSeconds(MAXTIMEOUT))
                {
                    PollingInterval = TimeSpan.FromMilliseconds(MAXTIMEOUT),
                };
                waitForElement.IgnoreExceptionTypes(typeof(NoSuchElementException));
                waitForElement.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
                result = waitForElement.Until(ExpectedConditions.StalenessOf(element));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

        [Obsolete]
        public IWebElement WaitForElementToAppear(By by)
        {
            IWebElement result = CheckelementIsPresentOnTheDOM(by);
            if (result == null)
            {
                try
                {
                    var wait = new WebDriverWait(driver, timeout: TimeSpan.FromSeconds(MAXTIMEOUT))
                    {
                        PollingInterval = TimeSpan.FromMilliseconds(MAXTIMEOUT),
                    };
                    wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                    wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
                    result = wait.Until(ExpectedConditions.ElementIsVisible(by));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return result;
        }

        [Obsolete]
        public bool WaitForExpectedTitleOfPageContainsString(string url)
        {
            bool result = false;
            try
            {
                result = ExpectedConditions.TitleContains(url).Invoke(driver);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

        [Obsolete]
        public bool WaitForExpectedUrlToBe(string url)
        {
            bool result = false;
            try
            {
                result = ExpectedConditions.UrlToBe(url).Invoke(driver);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

        [Obsolete]
        public bool WaitForExpectedUrlToContainString(string url)
        {
            bool result = false;
            try
            {
                result = ExpectedConditions.UrlContains(url).Invoke(driver);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

        [Obsolete]
        public bool WaitForUrl(string expectedUrl)
        {
            bool result = false;
            int attempt = 0;
            while (attempt <= RETRY)
            {
                if (_utilServices.StringValidation(driver.Url, expectedUrl, STRINGCHECK.CONT))
                {
                    result = WaitForExpectedUrlToContainString(expectedUrl);
                    break;
                }
                else
                {
                    attempt++;
                    Thread.Sleep(TimeSpan.FromSeconds(MAXTIMEOUT - 20));
                }
            }
            return result;
        }

        [Obsolete]
        public ReadOnlyCollection<IWebElement> WaitforVisibilityOfAllElementsLocated(ReadOnlyCollection<IWebElement> elements)
        {
            int i = 0;
            ReadOnlyCollection<IWebElement> result = null;

            while (i < RETRY)
            {
                try
                {
                    WebDriverWait waitForElement = new WebDriverWait(driver, timeout: TimeSpan.FromSeconds(MAXTIMEOUT))
                    {
                        PollingInterval = TimeSpan.FromMilliseconds(MAXTIMEOUT),
                    };
                    waitForElement.IgnoreExceptionTypes(typeof(NoSuchElementException));
                    waitForElement.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
                    result = waitForElement.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(elements));

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                if (result != null)
                {
                    break;
                }
                else
                {
                    Thread.Sleep(TimeSpan.FromSeconds(MAXTIMEOUT));
                    i++;
                }
            }
            return result;
        }

        [Obsolete]
        public ReadOnlyCollection<IWebElement> WaitForVisibilityOfAllElementsLocatedBy(By by)
        {
            ReadOnlyCollection<IWebElement> result = null;
            try
            {
                var wait = new WebDriverWait(driver, timeout: TimeSpan.FromSeconds(MAXTIMEOUT))
                {
                    PollingInterval = TimeSpan.FromMilliseconds(MAXTIMEOUT),
                };
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
                result = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

        [Obsolete]
        public bool WaitTillElementIsInvisibleBy(By by)
        {

            bool result = false;
            try
            {
                var wait = new WebDriverWait(driver, timeout: TimeSpan.FromSeconds(MAXTIMEOUT))
                {
                    PollingInterval = TimeSpan.FromMilliseconds(MAXTIMEOUT),
                };
                result = wait.Until(ExpectedConditions.InvisibilityOfElementLocated(by));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

        [Obsolete]
        public bool checkIfTheGivenTextIsPresentInTheSpecifiedWebElement(IWebElement webElement, string text)
        {
            bool result = false;
            try
            {
                result = _utilServices.StringValidation(WaitForVisibilityOfAnElement(webElement).Text, text, STRINGCHECK.EQL);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

        [Obsolete]
        public void EnterTextIntoList(By locator, string text)
        {
            ReadOnlyCollection<IWebElement> List = WaitForVisibilityOfAllElementsLocatedBy(locator);
            foreach (var item in List)
            {
                WaitForAnElementClickable(item);
                ClearSendText(item, text.Trim());
            }
        }

        [Obsolete]
        public void SelectItemDropTheDropDownBy(By by, string value, int? index, DROPDOWNSELECTION type)
        {
            ReadOnlyCollection<IWebElement> dropDownItems = WaitForVisibilityOfAllElementsLocatedBy(by);

            switch (type)
            {
                case DROPDOWNSELECTION.INDEX:
                    for (int i = 0; i < dropDownItems.Count; i++)
                    {
                        if (index != null && (index == i))
                        {
                            WaitForVisibilityOfAnElement(dropDownItems[i]);
                            MouseInteractions.MouseActions(dropDownItems[i], null, driver, MOUSE_ACTION_TYPE.CLICK);
                        }
                    }
                    break;
                case DROPDOWNSELECTION.VALUE:
                    for (int i = 0; i <= dropDownItems.Count; i++)
                    {
                        WaitForVisibilityOfAnElement(dropDownItems[i]);
                        if (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value) && _utilServices.StringValidation(dropDownItems[i].Text, value, STRINGCHECK.EQL))
                        {
                            MouseInteractions.MouseActions(dropDownItems[i], null, driver, MOUSE_ACTION_TYPE.CLICK);
                            break;
                        }
                    }
                    break;
            }
        }


        [Obsolete]
        public List<string> ExtractLabelsFromListOfWebElementsToList(By by)
        {
            ReadOnlyCollection<IWebElement> listEle = WaitForVisibilityOfAllElementsLocatedBy(by);
            ReadOnlyCollection<IWebElement> readOnlyCollections = WaitforVisibilityOfAllElementsLocated(listEle);
            List<string> liststring = null;
            try
            {
                liststring = new List<string>();
                foreach (var item in readOnlyCollections)
                {
                    liststring.Add(item.Text);
                }
            }
            catch (Exception)
            {
                liststring = new List<string>();
                for (int i = 0; i < readOnlyCollections.Count; i++)
                {
                    liststring.Add(WaitForVisibilityOfAllElementsLocatedBy(by)[i].Text);
                }
            }
            return liststring;
        }
    }
}

public static class MouseInteractions
{

    public static void MouseActions(this IWebElement sourceEle, IWebElement targetEle, IWebDriver driver, MOUSE_ACTION_TYPE actionsType)
    {
        Actions actionProvider = new Actions(driver);
        switch (actionsType)
        {
            case MOUSE_ACTION_TYPE.MOVE_TO_ELEMENT:
                // Performs mouse move action onto the element
                actionProvider.MoveToElement(sourceEle).Build().Perform();
                break;
            case MOUSE_ACTION_TYPE.CLICK:
                // Perform click action on the element
                actionProvider.MoveToElement(sourceEle).Click(sourceEle).Build().Perform();
                break;
            case MOUSE_ACTION_TYPE.DOUBLE_CLICK:
                // Perform double-click action on the element
                actionProvider.MoveToElement(sourceEle).DoubleClick(sourceEle).Build().Perform();
                break;
            case MOUSE_ACTION_TYPE.CLICK_HOLD:
                // Perform click-and-hold action on the element
                actionProvider.ClickAndHold(sourceEle).MoveToElement(targetEle).Build().Perform();
                // Performs release event              
                actionProvider.Release().Build().Perform();
                break;
            case MOUSE_ACTION_TYPE.CONTEXT_CLICK:
                // Perform Context-click action on the element
                actionProvider.MoveToElement(sourceEle).ContextClick(sourceEle).Build().Perform();
                break;
            case MOUSE_ACTION_TYPE.DRAG_DROP:
                // Performs drag and drop action of sourceEle onto the targetEle
                actionProvider.DragAndDrop(sourceEle, targetEle).Build().Perform();
                break;
        }
    }
}

