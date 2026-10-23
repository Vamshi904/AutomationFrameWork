using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFrameWork.PagesAndObjects.System
{
    public class CountriesPageElements
    {


        private readonly IPage _page;
        public CountriesPageElements(IPage page)
        {
            _page = page;
        }
        public ILocator Countrymenu => _page.Locator("(//a [normalize-space()='Countries'])[1]");

        public ILocator AddCountrybutton => _page.Locator("//a[contains(@class,'btn btn-primary')]");

        public ILocator Countryname => _page.Locator("//input[@id='countryname']");
        public ILocator CurrencyCodeField => _page.Locator("//input[@id='countrycode']");
        public ILocator measurementsDropdown => _page.Locator("//select[@id='measurements']");
        public ILocator courier_typedropdown => _page.Locator("//select[@id='courier_type']");

        public ILocator SubmitButton => _page.Locator("//button[contains(@class,'btn bg-primary')]");

       
        public ILocator CurrencyName => _page.Locator("(//td//a[@class='text-decoration-none text-primary fw-medium'])[1]");

        public ILocator SearchName => _page.Locator("//input[@id='searchInput']");

    }
}
