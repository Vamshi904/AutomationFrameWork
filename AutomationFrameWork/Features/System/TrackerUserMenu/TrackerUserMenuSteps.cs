using AutomationFrameWork.Drivers;
using AutomationFrameWork.Pages;
using AutomationFrameWork.PagesAndObjects.System;
using AutomationFrameWork.Utilities;
using Xunit; // for Assert

namespace AutomationFrameWork.Features.System.TrackerUserMenu;

[Binding]
public class TrackerUserMenuSteps
{
    private readonly TestContext _testContext;
    private readonly TrackerUserMenuPage _trackerUserMenuPage;
    private readonly CommonActionsPage _commonActionsPage;

    public TrackerUserMenuSteps(PlaywrightDriver playwrightDriver, ScenarioContext scenarioContext)
    {
        if (playwrightDriver == null) throw new ArgumentNullException(nameof(playwrightDriver));
        if (scenarioContext == null) throw new ArgumentNullException(nameof(scenarioContext));

        _testContext = new TestContext(playwrightDriver.Page, scenarioContext);
        _trackerUserMenuPage = new TrackerUserMenuPage(_testContext);
        _commonActionsPage = new CommonActionsPage(_testContext.Page);
    }

    [When("Click on the Tracker User menu")]
    public async Task WhenClickOnTheTrackerUserMenuAsync()
    {
        await _trackerUserMenuPage.ClickTrackerUserMenu();
    }

    [When("the user clicks on the Add Tracker User button")]
    public async Task WhenTheUserClicksOnTheAddTrackerUserButtonAsync()
    {
        await _trackerUserMenuPage.ClickAddTrackerUserAsync();
    }

    [When("fills in the Track User Form with valid details")]
    public async Task WhenFillsInTheTrackUserFormWithValidDetailsAsync()
    {
        await _trackerUserMenuPage.FillTrackerUserFormAsync("vamshi", "vamshigoud", "Tech@1234", "Jambo Cargo");
    }

    [When("Click on the Submit information button")]
    public async Task WhenClickOnTheSubmitInformationButtonAsync()
    {
        await _trackerUserMenuPage.SubmitInformationAsync();
    }

    [Then("Verify the confirmation message is displayed")]
    public async Task ThenVerifyTheConfirmationMessageIsDisplayedAsync()
    {
        string successMessage = await _commonActionsPage.GetSuccessNotificationText();
        Console.WriteLine(successMessage);
        Assert.Contains("Track User information added successfully", successMessage);
    }

    // Edit Steps
    [When("Click on the Name link")]
    public async Task WhenClickOnTheNameLinkAsync()
    {
        await _trackerUserMenuPage.ClickNamelink();
    }

    [When("Editing the existing the details")]
    public async Task WhenEditingTheExistingTheDetailsAsync()
    {
        await _trackerUserMenuPage.EditTrackerUserFormAsync("UpdatedName", "updated.email@example.com", "UpdatedPass123", "ITEM IS READY FOR COLLECTION AT SALABED KISENYI WAREHOUSE");
    }

    [Then("Verify the Update confirmation message is displayed")]
    public async Task ThenVerifyTheUpdateConfirmationMessageIsDisplayedAsync()
    {
        string successMessage = await _commonActionsPage.GetSuccessNotificationText();
        Console.WriteLine(successMessage);
        Assert.Contains("Track User information record updated", successMessage);
    }

    [When("Fill the search Name in tracker user")]
    public async Task WhenFillTheSearchNameInTrackerUser()
    {
        await _trackerUserMenuPage.SearchField();
    }

    [Then("print all the details in the tracker user table")]
    public async Task ThenPrintAllTheDetailsInTheTrackerUserTable()
    {
        string safeDate = DateTime.Now.ToString("yyyyMMdd");
        string sheetName = $"TrackerUserMenu{safeDate}";

        await _commonActionsPage.PrintTableRecords(@"C:\Users\vannam\source\repos\AutomationFrameWork\AutomationFrameWork\TestData\TrackerUserMenu.xlsx", sheetName);
    }

}
