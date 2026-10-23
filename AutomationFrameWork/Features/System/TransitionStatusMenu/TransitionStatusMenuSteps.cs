using AutomationFrameWork.Drivers;
using AutomationFrameWork.Pages;
using AutomationFrameWork.Utilities;
namespace AutomationFrameWork.Features.System.TransitionStatusMenu;

[Binding]
public class TransitionStatusMenuSteps
{

    private readonly TestContext _testContext;
    private readonly TransitionStatusPage transitionStatusPage;
    private readonly CommonActionsPage _commonActionsPage;

    public TransitionStatusMenuSteps(PlaywrightDriver playwrightDriver, ScenarioContext scenarioContext)
    {
        if (playwrightDriver == null) throw new ArgumentNullException(nameof(playwrightDriver));
        if (scenarioContext == null) throw new ArgumentNullException(nameof(scenarioContext));

        _testContext = new TestContext(playwrightDriver.Page, scenarioContext);
        transitionStatusPage = new TransitionStatusPage(_testContext);
        _commonActionsPage = new CommonActionsPage(_testContext.Page);
    }


    [When("Click on the Transition Status menu")]
    public async Task WhenClickOnTheTransitionStatusMenu()
    {
        await transitionStatusPage.ClickTransitionStatusMenuAsync();
    }

    [When("the user clicks on the Add New Status button")]
    public async Task WhenTheUserClicksOnTheAddNewStatusButton()
    {
        await transitionStatusPage.ClickAddNewStatusButtonAsync();
    }

    [When("fills in the Transition Status Form with valid details")]
    public async Task WhenFillsInTheTransitionStatusFormWithValidDetails()
    {
        await transitionStatusPage.FillTransitionStatusFormAsync("Item collected", "Corresponding Agent", "Y", "Y");
    }

    [When("Click on the Submit information button in Transition Status Form")]
    public async Task WhenClickOnTheSubmitInformationButtonInTransitionStatusForm()
    {
        await transitionStatusPage.ClickSubmitInformationButtonAsync();
    }

    [Then("Verify the confirmation message is displayed for new Transition Status Page")]
    public async Task ThenVerifyTheConfirmationMessageIsDisplayedForNewTransitionStatusPage()
    {
        string successMessage = await _commonActionsPage.GetSuccessNotificationText();
        Console.WriteLine(successMessage);
        Assert.Contains("Transition status added successfully", successMessage);
    }

    [When("Click on the Transition Status link")]
    public async Task WhenClickOnTheTransitionStatusLink()
    {
        await transitionStatusPage.ClickNameLinkAsync();
    }

    [When("Editing existing Transition Status Form with valid details")]
    public async Task WhenEditingExistingTransitionStatusFormWithValidDetails()
    {
        await transitionStatusPage.FillTransitionStatusFormAsync("Item collected", "Corresponding Agent", "Y", "Y");
    }

    [Then("Verify the confirmation message is displayed for Edit Transition Status Page")]
    public async Task ThenVerifyTheConfirmationMessageIsDisplayedForEditTransitionStatusPage()
    {
        string successMessage = await _commonActionsPage.GetSuccessNotificationText();
        Console.WriteLine(successMessage);
        Assert.Contains("Transition status updated successfully", successMessage);
    }

  



}
