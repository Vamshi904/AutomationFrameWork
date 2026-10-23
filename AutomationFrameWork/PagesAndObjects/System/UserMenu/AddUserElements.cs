using Microsoft.Playwright;

namespace AutomationFrameWork.PagesAndObjects.System
{
    public class AddUserElements
    {
        private readonly IPage _page;

        public AddUserElements(IPage page)
        {
            _page = page;
        }

        // Locators
        public ILocator SystemTab => _page.Locator("(//a[@class='nav-link'])[6]");
        public ILocator AddUserButton => _page.Locator("//a[contains(@class,'btn-primary')]");
        public ILocator UserTypeDropdown => _page.Locator("//select[@id='usertype']");
        public ILocator UsernameInput => _page.Locator("#username");
        public ILocator PasswordInput => _page.Locator("#password");
        public ILocator FirstNameInput => _page.Locator("#firstname");
        public ILocator LastNameInput => _page.Locator("#lastname");
        public ILocator EmailInput => _page.Locator("#email");
        public ILocator OfficeDropdown => _page.Locator("#useroffice");
        public ILocator StatusDropdown => _page.Locator("#status");
        public ILocator AddUserbutton  => _page.Locator("//button[@class='btn bg-primary text-white px-4']");
        public ILocator UserListTable => _page.Locator("//div[@id='tableWrapper']");

        public ILocator Address => _page.Locator("//input[@id='address']");

        public ILocator MobileNumber  => _page.Locator("//input[@id='mobile']");
        public ILocator CouriersTab => _page.Locator("(//a[normalize-space()='Couriers'])[1]");

        public ILocator Confirmationmesssgae  => _page.Locator("//div[@id='tableWrapper']");

        public ILocator ListOfUserTitle => _page.Locator("//h3[normalize-space()='List of Users']");

        public ILocator TableRows => _page.Locator("//div[@id='tableWrapper']");


        public ILocator UsernameLink => _page.Locator("(//tbody[@id='usersTableBody']//td//a[@class='text-decoration-none text-primary fw-medium'])[2]");

        public ILocator UpdateUserButton => _page.Locator("//button[contains(@class, 'btn bg-primary') and normalize-space()='Update User']");

        public ILocator Deleteicon => _page.Locator("//td[@class='text-center']//a[@class='text-danger']");

        public ILocator SearchFeild => _page.Locator("//input[@id='searchInput']");



    }
}
