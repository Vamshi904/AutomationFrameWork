using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AutomationFrameWork.PagesAndObjects.System.TransitionPointMenu
{
    public class TransitionPointPageElement
    {
        private readonly IPage _page;

        public TransitionPointPageElement(IPage page)
        {
            _page = page;
        }

        public ILocator TransitionpointMenu => _page.Locator("(//a [normalize-space()='Transition Points'])[1]");

        public ILocator Transitionbutton => _page.Locator("//a[contains(@class,'btn btn-primary')]");

        public ILocator Transitionpointname => _page.Locator("//input[@id='transitionpointname']");

        public ILocator UsertypeDropdown => _page.Locator("//select[@id='usertype']");
        public ILocator UserOfficeDropdown => _page.Locator("//select[@id='useroffice']");

        public ILocator SelectTransitionStatus => _page.Locator("//label[@class='form-check-label fw-normal']").Nth(16);

        public ILocator SubmitInformationbutton => _page.Locator("//button[contains(@class,'btn bg-primary')]");

        public ILocator NameLink => _page.Locator("(//a[@class='text-decoration-none text-primary fw-medium'])[1]");

        public ILocator DeleteIcon => _page.Locator("(//a[@class='text-danger'])[1]");

        public ILocator SearchName => _page.Locator("//input[@id='searchInput']");


    }
}
