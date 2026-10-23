using AutomationFrameWork.Drivers;
using AutomationFrameWork.Utilities;
using ReqnRoll_Project_Demo.Pages;

namespace ReqnRoll_Project_Demo.StepDefinitions
{
    [Binding]
    public class AddToCartSteps
    {
        private readonly TestContext _testContext;
        private readonly AddToCartPage _addToCartPage; // Points to the class which holds the methods that perform the actual actions.

        public AddToCartSteps(PlaywrightDriver playwrightDriver, ScenarioContext scenarioContext) 
        {
            if (playwrightDriver == null) throw new ArgumentNullException(nameof(playwrightDriver));
            if (scenarioContext == null) throw new ArgumentNullException(nameof(scenarioContext));

            // Pass scenarioContext to TestContext
            _testContext = new TestContext(playwrightDriver.Page,scenarioContext);     // ScenarioContext (required)

            _addToCartPage = new AddToCartPage(_testContext);
        }

        [Then("validate add to cart functionality")]
        public async Task ThenValidateAddToCartFunctionality()
        {
            await _addToCartPage.AddtoCartAsync();
        }
    }
}
