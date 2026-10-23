using AutomationFrameWork.Drivers;

using AutomationFrameWork.Pages;

using AutomationFrameWork.PagesAndObjects.System;

using AutomationFrameWork.Utilities;

using System.Threading.Tasks;

namespace AutomationFrameWork.Features.System.DispatchLocationMenu;


[Binding]

public class DispatchLocationMenuStepDefinitions

{

    private readonly TestContext _testContext;

    private readonly DispatchLocationPage dispatchLocationPage;

    private readonly CommonActionsPage commonactionpage;

    public DispatchLocationMenuStepDefinitions(PlaywrightDriver playwrightDriver, ScenarioContext scenarioContext)

    {

        if (playwrightDriver == null) throw new ArgumentNullException(nameof(playwrightDriver));

        if (scenarioContext == null) throw new ArgumentNullException(nameof(scenarioContext));



        // Pass scenarioContext to TestContext

        _testContext = new TestContext(playwrightDriver.Page, scenarioContext); // ScenarioContext (required)

        dispatchLocationPage = new DispatchLocationPage(_testContext);

        commonactionpage = new CommonActionsPage(_testContext.Page);


    }


    //Add new Dispatch location steps

    [When("Click on the Dispatch Locations  menu")]

    public async Task WhenClickOnTheDispatchLocationsMenu()

    {

        await dispatchLocationPage.ClickDispatchLocationsMenu();

    }



    [When("click on the Add Dispatch Location button")]

    public async Task WhenClickOnTheAddDispatchLocationButton()

    {

        await dispatchLocationPage.ClickAddDispatchLocationButton();

    }



    [When("fills in the Dispatch Location information with valid details")]

    public async Task WhenFillsInTheDispatchLocationInformationWithValidDetailsAsync()

    {
        await dispatchLocationPage.FillDispatchLocationForm("Hyderbad ", "SALABED KISENYI");

    }



    [When("Click on the Submit Information")]

    public async Task WhenClickOnTheSubmitInformation()

    {
        await dispatchLocationPage.SubmitDispatchLocation();
    }



    [Then("the Dispatch Location should appear in the user list")]

    public async Task ThenTheDispatchLocationShouldAppearInTheUserListAsync()

    {

        String successmessage = await commonactionpage.GetSuccessNotificationText();

        Console.WriteLine(successmessage);

        Assert.Contains("Dispatch location added successfully", successmessage);

    }


    [When("clicks on Dispatch Location Name")]

    public async Task WhenClicksOnDispatchLocationName()

    {
        await dispatchLocationPage.ClickDispatchLocationName();

    }



    [When("updates the Edit dispatch location details with valid details")]

    public async Task WhenUpdatesTheEditDispatchLocationDetailsWithValidDetails()

    {
        await dispatchLocationPage.EditDispatchLocationDetails("Kamareddy", "Namanve warehouse");

    }



    [When("clicks on the Submit Information")]

    public async Task WhenClicksOnTheSubmitInformationAsync()

    {
        await dispatchLocationPage.SubmitDispatchLocation();

    }



    [Then("confirmation message is displayed")]

    public async Task ThenConfirmationMessageIsDisplayedAsync()

    {

        String successmessage = await commonactionpage.GetSuccessNotificationText();

        Console.WriteLine(successmessage);

        Assert.Contains("Dispatch location information has been successfully updated", successmessage);

    }





    [When("Click on the delete icon inList of Dispatch Locations")]

    public async Task WhenClickOnTheDeleteIconInListOfDispatchLocations()

    {
        await dispatchLocationPage.ClickDeleteicon();

    }
}

