using AutomationFrameWork.Drivers;
using AutomationFrameWork.Pages;
using AutomationFrameWork.PagesAndObjects.System;

using AutomationFrameWork.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFrameWork.Features.System.CountriesMenu;

[Binding]
public class CountriesMenuSteps
{
    private readonly TestContext _testContext;
    private readonly CountriesPage CountriesPage;
    private readonly CommonActionsPage commonactionpage;

    public CountriesMenuSteps(PlaywrightDriver playwrightDriver, ScenarioContext scenarioContext)
    {
        if (playwrightDriver == null) throw new ArgumentNullException(nameof(playwrightDriver));
        if (scenarioContext == null) throw new ArgumentNullException(nameof(scenarioContext));

        _testContext = new TestContext(playwrightDriver.Page, scenarioContext);
        CountriesPage = new CountriesPage(_testContext);
        commonactionpage = new CommonActionsPage(_testContext.Page);
    }


    [When("Click on the Countries menu")]
    public async Task WhenClickOnTheCountriesMenu()
    {
        await CountriesPage.ClickCountryMenu();
    }

    [When("the user clicks on the Add Country button")]
    public async Task WhenTheUserClicksOnTheAddCountryButton()
    {
        await CountriesPage.ClickAddCountryButton();
    }

    [When("fills in the Country Form with valid details")]
    public async Task WhenFillsInTheCountryFormWithValidDetails()
    {
        await CountriesPage.FillCountryForm("India", "91", "Kgs", "Queen Beatrix International Airport");
    }

    [When("Click on the Submit information button in Country Form")]
    public async Task WhenClickOnTheSubmitInformationButtonInCountryForm()
    {
        await CountriesPage.SubmitCountry();
    }

    [Then("Verify the confirmation message is displayed for new Country Form")]
    public async Task ThenVerifyTheConfirmationMessageIsDisplayedForNewCountryFormAsync()
    {
        string successmessage = await commonactionpage.GetSuccessNotificationText();
        Console.WriteLine(successmessage);
        Assert.Contains("Country added successfully", successmessage);
    }

    [When("the user clicks on the Country Name link")]
    public async Task WhenTheUserClicksOnTheCountryNameLink()
    {
        await CountriesPage.ClickCountryNameLink();
    }

    [When("Edit in the Country Form with valid details")]
    public async Task WhenEditInTheCountryFormWithValidDetailsAsync()
    {
        await CountriesPage.FillCountryForm("India", "91", "Kgs", "Queen Beatrix International Airport");
    }

    [Then("Verify the confirmation message is displayed for Edit Country Form")]
    public async Task ThenVerifyTheConfirmationMessageIsDisplayedForEditCountryFormAsync()
    {
        string successmessage = await commonactionpage.GetSuccessNotificationText();
        Console.WriteLine(successmessage);
        Assert.Contains("Country details updated successfully", successmessage);
    }

    [Then("print all the details in the  Countries table")]
    public async Task ThenPrintAllTheDetailsInTheCountriesTableAsync()
    {
        //  await commonactionpage.PrintTableRecords1();
        await commonactionpage.PrintTableRecords(@"C:\AutomationFrameWork\TestData\Countries.xlsx", "Countries");


    }



}
