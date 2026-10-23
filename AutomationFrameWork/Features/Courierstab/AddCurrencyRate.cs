using AutomationFrameWork.Drivers;
using AutomationFrameWork.Pages;
using AutomationFrameWork.PagesAndObjects.Couriers;
using AutomationFrameWork.Utilities;
using Microsoft.Playwright;


namespace AutomationFrameWork.Features.Courierstab
{
    [Binding]
    public class AddCurrencyRateSteps
    {
        private readonly TestContext _testContext;
        private readonly AddCurrencyRatePage _addCurrencyRatePage;
        private readonly CommonActionsPage commonactionpage;
        public AddCurrencyRateSteps(PlaywrightDriver playwrightDriver, ScenarioContext scenarioContext)
        {
            if (scenarioContext.TryGetValue("Page", out IPage page))
            {
                _testContext = new TestContext(page, scenarioContext);
            }
            else
            {
                _testContext = new TestContext(playwrightDriver.Page, scenarioContext);
            }

            _addCurrencyRatePage = new AddCurrencyRatePage(_testContext);
            commonactionpage = new CommonActionsPage(_testContext.Page);
        }

        [When(@"the user clicks on the Couriers tab")]
        public async Task WhenTheUserClicksOnTheCouriersTab()
        {
            await _addCurrencyRatePage.NavigateToCouriersTabAsync();
        }

        [When(@"the user clicks on the Rate menu")]
        public async Task WhenTheUserClicksOnTheRateMenu()
        {
            await _addCurrencyRatePage.ClickRateMenuAsync();
        }

        [When(@"the user clicks on the Add Rates button")]
        public async Task WhenTheUserClicksOnTheAddRatesButton()
        {
            await _addCurrencyRatePage.ClickAddRatesButtonAsync();
        }

        [When(@"the user enters valid details for Add New Currency Rate")]
        public async Task WhenTheUserEntersValidDetailsForAddNewCurrencyRate()
        {
            await _addCurrencyRatePage.FillCurrencyRateFormAsync(
                fromCurrency: "EURO",
                toCurrency: "Indian Rupee",
                exchangeRate: "5"
            );
        }


        [When(@"saves the new rate")]
        public async Task WhenSavesTheNewRate()
        {
            await _addCurrencyRatePage.ClickSaveRateButtonAsync();
        }

        [Then(@"the new currency rate should appear in the rate list")]
        public async Task ThenTheNewCurrencyRateShouldAppearInTheRateList()
        {
            Assert.True(await _addCurrencyRatePage.IsRatePresentAsync());
            String successmessage = await commonactionpage.GetSuccessNotificationText();
            Console.Write(successmessage);
            Assert.Contains("Rate added successfully!", successmessage);
        }
        [When(@"the user clicks on the customer tab")]
        public async Task WhenTheUserClicksOnTheCustomerTab()
        {
            await _addCurrencyRatePage.NavigateToCustomerTabAsync();
        }
        [When(@"selects the Active status from the dropdown")]
        public async Task WhenSelectsTheActiveStatusFromTheDropdown()
        {
            await _addCurrencyRatePage.SelectActiveStatusAsync();
        }
        [Then(@"print all the details in the table")]
        public async Task ThenPrintAllTheDetailsInTheTable()
        {
            await _addCurrencyRatePage.PrintCustomerTableAsync();
        }

        //For Edit functionality 
        [When(@"the user clicks on a username button")] 
        public async Task WhenTheUserClicksOnAUsernameButtonAsync()
        {
            await _addCurrencyRatePage.ClickUsernameButtonAsync(); ;
        }
        [When(@"the user edits the user details")] 
        public async Task WhenTheUserEditsTheUserDetails()
        {
            await _addCurrencyRatePage.EditCurrencyRateFormAsync(fromCurrency: "Franc BEAC",
                toCurrency: "Indian Rupee",
                exchangeRate: "20");
        }
        [When(@"the user clicks on the Update Information button")]  
        public async Task WhenTheUserClicksOnTheUpdateInformationButton() 
        { 
            await _addCurrencyRatePage.ClickUpdateButtonAsync(); 
        }

        [Then(@"the confirmation message should appear in the rate list")] 
        public async Task ThenTheConfirmationMessageShouldAppearInTheRateListAsync() 
        {
            Assert.True(await _addCurrencyRatePage.IsRatePresentAsync());


            String successmessage = await commonactionpage.GetSuccessNotificationText();
            Console.Write(successmessage);
            Assert.Contains("Rate updated successfully!", successmessage);


        }
    }
}
