using AutomationFrameWork.Drivers;
using AutomationFrameWork.Pages;
using AutomationFrameWork.PagesAndObjects.System;
using AutomationFrameWork.Utilities;
using System.Threading.Tasks;

namespace AutomationFrameWork.Features.System.CurrenciesMenu;

[Binding]
public class CurrenciesMenuSteps
{
    private readonly TestContext _testContext;
    private readonly CurrenciesMenuPage currenciesMenuPage;
    private readonly CommonActionsPage commonactionpage;

    public CurrenciesMenuSteps(PlaywrightDriver playwrightDriver, ScenarioContext scenarioContext)
    {
        if (playwrightDriver == null) throw new ArgumentNullException(nameof(playwrightDriver));
        if (scenarioContext == null) throw new ArgumentNullException(nameof(scenarioContext));

        _testContext = new TestContext(playwrightDriver.Page, scenarioContext);
        currenciesMenuPage = new CurrenciesMenuPage(_testContext);
        commonactionpage = new CommonActionsPage(_testContext.Page);
    }

    //Adding currency steps 

    [When("Click on the Currencies menu")]
    public async Task WhenClickOnTheCurrenciesMenu()
    {
        await currenciesMenuPage.ClickCurrencyMenu();
    }

    [When("click on the Add Currency button")]
    public async Task WhenClickOnTheAddCurrencyButton()
    {
        await currenciesMenuPage.ClickAddCurrencyButton();
    }

    [When("fills in the  Currency Form with valid details")]
    public async Task WhenFillsInTheCurrencyFormWithValidDetails()
    {
        await currenciesMenuPage.FillCurrencyForm("Germany currency ", "GBP", "Germany", "Yes");
    }

    [When("Click on the Submit Information in Currency Form page.")]
    public async Task WhenClickOnTheSubmitInformationInCurrencyFormPage_()
    {
        await _testContext.Page.ReloadAsync();
        await currenciesMenuPage.FillCurrencyForm("Germany currency ", "GBP", "Germany", "Yes");

        await currenciesMenuPage.SubmitCurrencyForm();
    }

    [When("Verify the confirmation message is displayed for new added currency page")]
    public async Task WhenVerifyTheConfirmationMessageIsDisplayedForNewAddedCurrencyPageAsync()
    {

        string message = await CommonActionsPage.AcceptAlertAsync(_testContext.Page);
        Console.WriteLine(message);

        string successmessage = await commonactionpage.GetSuccessNotificationText();

        Console.WriteLine(successmessage);

        Assert.Contains("Currency added successfully", successmessage);
    }

    //Editing currency steps 
    [When("click on the Currency Name")]
    public async Task WhenClickOnTheCurrencyName()
    {
        await currenciesMenuPage.ClickCurrencyName();
    }

    [When("Editing existing Currency Form with valid details")]
    public async Task WhenEditingExistingCurrencyFormWithValidDetailsAsync()
    {
        var rows = ExcelHelper.ReadExcel("TestData.xlsx", "AgentData");
        

        await currenciesMenuPage.FillCurrencyForm("Germany currency ", "GBP", "Germany", "Yes");
    }

    [When("Verify the confirmation message is displayed")]
    public async Task WhenVerifyTheConfirmationMessageIsDisplayedAsync()
    {
        string successmessage = await commonactionpage.GetSuccessNotificationText();

        Console.WriteLine(successmessage);

        Assert.Contains("\"Currency details updated successfully\"", successmessage);
    }

    [When("Fill the search Name")]
    public async Task WhenFillTheSearchName()
    {
        await currenciesMenuPage.SearchName();
    }

    [Then("print all the details in the Currencies table")]
    public async Task ThenPrintAllTheDetailsInTheCurrenciesTable()
    {
        await commonactionpage.PrintTableRecords(@"C:\AutomationFrameWork\TestData\AgentCommission.xlsx", "AgentCommission");
    }


}
