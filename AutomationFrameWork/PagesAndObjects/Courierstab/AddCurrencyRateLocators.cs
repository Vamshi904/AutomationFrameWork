using Microsoft.Playwright;

namespace AutomationFrameWork.PagesAndObjects.Couriers
{
    public class AddCurrencyRateLocators
    {
        private readonly IPage _page;

        public AddCurrencyRateLocators(IPage page)
        {
            _page = page;
        }

        public ILocator CouriersTab => _page.Locator("(//a[normalize-space()='Couriers'])[1]");
        public ILocator RateMenu => _page.Locator("//a[normalize-space()='Rates']");
        public ILocator AddRatesButton => _page.Locator("//a[contains(@class,'btn-primary') and normalize-space()='Add Rates']");
        public ILocator FromCurrencyDropdown => _page.Locator("#fromcurrency");
        public ILocator ToCurrencyDropdown => _page.Locator("#tocurrency");
        public ILocator ExchangeRateInput => _page.Locator("#exch_rate");
        public ILocator SubmitButton => _page.Locator("//button[normalize-space()='Submit Information']");
        public ILocator RatesList => _page.Locator("//div[@id='tableWrapper']");

             //for coustomer tab locators 
        public ILocator CustomerTab => _page.Locator("(//a[@class='nav-link '])[2]"); 
        public ILocator StatusDropdown => _page.Locator("#statusSelect");
        // adjust selector
        public ILocator TableRows => _page.Locator("//div[@id='tableWrapper']//tr");


        // Edit rate locators 
        public ILocator UsernameLink => _page.Locator("(//td[@class='text-truncate col-from-currency']//a)[1]");

        public ILocator UpdateRateButton => _page.Locator("//button[contains(@class,'btn bg-primary') and normalize-space()='Update Rate']");

        public ILocator RateListTitle => _page.Locator("//h3[normalize-space()='Currency Rates']");


    }
}
