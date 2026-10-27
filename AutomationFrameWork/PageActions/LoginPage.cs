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

            if (!titleText.Contains("ScrubDB"))
            {
                throw new Exception($"Page title mismatch. Actual: {titleText}");
            }
            else
            {
                Console.WriteLine("Page title verified successfully");
            }
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