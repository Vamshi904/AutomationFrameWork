using AutomationFrameWork.Utilities;
using System.Linq;
namespace AutomationFrameWork.PagesAndObjects.System
{
    public class CurrenciesMenuPage
    {
        private readonly CurrenciesMenuPageElements currenciesMenuPageElements;
        private readonly TestContext _testContext;

        public CurrenciesMenuPage(TestContext testContext)
        {
            _testContext = testContext;
            currenciesMenuPageElements = new CurrenciesMenuPageElements(_testContext.Page);
        }

        public async Task ClickCurrencyMenu()
        {
            await currenciesMenuPageElements.CurrencyMenu.ClickAsync();
        }

        public async Task ClickAddCurrencyButton()
        {
            await currenciesMenuPageElements.AddCurrencyButton.ClickAsync();
        }

        public async Task FillCurrencyForm(string currencyName, string currencyCode, string countryName, string additemcourier)
        {
            await currenciesMenuPageElements.CurrencyNameField.ClearAsync();
            await currenciesMenuPageElements.CurrencyNameField.FillAsync(currencyName);
            await currenciesMenuPageElements.CurrencyCodeField.ClearAsync();
            await currenciesMenuPageElements.CurrencyCodeField.FillAsync(currencyCode);
            await currenciesMenuPageElements.CountryNameDropdown.SelectOptionAsync(countryName);
            await currenciesMenuPageElements.additemcourierdropdown.SelectOptionAsync(additemcourier);
        }

        public async Task SubmitCurrencyForm()
        {
            await currenciesMenuPageElements.SubmitButton.HoverAsync();
            await currenciesMenuPageElements.SubmitButton.ClickAsync();

        }
        public async Task ClickCurrencyName()
        {
            await currenciesMenuPageElements.CurrencyName.ClickAsync();

        }
        public async Task SearchName()
        {
            await currenciesMenuPageElements.SearchName.FillAsync("Algerian Dinar");
            await currenciesMenuPageElements.SearchName.PressAsync("Enter");


        }

    }
}
