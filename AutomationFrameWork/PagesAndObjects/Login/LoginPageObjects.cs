using Microsoft.Playwright;

namespace AutomationFrameWork.PagesAndObjects.Login
{
    public class LoginPageObjects
    {
        private readonly IPage _page;
        public LoginPageObjects(IPage page)
        {
            _page = page ?? throw new ArgumentNullException(nameof(page));
        }

        public ILocator UsernameTextField =>
            _page.Locator("//input[@id='username']");

        public ILocator PasswordTextField =>
            _page.Locator("//input[@id='password']");

        public ILocator LoginBtn =>
            _page.Locator("//button[@id='loginButton']");

        public ILocator BurgerMenu =>
            _page.Locator("//li[@class='nav-item nav-profile dropdown']");

        public ILocator LogOutMenu =>
            _page.Locator("(//a[@class='dropdown-item'])[2]");
    }
}
