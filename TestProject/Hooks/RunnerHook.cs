using CSharpUISelenium.DriverManager;
using CSharpUISelenium.ServiceImplimentation;
using CSharpUISelenium.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.IO;
using TechTalk.SpecFlow;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using System.Reflection;

namespace TestProject.Hooks
{
    [Binding]
    public class RunnerHook : Base
    {
        private FeatureContext _featureContext;
        private ScenarioContext _scenarioContext;
        private static IUtilServices _utils = new Utils();
        private IDriverService _driverService;

        private static IConfigurationRoot appSettingReader = new ConfigurationBuilder().AddJsonFile(@"appsettings.json", optional: true).Build();
        string browserName = appSettingReader["browserName"];
        private static string screenshotsPath = Directory.GetCurrentDirectory() + "\\Extent\\screenshots\\";

        //Extent Report init
        protected static AventStack.ExtentReports.ExtentReports extentReport = new AventStack.ExtentReports.ExtentReports();
        private ExtentTest featureName;
        private ExtentTest scenario;
        private static string ExtentPath = Directory.GetCurrentDirectory() + "\\Extent\\";

        public RunnerHook(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            _featureContext = featureContext;
            _scenarioContext = scenarioContext;
            _utils = _utils ?? new Utils();
           
        }



        [BeforeTestRun]
        public static void TestInitalize()
        {
            string filePath = _utils.createDirectory(ExtentPath);
            var fileName = MethodBase.GetCurrentMethod().DeclaringType.ToString() + "_" + _utils.getTimeStampAsString() + ".html";
            ExtentV3HtmlReporter htmlReporter = new ExtentV3HtmlReporter(filePath + fileName);
            extentReport.AttachReporter(htmlReporter);
        }
        [BeforeScenario]
        public void BeforeScen(ScenarioContext scenarioContext)
        {
            //log for test output
            Console.WriteLine("Starting scenarioContext" + scenarioContext.ScenarioInfo.Title);

            //start driver
            _driverService = _driverService ?? new DriverSetup();
            driver = _driverService.DriverFactory(browserName);


            //Get feature Name
            featureName = extentReport.CreateTest<Feature>(_featureContext.FeatureInfo.Title);

            //Create dynamic scenario name
            scenario = featureName.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
        }

        [AfterStep]
        public void AfterEachStep()
        {
            var stepName = _scenarioContext.StepContext.StepInfo.Text;
            var featureName = _featureContext.FeatureInfo.Title;
            var scenarioName = _scenarioContext.ScenarioInfo.Title;
            var stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();

            if (_scenarioContext.TestError == null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "When")
                    scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "And")
                    scenario.CreateNode<And>(_scenarioContext.StepContext.StepInfo.Text);
            }
            else if (_scenarioContext.TestError != null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.InnerException);
                else if (stepType == "When")
                    scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.InnerException);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
            }

        }

        [AfterScenario]
        public void AfterScen(ScenarioContext scenarioContext)
        {
            Console.WriteLine("AfterScenario Context" + scenarioContext.ScenarioInfo.Title);

            //log and take screenshot for the failed scenarios
            if (_scenarioContext.TestError != null)
            {
                //create folder for screenshots
                string capture = _utils.Screenshot(_utils.createDirectory(screenshotsPath) + _scenarioContext.ScenarioInfo.Title + "_" + _utils.getTimeStampAsString());

                var error = _scenarioContext.TestError;
                Console.WriteLine("It was of type:" + error.GetType().Name);
                Console.WriteLine("An error ocurred:" + error.Message);

                scenario.Fail(error.Message).AddScreenCaptureFromPath(capture);
            }
            //close the tab and quit the browser after every scenario
            Base.driver.Close();
            Base.driver.Quit();
        }
        [AfterTestRun]
        public static void TearDownReport()
        {
            //Flush report once test completes
            extentReport.Flush();

        }

    }

}
