using AutomationFrameWork.Drivers;
using AutomationFrameWork.Pages;
using AutomationFrameWork.PagesAndObjects.System;
using AutomationFrameWork.Utilities;
using System.Linq;


namespace AutomationFrameWork.Features.System.AgentMenu;

[Binding]
public class AgentMenuSteps
{
    private readonly TestContext _testContext;
    private readonly AgentMenuPage agentMenuPage;
    private readonly CommonActionsPage commonactionpage;

    public AgentMenuSteps(PlaywrightDriver playwrightDriver, ScenarioContext scenarioContext)
    {
        if (playwrightDriver == null) throw new ArgumentNullException(nameof(playwrightDriver));
        if (scenarioContext == null) throw new ArgumentNullException(nameof(scenarioContext));

        _testContext = new TestContext(playwrightDriver.Page, scenarioContext);
        agentMenuPage = new AgentMenuPage(_testContext);
        commonactionpage = new CommonActionsPage(_testContext.Page);
    }

    [When("Click on the Agent User menu")]
    public async Task WhenClickOnTheAgentUserMenuAsync()
    {
        await agentMenuPage.ClickAgentsMenu();
    }

    [When("the user clicks on the Add Agent button")]
    public async Task WhenTheUserClicksOnTheAddAgentButtonAsync()
    {
        await agentMenuPage.ClickAddAgentButton();
    }
   // Excel reading purpose
    [When("fills in the Agent Form with valid details")]
    public async Task WhenFillsInTheAgentFormWithValidDetailsAsync()
    {
        var filePath = @"C:\Users\vannam\source\repos\AutomationFrameWork\AutomationFrameWork\TestData\TestData.xlsx";
        var rows = ExcelHelper.ReadExcel(filePath, "AgentData");

        try
        {
            foreach (var row in rows)
            {
                if (row.TryGetValue("Name", out var name) &&
                              row.TryGetValue("Email", out var email))
                {
                    await agentMenuPage.FillAgentForm(
                        name,
                        row["PrefixCode"],
                        row["Country"],
                        row["Currency"],
                        row["City"],
                        row["State"],
                        row["MobileCode"],
                        row["MobileNumber"],
                        email
                    );
                }
                else
                {
                    Console.WriteLine("Missing expected columns in Excel file.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await agentMenuPage.FillAgentForm("John Doe", "AG123", "INDIA", "Indian Rupee", "Hyderabad", "Telangana", "91", "9876543210", "vamshi@gmail.com");
        }
    }

    [When("Click on the Submit information button in Agent Form")]
    public async Task WhenClickOnTheSubmitInformationButtonInAgentFormAsync()
    {
        await agentMenuPage.SubmitAgentForm();
    }

    [Then("Verify the confirmation message is displayed for new added agent")]
    public async Task ThenVerifyTheConfirmationMessageIsDisplayedForNewAddedAgentAsync()
    {
        string successmessage = await commonactionpage.GetSuccessNotificationText();
        Console.WriteLine(successmessage);
        Assert.Contains("Agent information added successfully", successmessage);
    }

    [When("the user clicks on the Name link")]
    public async Task WhenTheUserClicksOnTheNameLinkAsync()
    {

        await agentMenuPage.ClickUsernamelink();
    }

    [When("Edit in the Agent Form with valid details")]
    public async Task WhenEditInTheAgentFormWithValidDetailsAsync()
    {
        await agentMenuPage.EditFillAgentForm("John Doe", "AG123", "INDIA", "Indian Rupee", "Hyderabad", "Telangana", "91", "9876543210", "vamshi@gmail.com");
    }
    [Then("Verify the confirmation message is displayed for Edit agent")]
    public async Task ThenVerifyTheConfirmationMessageIsDisplayedForEditAgentAsync()
    {
        string successmessage = await commonactionpage.GetSuccessNotificationText();
        Console.WriteLine(successmessage);
        Assert.Contains("Agent information updated successfully", successmessage);
    }



    [When("Click on the delete icon in Agent page")]
    public async Task WhenClickOnTheDeleteIconInAgentPage()
    {
        await agentMenuPage.DeleteAgent();
    }

    [Then("Verify the confirmation message is displayed for Delete agent")]
    public async Task ThenVerifyTheConfirmationMessageIsDisplayedForDeleteAgentAsync()
    {

        string message = await CommonActionsPage.AcceptAlertAsync(_testContext.Page);

        // Verify the alert text
        Assert.Contains("Are you sure you want to delete", message);

        String successmessage = await commonactionpage.GetSuccessNotificationText();
        Assert.Contains("User information deleted successfully", successmessage);
    }


    [Then("print all the details in the  Agent table")]
    public async Task ThenPrintAllTheDetailsInTheAgentTable()
    {

        await commonactionpage.PrintTableRecords1();

       

    }

   

}








