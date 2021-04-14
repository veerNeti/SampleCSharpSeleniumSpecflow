using CSharpUISelenium.DriverManager;
using CSharpUISelenium.ServiceImplimentation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;
using TestProject.Pages;

namespace TestProject.StepDefinition
{
    [Binding]
    public class Saucedemo_LoginSteps:Base
    {
        private IUtilServices utilServices = new Utils();
        private LoginPage loginPage = new LoginPage();

        public Saucedemo_LoginSteps()
        {
            utilServices = utilServices?? new Utils();
            loginPage = loginPage??new LoginPage();
        }
        [Given(@"I navigate to demo URL ""(.*)""")]
        public void GivenINavigateToDemoURL(string url)
        {
            utilServices.LauchApp(url);
        }

        [Given(@"Enter demo User ""(.*)""")]
        public void GivenEnterDemoUser(string userID)
        {
            loginPage.EnterUserID(userID);
        }

        [Given(@"Enter demo password ""(.*)""")]
        public void GivenEnterDemoPassword(string pass)
        {
            loginPage.EnterPassword(pass);
        }

        [When(@"I click on Login button")]
        public void WhenIClickOnLoginButton()
        {
            loginPage.clickloginBtn();
        }

        [Then(@"User ""(.*)"" be able to navigate to ""(.*)"" page")]
        public void ThenUserBeAbleToNavigateToPage(string check, string expectedUrl)
        {
            if (utilServices.StringValidation(check, "SHOULD", STRINGCHECK.EQL))
            {
                Assert.IsTrue(utilServices.StringValidation(driver.Url, expectedUrl, STRINGCHECK.CONT));
            }
            else
            {
                Assert.IsFalse(utilServices.StringValidation(driver.Url, expectedUrl, STRINGCHECK.CONT));

            }
        }



    }
    
}
