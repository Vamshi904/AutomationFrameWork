using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFrameWork.PagesAndObjects.System
{
    public class TransitionStatusPageElements
    {

        private readonly IPage _page;

        public TransitionStatusPageElements(IPage page)
        {
            _page = page;
        }

        public ILocator TransitionStatusMenu => _page.Locator("(//a [normalize-space()='Transition Points'])[1]");

        public ILocator AddNewStatusButton => _page.Locator("//a[contains(@class,'btn btn-primary')]");

        public ILocator TransitionStatusName => _page.Locator("//input[@id='transitionpointname']");

        public ILocator SelectUsertypeDropdown => _page.Locator("//select[@id='usertype']");

        public ILocator StatusBeUpdatedDropdown => _page.Locator("//select[@id='repeatstatus']");

        public ILocator IsConsignmentStatusDropdown => _page.Locator("//select[@id='consignment_status']");


        public ILocator SubmitInformationbutton => _page.Locator("//button[contains(@class,'btn bg-primary')]");

        public ILocator NameLink => _page.Locator("(//a[@class='text-decoration-none text-primary fw-medium'])[1]");

        public ILocator DeleteIcon => _page.Locator("(//a[@class='text-danger'])[1]");

    }
}
