using AventStack.ExtentReports;
using Microsoft.Extensions.Configuration;
using AutomationFrameWork.Drivers;
using AutomationFrameWork.Pages;
using AutomationFrameWork.Utilities;
using System.Text;

namespace AutomationFrameWork.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly PlaywrightDriver _playwrightDriver;
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;

        private static ExtentReports _extentReports;
        private static ExtentTest _extentFeature;   // shared across scenarios, one per feature.
        private ExtentTest _extentScenario;         // unique for each scenario instance.

        private static int _executedScenarioCount = 0;
        private static String? _testRunException;


        public Hooks(PlaywrightDriver playwrightDriver, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _playwrightDriver = playwrightDriver ?? throw new ArgumentNullException(nameof(playwrightDriver));
            _scenarioContext = scenarioContext ?? throw new ArgumentNullException(nameof(scenarioContext));
            _featureContext = featureContext ?? throw new ArgumentNullException(nameof(featureContext));
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            _extentReports = ExtentReportManager.GetExtentReport();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            if (_testRunException!=null)
            {
                CreateNoExecutionReport(_testRunException);
            }
            else
            {
                _extentReports.Flush();
            }
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            var feature = _extentReports.CreateTest(featureContext.FeatureInfo.Title);
            featureContext["ExtentFeature"] = feature;
        }

        [BeforeScenario]
        public async Task BeforeScenarioAsync()
        {
            try
            {
                LoadConfiguration();

                Interlocked.Increment(ref _executedScenarioCount);

                _extentFeature = _featureContext.Get<ExtentTest>("ExtentFeature");
                _extentScenario = _extentFeature.CreateNode(_scenarioContext.ScenarioInfo.Title);

                ExtentTestManager.Test = _extentScenario;
                _scenarioContext["ExtentScenario"] = _extentScenario;

                await _playwrightDriver.InitializeAsync();
            }
            catch (Exception ex)
            {
                _testRunException = ex.Message;

                // 🔴 Mark scenario as FAILED in Extent
                _extentScenario?.Fail(
                    "<b>Scenario setup failed.</b><br>" +
                    $"<b>Message:</b> {ex.Message}"
                );

                // 🔴 Also fail feature explicitly
                _extentFeature?.Fail("Feature failed during scenario setup.");

                // 🔴 Log to console (CI visibility)
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Scenario setup failed:");
                Console.WriteLine(ex);
                Console.ResetColor();

                // 🔴 THIS IS CRITICAL
                throw; // stop execution, mark test as failed
            }
        }

        [AfterScenario]
        public async Task AfterScenarioAsync()
        {
            await _playwrightDriver.CleanupAsync();
        }

        [AfterStep]
        public async Task AfterStep(ScenarioContext scenarioContext)
        {
            var stepText = scenarioContext.StepContext.StepInfo.Text;
            var stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType;

            // Try to get from scenario context first, then fall back to ExtentTestManager
            ExtentTest extentScenario;

            if (scenarioContext.TryGetValue("ExtentScenario", out ExtentTest storedScenario))
            {
                extentScenario = storedScenario;
            }
            else
            {
                extentScenario = ExtentTestManager.Test;
            }

            if (extentScenario == null)
                return;

            if (scenarioContext.TestError == null)
            {
                extentScenario.Pass($"{stepType} {stepText}");
            }
            else
            {
                var base64Screenshot = await CaptureScreenshotAsBase64Async();
                if (!string.IsNullOrEmpty(base64Screenshot))
                {
                    extentScenario.Fail($"{stepType} {stepText} " +
                    $"<br>* Reason: {_scenarioContext.TestError.Message} " +
                    $"<br>* Failure Screenshot : ", MediaEntityBuilder
                            .CreateScreenCaptureFromBase64String(base64Screenshot)
                            .Build());
                }
            }
        }


        private void LoadConfiguration()
        {
            var projectRoot = CommonActionsPage.GetProjectRoot();
            Console.WriteLine("Project Root : " + projectRoot);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(projectRoot)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var configReader = configuration.GetSection("Credentials").Get<ConfigReader>();
            var urls = configuration.GetSection("Urls").Get<UrlSettings>();
            var automationSettings = configuration.GetSection("AutomationSettings").Get<AutomationSettings>();

            configReader.Password = DecodeBase64(configReader.Password);
            configReader.Urls = urls;
            configReader.AutomationSettings = automationSettings;

            _scenarioContext["ConfigReader"] = configReader;
        }

        private static string DecodeBase64(string base64Text)
        {
            var bytes = Convert.FromBase64String(base64Text);
            var decodedValue = Encoding.UTF8.GetString(bytes);

            return decodedValue;
        }

        private async Task<string?> CaptureScreenshotAsBase64Async()
        {
            try
            {
                var screenshotBytes = await _playwrightDriver.Page.ScreenshotAsync(new()
                {
                    FullPage = true
                });

                return Convert.ToBase64String(screenshotBytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Base64 screenshot capture failed: " + ex.Message);
                return null;
            }
        }

        private static void CreateNoExecutionReport(string exception)
        {
            var extent = ExtentReportManager.GetExtentReport();
            var test = extent.CreateTest("Test Execution Status");

            test.Fail(
                "<b>No scenarios were executed in this test run.</b><br>" +
                "<span style='color:red;'><b>Execution failed due to error:</b></span><br>" +
                $"<b>Message:</b> {exception}<br>"
            );

            extent.Flush();
        }



    }
}
