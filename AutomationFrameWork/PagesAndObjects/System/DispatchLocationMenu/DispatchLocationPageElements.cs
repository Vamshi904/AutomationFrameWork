using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFrameWork.PagesAndObjects.System
{
    public class DispatchLocationPageElements
    {

        private readonly IPage _page;

        public DispatchLocationPageElements(IPage page)
        {
            _page = page;
        }

        public ILocator DispatchLocationsMenu => _page.Locator("(//a [normalize-space()='Dispatch Locations'])[1]");

        public ILocator AddDispatchLocationButton => _page.Locator("//a[contains(@class,'btn btn-primary')]");


        public ILocator DispatchLocationInput => _page.Locator("//input[@id='dispatchlocation']");


        public ILocator SubmitInformationButton => _page.Locator("//button[contains(@class,'btn bg-primary') and normalize-space()='Submit Information']");

        public ILocator CorrespondingAgentDropdown => _page.Locator("//select[@id='correspondingagent']");

        public ILocator DispathchLocationName => _page.Locator("(//td//a[@class='text-decoration-none text-primary fw-medium'])[1]");

        //Edit Locators 
        public ILocator DispatchLocationInputField => _page.Locator("//input[@id='dispatch_location']");


        public ILocator Deleteicon => _page.Locator("(//td[@class='text-center']//a)[1]");










    }
}
