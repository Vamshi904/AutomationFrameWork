using AutomationFrameWork.Drivers;
using AutomationFrameWork.Pages;
using AutomationFrameWork.Utilities;

namespace AutomationFrameWork.SampleCode

{

    [Binding]

    public class LoginSteps : BaseSteps

    {
        private CommonActionsPage Page => new CommonActionsPage(Context.Page);       // Lazy init — only created when first step runs, by then Page is not null


        private LoginPage _loginPage => new LoginPage(Context, _driver.Page);
        public LoginSteps(PlaywrightDriver playwrightDriver, ScenarioContext scenarioContext) : base(playwrightDriver, scenarioContext)

        {


        }

        [Given("user lands on EMS application")]

        public async Task GivenINavigateToTheApplicationUrlAsync()

        {

            var configReader = Context.ScenarioContext.Get<ConfigReader>("ConfigReader");

            await Page.NavigateToURLAsync(configReader.Urls.EMS);


        }
        [Given("the user enters valid login credentials")]
        public async Task GivenTheUserEntersValidLoginCredentials()
        {
            await _loginPage.ClickonAdvanceandprocessedbuton();

            await _loginPage.EnterCredentials("mgr.dele", "Ems!Dev2026");
        }

        [Given("the user clicks the Sign In button")]
        public async Task GivenTheUserClicksTheSignInButton()
        {
            await _loginPage.ClickSignIn();
        }

        [When("Verify the page title is correct")]

        public async Task WhenVerifyPageTitle()

        {

            await _loginPage.VerifyPageTitle();

        }

        [Then("User logout the application")]

        public async Task ThenUserLogout()

        {

          //  await _loginPage.Logout();

        }


    }

}

