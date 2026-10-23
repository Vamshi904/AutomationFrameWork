using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFrameWork.PagesAndObjects.System.ConsignmentsMenu
{
    internal class ConsignmentPageElement
    {

        private readonly IPage _page;
        public ConsignmentPageElement(IPage page)
        {
            _page = page;
        }
        public ILocator ConsignmentMenu => _page.Locator("(//a [normalize-space()='Consignments'])[1]");
        public ILocator AddConsignmentbutton => _page.Locator("//a[contains(@class,'btn btn-primary')]");
        public ILocator ConsignmentName => _page.Locator("//input[@id='consignmentname']");
        public ILocator ShortCode => _page.Locator("//input[@id='shortcodename']");
        public ILocator ChooseCouierType => _page.Locator("//select[@id='courier_type']");
        public ILocator SubmitButton => _page.Locator("//button[contains(@class,'btn bg-primary')]");
        public ILocator ConsignmentNameLink => _page.Locator("(//td//a[@class='text-decoration-none text-primary fw-medium'])[1]");

        public ILocator SearchName => _page.Locator("//input[@id='searchInput']");
    }
}
