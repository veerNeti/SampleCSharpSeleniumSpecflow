using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpUISelenium.Services
{
    public interface IDriverService
    {
        IWebDriver ChromeDriver();
        IWebDriver DriverFactory(String browserName);


    }
}
