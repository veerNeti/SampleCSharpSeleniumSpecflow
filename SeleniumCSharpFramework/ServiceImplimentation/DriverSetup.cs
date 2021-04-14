using CSharpUISelenium.DriverManager;
using CSharpUISelenium.Services;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace CSharpUISelenium.ServiceImplimentation
{
    public class DriverSetup : Base, IDriverService
    {
        private static IConfigurationRoot appSettingReader = new ConfigurationBuilder().AddJsonFile(@"appsettings.json", optional: true).Build();
        private static string _chromepath = Directory.GetCurrentDirectory() + "\\DriverBinary\\Chrome\\";
        private static string _logpath = Environment.CurrentDirectory + @"\chromedriver.log";
        private int MAXTIMEOUT = int.Parse(appSettingReader["Time-out"]);

        public IWebDriver ChromeDriver()
        {
            try
            {
                ChromeOptions options = new ChromeOptions();

                if (bool.Parse(appSettingReader["chrome-maximized"]))
                {
                    options.AddArguments("start-maximized");
                }
                if (bool.Parse(appSettingReader["chrome-incognito"]))
                {
                    options.AddArguments("--incognito");
                }
                if (bool.Parse(appSettingReader["chrome-headless"]))
                {
                    options.AddArguments("--headless");
                }
                if (bool.Parse(appSettingReader["chrome-devtools"]))
                {
                    options.AddArguments("--auto-open-devtools-for-tabs");
                }
                if (bool.Parse(appSettingReader["chrome-fullScreen"]))
                {
                    options.AddArguments("start-fullscreen");
                }
                if (bool.Parse(appSettingReader["chrome-setWindowSize"]))
                {
                    options.AddArguments("window-size=" + appSettingReader["chrome-width"] + appSettingReader["chrome-height"]);
                }
                if (bool.Parse(appSettingReader["chrome-logging"]))
                {
                    ChromePerformanceLoggingPreferences prefs = new ChromePerformanceLoggingPreferences();
                    prefs.IsCollectingNetworkEvents = true;
                }
                var service = ChromeDriverService.CreateDefaultService(_chromepath);
                service.LogPath = _logpath;
                service.EnableVerboseLogging = true;

                driver = new ChromeDriver(service, options);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(MAXTIMEOUT);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception starting chrome " + ex.Message);
            }
            return driver;
        }

        public IWebDriver DriverFactory(string browserName)
        {
            IWebDriver driver = null;
            switch (browserName.ToLower())
            {
                case "chrome":
                    driver =ChromeDriver();
                    break;
                case "firefox":

                    break;
                case "internet explorer":

                    break;
                case "microsoftedge":

                    break;
                case "safari":

                    break;
                default:
                    Console.WriteLine("Selected Browser " + browserName + " name is NOT supported in the Framework");

                    break;
            }
            return driver;
        }
    }
}
