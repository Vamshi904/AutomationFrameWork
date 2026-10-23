using AutomationFrameWork.Drivers;
using AutomationFrameWork.Pages;
using AutomationFrameWork.PagesAndObjects.System;
using AutomationFrameWork.PagesAndObjects.System.TransitionPointMenu;
using AutomationFrameWork.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFrameWork;

[Binding]
public class TransitionPointMenuSteps
{

    private readonly TestContext _testContext;

    private readonly TransitionPointPage TransitionPointPage;
    private readonly CommonActionsPage commonactionpage;
    public TransitionPointMenuSteps(PlaywrightDriver playwrightDriver, ScenarioContext scenarioContext)
    {
        if (playwrightDriver == null) throw new ArgumentNullException(nameof(playwrightDriver));
        if (scenarioContext == null) throw new ArgumentNullException(nameof(scenarioContext));

        // Pass scenarioContext to TestContext
        _testContext = new TestContext(playwrightDriver.Page, scenarioContext); // ScenarioContext (required)
        TransitionPointPage = new TransitionPointPage(_testContext);
        commonactionpage = new CommonActionsPage(_testContext.Page);

    }

    [When("Click on the Transition Point menu")]
    public async Task WhenClickOnTheTransitionPointMenu()
    {
        await TransitionPointPage.ClickTransitionPointMenu();
    }

    [When("click on the Add new Transition Point button")]
    public async Task WhenClickOnTheAddNewTransitionPointButton()
    {
        await TransitionPointPage.ClickNewTransitionbutton();
    }

    [When("fills in the Transition Point form with {string} {string} {string}  {string}")]
    public async Task WhenFillsInTheTransitionPointFormWith(string hyderabad, string useroffice, string usertype, string p3)
    {
        await TransitionPointPage.FilltheForms(hyderabad, usertype, useroffice, p3);
    }

    [When("Click on the submit information Location")]
    public async Task WhenClickOnTheSubmitInformationLocation()
    {
        await TransitionPointPage.ClickSubmitbutton();
    }

    [Then("the new Transition Point should appear in the user list")]
    public async Task ThenTheNewTransitionPointShouldAppearInTheUserList()
    {
        string successMessage = await commonactionpage.GetSuccessNotificationText();
        Console.WriteLine(successMessage);
        Assert.Contains("Transition point added successfully", successMessage);
    }

    [When("the user clicks on the  Transition point Name  link")]
    public async Task WhenTheUserClicksOnTheTransitionPointNameLink()
    {
        await TransitionPointPage.ClickTransitionPointNamelink();
    }

    [Then("the Edit Transition Point should appear in the user list")]
    public async Task ThenTheEditTransitionPointShouldAppearInTheUserList()
    {
        string successMessage = await commonactionpage.GetSuccessNotificationText();
        Console.WriteLine(successMessage);
        Assert.Contains("Transition point information has been successfully updated", successMessage);
    }

    [When("Enter the name in the search Field in list of transition points")]
    public async Task WhenEnterTheNameInTheSearchFieldInListOfTransitionPoints()
    {
        await TransitionPointPage.SearchField("Hyderabad");
    }

    [Then("print all the details in the Transition Point table")]
    public async Task ThenPrintAllTheDetailsInTheTransitionPointTableAsync()
    {
        await commonactionpage.PrintTableRecords(@"C:\AutomationFrameWork\TestData\TransitionPoint.xlsx", "TransitionPoint");
    }

    [When("Enter {string} in the search Field in list of transition points")]
    public async Task WhenEnterInTheSearchFieldInListOfTransitionPointsAsync(string TransitionName)
    {
        await TransitionPointPage.SearchField(TransitionName);
    }
}
