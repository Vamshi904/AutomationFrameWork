using AutomationFrameWork.Drivers;
using AutomationFrameWork.Pages;
using AutomationFrameWork.PagesAndObjects.CustomerTab;
using AutomationFrameWork.PagesAndObjects.System;
using AutomationFrameWork.Utilities;
using System.Threading.Tasks;
namespace AutomationFrameWork.Features.Customer.CustomersMenu
{
    [Binding]
    public class CustomersMenuSteps
    {
        private readonly TestContext _testContext;
        private readonly CustomerMenuPage CustomerMenuPage;
        private readonly CommonActionsPage commonactionpage;
        public CustomersMenuSteps(PlaywrightDriver playwrightDriver, ScenarioContext scenarioContext)
        {
            if (playwrightDriver == null) throw new ArgumentNullException(nameof(playwrightDriver));
            if (scenarioContext == null) throw new ArgumentNullException(nameof(scenarioContext));

            _testContext = new TestContext(playwrightDriver.Page, scenarioContext);
            CustomerMenuPage = new CustomerMenuPage(_testContext);
            commonactionpage = new CommonActionsPage(_testContext.Page);
        }

        [When("the user navigates to the  Customer tab")]
        public async Task WhenTheUserNavigatesToTheCustomerTab()
        {
            await CustomerMenuPage.Customers();

        }
        [When("the user clicks on the Add  Customer button")]
        public async Task WhenTheUserClicksOnTheAddCustomerButton()
        {
            await CustomerMenuPage.Addcustomer();
        }

        [When("fills in the  Customer Form with valid details")]
        public async Task WhenFillsInTheCustomerFormWithValidDetails()
        {
            await CustomerMenuPage.FillForms();
        }

        [When("Click on the Submit information button in  Customer Form")]
        public async Task WhenClickOnTheSubmitInformationButtonInCustomerForm()
        {
            await CustomerMenuPage.AddCustomerbutton();
        }

        [Then("Verify the confirmation message is displayed for new  Customer Form")]
        public async Task ThenVerifyTheConfirmationMessageIsDisplayedForNewCustomerFormAsync()
        {
            string successmessage = await commonactionpage.GetSuccessNotificationText();
            Console.WriteLine(successmessage);
            Assert.Contains("OTP sent successfully to your mobile number.", successmessage);
            await CustomerMenuPage.Back();


        }
        [When("the user clicks on the customer Name link")]
        public async Task  WhenTheUserClicksOnTheCustomerNameLink()
        {
            await CustomerMenuPage.Namelink();
        }

        [When("Edit in the  Customer Form with valid details")]
        public async Task WhenEditInTheCustomerFormWithValidDetails()
        {
            await CustomerMenuPage.UpdateForms();
        }

        [Then("Verify the confirmation message is displayed for Edit  Customer Form")]
        public async Task ThenVerifyTheConfirmationMessageIsDisplayedForEditCustomerForm()
        {
            string successmessage = await commonactionpage.GetSuccessNotificationText();
            Console.WriteLine(successmessage);
            Assert.Contains("OTP sent successfully to your mobile number.", successmessage);
            await CustomerMenuPage.Back();
        }


    }
}
