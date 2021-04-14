using CSharpUISelenium.DriverManager;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace CSharpUISelenium.ServiceImplimentation
{
    public class Utils : Base, IUtilServices
    {
        static IConfigurationRoot cb = new ConfigurationBuilder().AddJsonFile(@"appsettings.json", optional: true).Build();
        private static int TIMEOUT = int.Parse(cb["Time-out"]);
        private static int RETRY = int.Parse(cb["RETRY"]);

        public Utils()
        {

        }

        public string createDirectory(string Path)
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
                Console.WriteLine("Creating directory:" + Path);
            }
            else
            {
                Console.WriteLine("File \"{0}\" already exists." + Path);
            }
            return Path;
        }

        public string GenerateOneRandomString()
        {

            StringBuilder strbuilder = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < 8; i++)
            {
                // Generate floating point numbers
                double myFloat = random.NextDouble();
                // Generate the char using below logic
                var myChar = Convert.ToChar(Convert.ToInt32(Math.Floor(25 * myFloat) + 65));
                strbuilder.Append(myChar);
            }
            return strbuilder.ToString();
        }

        public int GetOddNumberInRange(int start, int end)
        {
            int n = start;
            int result = 0;
            while (n <= end)
            {
                if (n % 2 != 0)
                {
                    result = n;
                }
                n++;
            }
            return result;
        }
        public bool StringValidation(string str1, string str2, STRINGCHECK str)
        {

            bool flg = false;
            if (!String.IsNullOrEmpty(str1) && !String.IsNullOrEmpty(str2))
            {
                switch (str)
                {
                    case STRINGCHECK.EQL:
                        flg = str1.ToLower().Trim().Equals(str2.ToLower().Trim());
                        break;
                    case STRINGCHECK.CONT:
                        flg = str1.ToLower().Trim().Contains(str2.ToLower().Trim());
                        break;
                }
            }
            Console.WriteLine("StringValidation:" + flg + "str1:" + str1 + "str2:" + str2);
            return flg;
        }

        public string GenerateRandomNumber(int start, int end)
        {
            Random r = new Random();
            string result = r.Next(start, end).ToString();
            return result;
        }

        public void LauchApp(string url)
        {
            driver.Navigate().GoToUrl(url);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TIMEOUT);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(TIMEOUT);
        }

        public string Screenshot(string fileName)
        {
            Screenshot screen = ((ITakesScreenshot)driver).GetScreenshot();
            string path = fileName+ ".png";
            screen.SaveAsFile(path);
            return path;
        }

            public string getTimeStampAsString()
        {
            return DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss");
        }


    }
}
