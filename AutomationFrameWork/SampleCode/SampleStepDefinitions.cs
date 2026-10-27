using AutomationFrameWork.Drivers;
using AutomationFrameWork.Utilities;

namespace AutomationFrameWork.SampleCode
{
    [Binding]
    public class SampleStepDefinitions : BaseSteps
    {

        // Lazy init — only created when first step runs, by then Page is not null
        private SamplePageAction _SearchPageAction => new SamplePageAction(Context, _driver.Page);

        public SampleStepDefinitions(PlaywrightDriver playwrightDriver, ScenarioContext scenarioContext) : base(playwrightDriver, scenarioContext)
        {
        }

        [Given("I navigate to the application url")]
        public async Task GivenINavigateToTheApplicationUrlAsync()
        {
            var configReader = Context.ScenarioContext.Get<ConfigReader>("ConfigReader");
            await Context.CommonActions.NavigateToURLAsync(configReader.Urls.ScrubDb);
        }


    }
}
