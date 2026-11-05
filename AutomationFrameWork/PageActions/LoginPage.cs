using AutomationFrameWork.Utilities;
using Microsoft.Playwright;

namespace AutomationFrameWork.SampleCode
{
    public class LoginPage : LoginObjects
    {
        private readonly IPage _page;

        public LoginPage(TestContext context, IPage page) : base(page)
        {
            _page = page;
        }

        public async Task VerifyPageTitle()
        {
            await PageTitle.WaitForAsync(new()
            {
                State = WaitForSelectorState.Visible,
                Timeout = 60000
            });

            var titleText = await PageTitle.InnerTextAsync();

            if (!titleText.Contains("Dashboard"))
            {
                throw new Exception($"Page title mismatch. Actual: {titleText}");
            }
            else
            {
                Console.WriteLine("Page title verified successfully");
            }
        }

        public async Task EnterCredentials(string username, string password)
        {
            if (await Username.CountAsync ()>0)
            {
                await Username.FillAsync(username);

            }       
            else
            {
               Console.WriteLine("Username field is not visible");
            }
                await Task.Delay(2000);
            if (await Password.CountAsync() > 0)
            {
                await Password.FillAsync(password);

            }
            else
            {
              Console.WriteLine("Password field is not visible");  
            }
        }

        public async Task ClickSignIn()
        {
            await Task.Delay(2000);

            if (await SignButton.CountAsync()>0)    
            {
                await SignButton.ClickAsync();

            }
            else
            {
               Console.WriteLine("Sign In button is not visible");
            }
        }

        public async Task ClickonAdvanceandprocessedbuton()
        {
          
            if (await AdvanceButton.IsVisibleAsync())
            {
                await AdvanceButton.ClickAsync();

            }
            await Task.Delay(2000);

            if (await ProcessedLink.IsVisibleAsync())
            {
                await ProcessedLink.ClickAsync();

            }
            await Task.Delay(2000);
        }
        public async Task Logout()
        {
            await SignOffLink.ClickAsync();
            await LogoutMsg.WaitForAsync(new()
            {
                State = WaitForSelectorState.Visible,
                Timeout = 10000
            });

            if (await LogoutMsg.IsVisibleAsync())
            {
                Console.WriteLine("Application successfully logged out");
            }
        }
    }
}