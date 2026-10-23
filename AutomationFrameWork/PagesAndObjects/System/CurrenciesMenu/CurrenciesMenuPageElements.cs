using Microsoft.Playwright;

namespace AutomationFrameWork.PagesAndObjects.System
{
    public class CurrenciesMenuPageElements
    {
        private readonly IPage _page;
        public CurrenciesMenuPageElements(IPage page)
        {
            _page = page;
        }
        public ILocator CurrencyMenu => _page.Locator("(//a [normalize-space()='Currencies'])[1]");

        public ILocator AddCurrencyButton => _page.Locator("//a[contains(@class,'btn btn-primary')]");

        public ILocator CurrencyNameField => _page.Locator("//input[@id='currencyname']");
        public ILocator CurrencyCodeField => _page.Locator("//input[@id='currencycode']");

        public ILocator SubmitButton => _page.Locator("//button[contains(@class,'btn bg-primary')]");

        public ILocator CountryNameDropdown => _page.Locator("//select[@id='countryname']");
        public ILocator additemcourierdropdown => _page.Locator("//select[@id='additemcourier']");
        public ILocator CurrencyName => _page.Locator("(//td//a[@class='text-decoration-none text-primary fw-medium'])[1]");

        public ILocator SearchName => _page.Locator("//input[@id='searchInput']");

    }
}
