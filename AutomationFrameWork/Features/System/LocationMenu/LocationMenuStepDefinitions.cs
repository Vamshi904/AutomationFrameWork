using AutomationFrameWork.Drivers;
using AutomationFrameWork.Pages;
using AutomationFrameWork.PagesAndObjects.System;
using AutomationFrameWork.Utilities;
using Reqnroll;
using System;
using System.Threading.Tasks;

namespace AutomationFrameWork.Features.System.LocationMenu;

[Binding]
public class LocationMenuStepDefinitions
{
    private readonly TestContext _testContext;

    private readonly LocationPage locationPage;
    private readonly CommonActionsPage commonactionpage;
public LocationMenuStepDefinitions(PlaywrightDriver playwrightDriver, ScenarioContext scenarioContext)
    {
        if (playwrightDriver == null) throw new ArgumentNullException(nameof(playwrightDriver));
        if (scenarioContext == null) throw new ArgumentNullException(nameof(scenarioContext));

        // Pass scenarioContext to TestContext
        _testContext = new TestContext(playwrightDriver.Page, scenarioContext); // ScenarioContext (required)
        locationPage = new LocationPage(_testContext);
        commonactionpage = new CommonActionsPage(_testContext.Page);

    }



    [When("Click on the Location menu")]
    public async Task WhenClickOnTheLocationMenu()
    {
        await locationPage.ClickLocationMenu();
    }

    [When("click on the Add Location button")]
    public async Task WhenClickOnTheAddLocationButton()
    {
        await locationPage.ClickAddLocationButton();
    }

    [When("fills in the Location information with valid details")]
    public async Task WhenFillsInTheLocationInformationWithValidDetails()
    {
        await locationPage.FillLocationForm("Hyderabad", "123456", "SALABED KISENYI");
    }

    [When("Click on the Add Location")]
    public async Task WhenClickOnTheAddLocation()
    
    {
        await Task.Delay(2000);

        await locationPage.SubmitLocation();

    }

    [Then("the new Location should appear in the user list")]
    public async Task ThenTheNewLocationShouldAppearInTheUserListAsync()
    {

        String successmessage = await commonactionpage.GetSuccessNotificationText();
        Console.Write(successmessage);
        Assert.Contains("Consignment location information has been successfully added", successmessage);
    }


    [When("Click on the delete icon in List of Locations page")]
    public async Task  WhenClickOnTheDeleteIconInListOfLocationsPage()
    {
        await locationPage.ClickDeleteicon();
        await Task.Delay(2000);

        string message = await CommonActionsPage.AcceptAlertAsync(_testContext.Page);

        // Verify the alert text
        Assert.Contains("Do you wish to delete the selected record?", message);

    }



    //Export functionality steps 

    [When("selects a Location Name")]
    public async Task WhenSelectsALocationName()
    {
     await locationPage.ClickLocationName();
    }

    [When("the user clicks on the Export Excel functionality")]
    public async Task WhenTheUserClicksOnTheExportExcelFunctionality()
    {
        await locationPage.ClickExportExcelbutton();
    }

    [Then("verify the Excel file is downloaded")]
    public async Task ThenVerifyTheExcelFileIsDownloaded()
    {
        var download = await _testContext.Page.RunAndWaitForDownloadAsync(async () =>
        {
            await locationPage.ClickExportExcelbutton();
        });

        string downloadsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
        string filePath = Path.Combine(downloadsFolder, download.SuggestedFilename);

        await download.SaveAsAsync(filePath);

        Assert.True(File.Exists(filePath), "Excel file was not downloaded.");
        Assert.True(new FileInfo(filePath).Length > 0, "Downloaded file is empty.");
    }






}
