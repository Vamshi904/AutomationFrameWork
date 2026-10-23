using AutomationFrameWork.Drivers;
using AutomationFrameWork.Pages;
using AutomationFrameWork.PagesAndObjects.Couriers;
using AutomationFrameWork.PagesAndObjects.Login;
using AutomationFrameWork.PagesAndObjects.System;
using AutomationFrameWork.Utilities;
using Microsoft.Playwright;
using System.Linq;
using System.Threading.Tasks;

namespace AutomationFrameWork.Features.System
{
    [Binding]
    public class AddUserSteps
    {
        private readonly TestContext _testContext;
        private readonly LoginPage _loginPage;
        private readonly AddUserPage _addUserPage;
        private readonly CommonActionsPage commonactionpage;

        public AddUserSteps(PlaywrightDriver playwrightDriver, ScenarioContext scenarioContext)
        {
            if (scenarioContext.TryGetValue("Page", out IPage page))
            {
                _testContext = new TestContext(page, scenarioContext);
            }
            else
            {
                _testContext = new TestContext(playwrightDriver.Page, scenarioContext);
            }

            _loginPage = new LoginPage(_testContext);
            _addUserPage = new AddUserPage(_testContext);
            commonactionpage = new CommonActionsPage(page);

        }


        [When(@"the user navigates to the System tab")]
        public async Task WhenTheUserNavigatesToTheSystemTab()
        {
            await Task.Delay(5000);

            await _addUserPage.NavigateToSystemTab();
        }

        [When(@"clicks on Add User")]
        public async Task WhenClicksOnAddUser()
        {
            await _addUserPage.ClickAddUser();
        }

        //[When(@"fills in the user information form with valid details")]
        //public async Task WhenFillsInTheUserInformationFormWithValidDetails()
        //{

        //    try
        //    {
        //        await _addUserPage.FillUserForm(
        //            username: "TestUser1",
        //            password: "SecurePass123",
        //            firstName: "John",
        //            lastName: "Doe",
        //            email: "john.doe@example.com",
        //            office: "Lontex Manchester",
        //            status: "Active",
        //            userType: "Administrator",
        //            mobileNo: "7095621330", Address: "Kukatpally, hyderabad"
        //        );




        //    }
        //    catch (Exception e ){
        //        Console.WriteLine(e.Message);
        //    }
        //}

        [When(@"fills in the user information form with valid details")]
        public async Task WhenFillsInTheUserInformationFormWithValidDetails()
        {
            // Generate dynamic values
            string dynamicUsername = $"User_{Guid.NewGuid().ToString().Substring(0, 6)}";
            string dynamicPassword = $"Pass_{Guid.NewGuid().ToString().Substring(0, 8)}";
            string dynamicFirstName = $"First_{DateTime.Now.Ticks % 1000}";
            string dynamicLastName = $"Last_{DateTime.Now.Ticks % 1000}";
            string dynamicEmail = $"{dynamicUsername}@example.com";
            string dynamicMobile = $"9{new Random().Next(100000000, 999999999)}";
            string dynamicAddress = $"Address_{Guid.NewGuid().ToString().Substring(0, 5)}";

            // Fixed values for status and userType
            string status = "Active";
            string userType = "Administrator";

            await _addUserPage.FillUserForm(
                username: dynamicUsername,
                password: dynamicPassword,
                firstName: dynamicFirstName,
                lastName: dynamicLastName,
                email: dynamicEmail,
                office: "Lontex Manchester",   // can also be randomized if needed
                status: status,
                userType: userType,
                mobileNo: dynamicMobile,
                Address: dynamicAddress
            );
        }


        [When(@"saves the new user")]
        public async Task WhenSavesTheNewUser()
        {
            await _addUserPage.Adduserbutton();
        }

        [Then(@"the new user should appear in the user list")]
        public async Task ThenTheNewUserShouldAppearInTheUserList()
        {
            Assert.True(await _addUserPage.IsUserPresent("User"));
        }
        [Then(@"print all the details in the  User table" )]
        public async Task ThenPrintAllTheDetailsInTheTable()
        {
            await _addUserPage.PrintCustomerTableAsync();
        }

       //Edit User information steps 
        [When(@"the user clicks on a username")]
        public async Task WhenTheUserClicksOnAUsername()
            
        {
            await _addUserPage.ClickUsername();
           
        }           
        [When(@"the user edits the existing information with valid details")]
            
        public async Task WhenTheUserEditsTheExistingInformationWithValidDetailsAsync()
        {
            await _addUserPage.EditExistingDetails(
                   username: "Vamshi",
                   password: "Vamshi@0123",
                   firstName: "Vamshi",
                   lastName: "Vamshi",
                   email: "vamshi@gamil.com",
                   office: "Click Cybernet",
                   status: "Active",
                   userType: "Administrator",
                   mobileNo: "7095621330", Address: "Kukatpally, Hyderabad"
               );
        }
          
        [When(@"the user clicks on the Update button")]
            
        public async Task WhenTheUserClicksOnTheUpdateInformationButton()
            
        {
            await _addUserPage.ClickUpdateUserButton();
        }
            
        [Then(@"the confirmation message should appear in the user list")]
            
        public async Task ThenTheConfirmationMessageShouldAppearInTheUserList()
            
        {
            await _addUserPage.IsUserPresent("Vamshi");          
        }

        // Delete User Steps 
        [When("the user enters the name in the Search field")]
        public async Task WhenTheUserEntersTheNameInTheSearchField()
        {
            await _addUserPage.EnterUserNameSearchField();
        }

        [When(@"Clicks on the delete icon")]
        public async Task WhenClicksOnTheDeleteIcon()
        {
            // Trigger the delete action
            await Task.Delay(2000);
            await _addUserPage.ClickDeleteicon();

            // Accept the alert using the CommonActionsPage helper
            string message = await CommonActionsPage.AcceptAlertAsync(_testContext.Page);

            // Verify the alert text
            Assert.Contains("Are you sure you want to delete", message);
        }



        [Then("the confirmation alert should be displayed")]
        public async Task ThenTheConfirmationAlertShouldBeDisplayed()
        {

           String successmessage = await commonactionpage.GetSuccessNotificationText();
            Assert.Contains("User information deleted successfully", successmessage);


        }

    }
}
