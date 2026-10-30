using AutomationFrameWork.Utilities;
using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

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
            if(await Username.IsVisibleAsync())
            {
                await Username.FillAsync(username);

            }
            await Task.Delay(2000);
            if(await Password.IsVisibleAsync())
            {
                await Password.FillAsync(password);

            }
        }

        public async Task ClickSignIn()
        {
           if(await SignButton.IsVisibleAsync())
            {
                await SignButton.ClickAsync();

            }
        }

        public async Task ClickonAdvanceandprocessedbuton()
        {
            await SignButton.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 15000
            });
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