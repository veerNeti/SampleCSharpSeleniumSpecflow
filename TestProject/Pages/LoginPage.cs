using CSharpUISelenium.DriverManager;
using CSharpUISelenium.ServiceImplimentation;
using CSharpUISelenium.Services;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.Pages
{
    public class LoginPage : Base
    {

        private By UserName = By.CssSelector("input#user-name");
        private By Password = By.CssSelector("input#password");
        private By Login = By.CssSelector("input#login-button");



        private IElementServices _elementServices = new ElementHelpers();
        public LoginPage()
        {
            _elementServices = _elementServices ?? new ElementHelpers();
        }

        public IWebElement GetUserNamePO()
        {
            return _elementServices.WaitForVisibilityOfAnElementBy(UserName);
        }
        public IWebElement GetPasswordPO()
        {
            return _elementServices.WaitForVisibilityOfAnElementBy(Password);
        }
         public IWebElement GetloginBtnPO()
        {
            return _elementServices.WaitForVisibilityOfAnElementBy(Login);
        }

        public void EnterUserID(string text)
        {
            _elementServices.ClearSendText(GetUserNamePO(), text);
        }

        public void EnterPassword(string text)
        {
            _elementServices.ClearSendText(GetPasswordPO(), text);
        }

        public Inventory clickloginBtn()
        {
            _elementServices.WaitForAnElementClickable(GetloginBtnPO()).Click();
            return new Inventory();
        }



    }
}
