using AutomationFrameWork.Utilities;
using Microsoft.Playwright;

namespace AutomationFrameWork.PagesAndObjects.Login
{
    public class LoginPage
    {
        private readonly TestContext _context;
        private readonly LoginPageObjects _objects;

        public LoginPage(TestContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _objects = new LoginPageObjects(_context.Page); // Page object tied to IPage
        }

        public async Task NavigateAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("URL cannot be null or empty", nameof(url));

            await _context.Page.GotoAsync(url);
            await _context.Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }

        public async Task LoginAsync(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("Username cannot be null or empty", nameof(userName));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null or empty", nameof(password));

            await _objects.UsernameTextField.WaitForAsync();
            await _objects.UsernameTextField.FillAsync(userName);

            await _objects.PasswordTextField.WaitForAsync();
            await _objects.PasswordTextField.FillAsync(password);

            await _objects.LoginBtn.WaitForAsync();
            await _objects.LoginBtn.ClickAsync();

            Console.WriteLine("** Username, Password entered and login button clicked");

            await _context.Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            _context.CommonActions.LogCurrentPageUrl();
        }

        public async Task LogoutAsync()
        {
            await _objects.BurgerMenu.WaitForAsync();
            await _objects.BurgerMenu.HoverAsync();
            await _context.Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await _objects.BurgerMenu.ClickAsync();

            Console.WriteLine("** Clicked on  menu");

            await _objects.LogOutMenu.WaitForAsync();
            await _objects.LogOutMenu.ClickAsync();

            Console.WriteLine("** Clicked on logout button");

            await _context.Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }


    }
}
