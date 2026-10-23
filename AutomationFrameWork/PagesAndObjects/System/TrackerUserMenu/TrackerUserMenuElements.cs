using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFrameWork.PagesAndObjects.System
{
    public class TrackerUserMenuElements
    {


        private readonly IPage _page;

        public TrackerUserMenuElements(IPage page)
        {
            _page = page;
        }

        public ILocator TrackerUserMenu => _page.Locator("(//a [normalize-space()='Track Users'])[1]");

        public ILocator AddTrackerUserbutton => _page.Locator("//a[contains(@class,'btn btn-primary')]");


        public ILocator Name => _page.Locator("//input[@id='name']");

        public ILocator UserName => _page.Locator("//input[@id='username']");


        public ILocator password => _page.Locator("//input[@id='password']");

        public ILocator SelectTransitionPointdropdown => _page.Locator("//select[@id='transitionpoint']");

        public ILocator SubmitInformationbutton => _page.Locator("//button[contains(@class,'btn bg-primary')]");

        public ILocator NameLink => _page.Locator("(//a[@class='text-decoration-none text-primary fw-medium'])[1]");

        public ILocator DeleteIcon => _page.Locator("(//a[@class='text-danger'])[1]");
        public ILocator SearchName => _page.Locator("//input[@id='searchInput']");










    }
}
