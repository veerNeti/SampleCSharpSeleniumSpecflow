using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CSharpUISelenium.ServiceImplimentation
{
  public  interface IElementServices
    {
        void ClearSendText(IWebElement ele, string text);
        bool WaitTillElementIsInvisibleBy(By by);
        bool CheckInvisibilityOfElementWithText(By by, string text);
        IWebElement WaitForElementToAppear(By by);
        void elementScrollToViewBy(By by);
        void elementScrollToViewEle(IWebElement ele);
        ReadOnlyCollection<IWebElement> WaitForVisibilityOfAllElementsLocatedBy(By by);
        bool checkIfTheGivenTextIsPresentInTheSpecifiedBy(By by, string text);
        bool checkIfTheGivenTextIsPresentInTheSpecifiedWebElement(IWebElement webElement, string text);
        IWebElement WaitForVisibilityOfAnElementBy(By by);
        IWebElement WaitForVisibilityOfAnElement(IWebElement webElement);
        IWebElement WaitForAnElementClickableBy(By by);
        IWebElement WaitForAnElementClickable(IWebElement ele);
        bool WaitForExpectedUrlToBe(string url);
        bool WaitForExpectedUrlToContainString(string url);
        bool WaitForExpectedTitleOfPageContainsString(string url);
        IAlert checkAlterIsPresent();
        IWebElement CheckelementIsPresentOnTheDOM(By by);
        ReadOnlyCollection<IWebElement> WaitforVisibilityOfAllElementsLocated(ReadOnlyCollection<IWebElement> elements);
        bool WaitforElementIsNoLongerAttachedToTheDOM(IWebElement element);

        void EnterTextIntoList(By locator, string text);
        void SelectItemDropTheDropDownBy(By by, string value, int? index, DROPDOWNSELECTION type);

        List<string> ExtractLabelsFromListOfWebElementsToList(By by);
        bool WaitForUrl(string expectedUrl);
    }

    public enum MOUSE_ACTION_TYPE
    {
        MOVE_TO_ELEMENT, //hover action
        CLICK,
        DOUBLE_CLICK,
        CLICK_HOLD,
        CONTEXT_CLICK,
        DRAG_DROP,
    }
}
