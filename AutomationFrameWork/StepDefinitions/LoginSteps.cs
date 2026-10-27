using AutomationFrameWork.Drivers;
using AutomationFrameWork.Pages;
using AutomationFrameWork.Utilities;

namespace AutomationFrameWork.SampleCode

{

    [Binding]

    public class LoginSteps : BaseSteps

    {
        private CommonActionsPage Page => new CommonActionsPage(Context.Page);       // Lazy init — only created when first step runs, by then Page is not null

        //private SamplePageAction _SearchPageAction => new SamplePageAction(Context, _driver.Page);

        private LoginPage _loginPage => new LoginPage(Context, _driver.Page);
        public LoginSteps(PlaywrightDriver playwrightDriver, ScenarioContext scenarioContext) : base(playwrightDriver, scenarioContext)

        {


        }

        [Given("user lands on ScrubDb application")]

        public async Task GivenINavigateToTheApplicationUrlAsync()

        {

            var configReader = Context.ScenarioContext.Get<ConfigReader>("ConfigReader");

            await Page.NavigateToURLAsync(configReader.Urls.ScrubDb);

        }

        [When("Verify the page title is correct")]

        public async Task WhenVerifyPageTitle()

        {

            await _loginPage.VerifyPageTitle();

        }

        [Then("User logout the application")]

        public async Task ThenUserLogout()

        {

            await _loginPage.Logout();

        }


    }

}

