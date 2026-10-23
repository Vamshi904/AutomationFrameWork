using AutomationFrameWork.PagesAndObjects.Login;
using AutomationFrameWork.Utilities;
using Microsoft.Playwright;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AutomationFrameWork.PagesAndObjects.System
{
    public class AddUserPage
    {
        private readonly TestContext _testContext;
        private readonly AddUserElements _elements;
        private readonly LoginPage _loginPage;

        public AddUserPage(TestContext testContext)
        {
            _testContext = testContext;
            _elements = new AddUserElements(_testContext.Page);
            _loginPage = new LoginPage(_testContext); // reuse existing login page object
        }

        // Login workflow
        public async Task LoginAsync(string username, string password, string loginUrl)
        {
            await _loginPage.NavigateAsync(loginUrl);
            await _loginPage.LoginAsync(username, password);
        }

        // Actions
        public async Task NavigateToSystemTab()
        {

            await _elements.CouriersTab.HoverAsync();
            await Task.Delay(2000); // waits 2 seconds
            await _elements.CouriersTab.ClickAsync();
           
            await _elements.SystemTab.HoverAsync();
            await Task.Delay(2000); // waits 2 seconds
            await _elements.SystemTab.ClickAsync(); 
        }

        public async Task ClickAddUser() => await _elements.AddUserButton.ClickAsync();

        public async Task FillUserForm(
            string username,
            string password,
            string firstName,
            string lastName,
            string email,
            string office,
            string status,
            string userType, string mobileNo , String Address)
        {
            try
            {
                //dropdowns 
                await _elements.UserTypeDropdown.SelectOptionAsync(userType);
                await Task.Delay(2000); // waits 2 seconds
                await _elements.StatusDropdown.SelectOptionAsync(status);
                await Task.Delay(2000); // waits 2 seconds
                await _elements.OfficeDropdown.SelectOptionAsync(office);
                await Task.Delay(2000); // waits 2 seconds

                //fillforms 
                await _elements.UsernameInput.FillAsync(username);
                await Task.Delay(2000); // waits 2 seconds
                await _elements.PasswordInput.FillAsync(password);
                await Task.Delay(2000); // waits 2 seconds
                await _elements.FirstNameInput.FillAsync(firstName);
                await Task.Delay(2000); // waits 2 seconds
                await _elements.LastNameInput.FillAsync(lastName);
                await Task.Delay(2000); // waits 2 seconds
                await _elements.EmailInput.FillAsync(email);
                await Task.Delay(2000); // waits 2 seconds
                await _elements.MobileNumber.FillAsync(mobileNo);
                await Task.Delay(2000); // waits 2 seconds
                await _elements.Address.FillAsync(Address);
                await Task.Delay(2000); // waits 2 seconds
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString() + e.Message); 
            }
        }

        public async Task Adduserbutton() => await _elements.AddUserbutton.ClickAsync();

        public async Task<bool> IsUserPresent(string username)
        {
            var confirmationmessage = _elements.Confirmationmesssgae;
            int count = await confirmationmessage.CountAsync();

            var title = _elements.ListOfUserTitle;
            int titlecount = await title.CountAsync();
            await Task.Delay(2000); // waits 2 seconds
            if (count > 0 && titlecount>0 || await title.IsVisibleAsync())
            {
                Console.WriteLine("Successfully added user");
            }
            else
            {
                Console.WriteLine("Confirmation message is not displayed");
            }

            return await _elements.ListOfUserTitle.IsVisibleAsync();
        }

        public async Task PrintCustomerTableAsync()
        {
            var rows = await _elements.TableRows.AllAsync();
            foreach (var row in rows)
            {
                var cells = await row.Locator("td").AllAsync();
                var rowData = new List<string>();
                foreach (var cell in cells)
                { rowData.Add(await cell.InnerTextAsync()); }
                Console.WriteLine(string.Join(" | ", rowData));
            }
        }

        public async Task ClickUsername()
        {
            await _elements.UsernameLink.ClickAsync();
        }

        public async Task ClickUpdateUserButton()
        {
            await _elements.UpdateUserButton.ClickAsync();
        }

        public async Task EditExistingDetails(string username,
            string password,
            string firstName,
            string lastName,
            string email,
            string office,
            string status,
            string userType, string mobileNo, String Address)
        {
            try
            {
                await _elements.UserTypeDropdown.SelectOptionAsync(userType);
                await Task.Delay(2000); // waits 2 seconds
                await _elements.StatusDropdown.SelectOptionAsync(status);
                await Task.Delay(2000); // waits 2 seconds
              await _elements.OfficeDropdown.SelectOptionAsync(office);
                await Task.Delay(2000); // waits 2 seconds

                //fillforms 
                await _elements.UsernameInput.ClearAsync();
                await _elements.UsernameInput.FillAsync(username);
                await Task.Delay(2000);
                // waits 2 seconds
  var count= await _elements.PasswordInput.CountAsync();

                if (count > 0)
                {
  await _elements.PasswordInput.ClearAsync();
                await _elements.PasswordInput.FillAsync(password);
                }
              
                await Task.Delay(2000); // waits 2 seconds
                await _elements.FirstNameInput.ClearAsync();
                await _elements.FirstNameInput.FillAsync(firstName);
                await Task.Delay(2000); // waits 2 seconds
                await _elements.LastNameInput.ClearAsync();
                await _elements.LastNameInput.FillAsync(lastName);
                await Task.Delay(2000); // waits 2 seconds
                await _elements.EmailInput.ClearAsync();
                await _elements.EmailInput.FillAsync(email);
                await Task.Delay(2000); // waits 2 seconds
                await _elements.MobileNumber.ClearAsync();
                await _elements.MobileNumber.FillAsync(mobileNo);
                await Task.Delay(2000); // waits 2 seconds
                await _elements.Address.ClearAsync();
                await _elements.Address.FillAsync(Address);
                await Task.Delay(2000); // waits 2 seconds
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task ClickDeleteicon()
        {
            await _elements.Deleteicon.HoverAsync();
            await Task.Delay(2000);
            await _elements.Deleteicon.ClickAsync();
        }

        public async Task EnterUserNameSearchField()
        {
            await _elements.SearchFeild.WaitForAsync();
            await Task.Delay(2000);
            await _elements.SearchFeild.FillAsync("Adam1");
            await Task.Delay(1000);
            await _elements.SearchFeild.PressAsync("Enter");

        }


    }
}
