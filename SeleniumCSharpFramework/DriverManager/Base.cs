using OpenQA.Selenium;
using Protractor;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpUISelenium.DriverManager
{
    public abstract class Base
    {
        public static IWebDriver driver;
        public static NgWebDriver ngDriver;
    }
}
