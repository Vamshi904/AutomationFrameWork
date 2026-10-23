using AutomationFrameWork.Drivers;
using AutomationFrameWork.Pages;
using AutomationFrameWork.PagesAndObjects.System;
using AutomationFrameWork.Utilities;
using System.Threading.Tasks;

namespace AutomationFrameWork.Features.System.AgentCommissionMenu;

[Binding]
public class AgentCommissionMenuSteps
{

    private readonly TestContext _testContext;
    private readonly AgentCommissionMenuPage AgentCommissionMenuPage;
    private readonly CommonActionsPage commonactionpage;

    public AgentCommissionMenuSteps(PlaywrightDriver playwrightDriver, ScenarioContext scenarioContext)
    {
        if (playwrightDriver == null) throw new ArgumentNullException(nameof(playwrightDriver));
        if (scenarioContext == null) throw new ArgumentNullException(nameof(scenarioContext));

        _testContext = new TestContext(playwrightDriver.Page, scenarioContext);
        AgentCommissionMenuPage = new AgentCommissionMenuPage(_testContext.Page);
        commonactionpage = new CommonActionsPage(_testContext.Page);
    }

    [When("Click on the Agent Commissions menu")]
    public async Task WhenClickOnTheAgentCommissionsMenu()
    {
        await AgentCommissionMenuPage.ClickAgentCommissionsMenu();
    }

    [When("the user clicks on the Add Agent Commission button")]
    public async Task WhenTheUserClicksOnTheAddAgentCommissionButton()
    {
        await AgentCommissionMenuPage.ClickAddAgentCommissionButton();
    }

    [When("fills in the Agent commission Form with valid details")]
    public async Task WhenFillsInTheAgentCommissionFormWithValidDetailsAsync()
    {
        await AgentCommissionMenuPage.FillAgentCommissionForm("ADDIS ABABA BOLE INTERNATIONAL AIRPORT", "Canada air cleared", "10", "20", "500", "10");
    }

    [When("Click on the Submit information button in Agent Commission Form")]
    public async Task WhenClickOnTheSubmitInformationButtonInAgentCommissionForm()
    {
        await AgentCommissionMenuPage.SubmitAgentCommissionForm();
    }

    [Then("Verify the confirmation message is displayed for new added agent commission")]
    public async Task ThenVerifyTheConfirmationMessageIsDisplayedForNewAddedAgentCommissionAsync()
    {
        String successmessage = await commonactionpage.GetSuccessNotificationText();

        Console.WriteLine(successmessage);

        Assert.Contains("Agent Commission details added successfully", successmessage);
    }

    //Edit Agent commision steps 
    [When("the user clicks on the Add Agent Name")]
    public async Task WhenTheUserClicksOnTheAddAgentNameAsync()
    {
        await AgentCommissionMenuPage.ClickAgentNameLink();
    }

    [When("Editing the Agent commission Form")]
    public async Task WhenEditingTheAgentCommissionForm()
    {
        var filePath = @"C:\Users\vannam\source\repos\AutomationFrameWork\AutomationFrameWork\TestData\AgentCommision.xlsx";
        // Only include date, no hours
        string safeDate = DateTime.Now.ToString("yyyyMMdd");
        string sheetName = $"AgentCommision{safeDate}";
        var rows = ExcelHelper.ReadExcel(filePath, sheetName);

        try
        {
            foreach (var row in rows)
            {
                await AgentCommissionMenuPage.FillAgentCommissionForm(
                    row["Agent"],
                    row["Courier Type"],
                    row["Range From"],
                    row["Range To"],
                    row["Commission"],
                    "12"
                );
            }                  
        }
        
        catch (Exception ex) { 
        
            Console.WriteLine(ex.Message);
            await AgentCommissionMenuPage.FillAgentCommissionForm("1000", "2000", "John Doe", "USD", "500", "10"); 
}

}
    [Then("Verify the confirmation message is displayed for Editing agent commission")]
    public async Task ThenVerifyTheConfirmationMessageIsDisplayedForEditingAgentCommissionAsync()
    {
        String successmessage = await commonactionpage.GetSuccessNotificationText();

        Console.WriteLine(successmessage);

        Assert.Contains("Dispatch location information has been successfully updated", successmessage);
    }

    //Delete functionality steps 
    [When("Click on the delete icon in List of Agent Commissions")]
    public async Task WhenClickOnTheDeleteIconInListOfAgentCommissions()
    {
        await AgentCommissionMenuPage.ClickDeleteIcon();

    }

    [Then("Verify the confirmation message is displayed for Delete agent Commission")]
    public async Task ThenVerifyTheConfirmationMessageIsDisplayedForDeleteAgentCommissionAsync()
    {
        string message = await CommonActionsPage.AcceptAlertAsync(_testContext.Page);
        // Verify the alert text
        Assert.Contains("Are you sure you want to delete this record?", message);

        String successmessage = await commonactionpage.GetSuccessNotificationText();

        Console.WriteLine(successmessage);

        Assert.Contains("Commission details deleted successfully", successmessage);
    }

    [When("Enter the name in the search Field.")]
    public async Task WhenEnterTheNameInTheSearchField_()
    {
        await AgentCommissionMenuPage.FillName();
    }

    [Then("print all the details in the  Agent Commision table")]
    public async Task ThenPrintAllTheDetailsInTheAgentCommisionTableAsync()
    {
        await commonactionpage.PrintTableRecords1();

        // Only include date, no hours
        string safeDate = DateTime.Now.ToString("yyyyMMdd");
        string sheetName = $"AgentCommision{safeDate}";

        await commonactionpage.PrintTableRecords(@"C:\Users\vannam\source\repos\AutomationFrameWork\AutomationFrameWork\TestData\AgentCommision.xlsx", sheetName);

    }

}
