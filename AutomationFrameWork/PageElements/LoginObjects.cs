using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Playwright;

namespace AutomationFrameWork.SampleCode
{
    public class LoginObjects
    {
        protected readonly IPage _page;

        public LoginObjects(IPage page)
        {
            _page = page;
        }


        public ILocator PageTitle => _page.GetByText("Dashboard").First;
        public ILocator SignOffLink => _page.GetByRole(AriaRole.Link, new() { Name = "Sign Off" });
        public ILocator LogoutMsg => _page.GetByRole(AriaRole.Heading, new() { Name = "You’ve been signed out of " });

        public ILocator Username => _page.Locator("#userName"); 

        public ILocator Password => _page.Locator("#password"); 

        public ILocator SignButton => _page.Locator("//button[text()='Sign in']");


        public ILocator AdvanceButton => _page.Locator("[id='details-button']");

        public ILocator ProcessedLink => _page.Locator("#proceed-link");


    }
}