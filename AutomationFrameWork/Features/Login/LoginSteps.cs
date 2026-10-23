using static Microsoft.Playwright.Assertions;
using AutomationFrameWork.Drivers;
using AutomationFrameWork.Utilities;
using AutomationFrameWork.PagesAndObjects.Login;

namespace AutomationFrameWork.StepDefinitions
{
    [Binding]
    public class LoginSteps
    {
        private readonly TestContext _testContext;
        private readonly LoginPage _loginPage;

        public LoginSteps(PlaywrightDriver playwrightDriver, ScenarioContext scenarioContext)  
        {
            if (playwrightDriver == null) throw new ArgumentNullException(nameof(playwrightDriver));
            if (scenarioContext == null) throw new ArgumentNullException(nameof(scenarioContext));

            // Pass scenarioContext to TestContext
            _testContext = new TestContext(playwrightDriver.Page, scenarioContext); // ScenarioContext (required)

            _loginPage = new LoginPage(_testContext);
        }


        [Given(@"user is on login page")]
        public async Task GivenUserIsOnLoginPageAsync()
        {
            var configReader = _testContext.ScenarioContext.Get<ConfigReader>("ConfigReader");
            await _loginPage.NavigateAsync(configReader.Urls.SauceDemo);
        }

        [When(@"user logs in with valid credentials")]
        public async Task WhenUserLogsInWithValidCredentialsAsync()
        {
            var configReader = _testContext.ScenarioContext.Get<ConfigReader>("ConfigReader");
            await _loginPage.LoginAsync(
                configReader.Username,
                configReader.Password
            );
          _testContext.ScenarioContext["Page"] = _testContext.Page;
        }

        [Then(@"user should be logged in successfully")]
        public async Task ThenUserShouldBeLoggedInSuccessfullyAsync()
        {
            var pageTitle = await _testContext.Page.TitleAsync();
            Console.WriteLine("Page title: " + pageTitle);
            await Task.Delay(4000);


            // Expect(_testContext.Page)
                //.ToHaveTitleAsync("Dashboard");
        }

        [Then(@"user must logout on clicking the logout button")]
        public async Task ThenUserMustLogoutOnClickingTheLogoutButtonAsync()
        {
            await Task.Delay(4000);

            await _loginPage.LogoutAsync();

        }


    }
}
