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


        public ILocator PageTitle => _page.GetByText("ScrubDB (UAT)");
        public ILocator SignOffLink => _page.GetByRole(AriaRole.Link, new() { Name = "Sign Off" });
        public ILocator LogoutMsg => _page.GetByRole(AriaRole.Heading, new() { Name = "You’ve been signed out of " });
    }
}