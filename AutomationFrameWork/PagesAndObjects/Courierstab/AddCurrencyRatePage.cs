using AutomationFrameWork.Utilities;
using Microsoft.Playwright;
using System.Threading.Tasks;

namespace AutomationFrameWork.PagesAndObjects.Couriers
{
    public class AddCurrencyRatePage
    {
        private readonly TestContext _testContext;
        private readonly AddCurrencyRateLocators _locators;

        public AddCurrencyRatePage(TestContext testContext)
        {
            _testContext = testContext;
            _locators = new AddCurrencyRateLocators(testContext.Page);
        }

        public async Task NavigateToCouriersTabAsync()
        {
            await Task.Delay(7000);

            await _locators.CouriersTab.ClickAsync();
        }

        public async Task ClickRateMenuAsync()
        {
            await _locators.RateMenu.ClickAsync();
        }

        public async Task ClickAddRatesButtonAsync()
        {
            await _locators.AddRatesButton.ClickAsync();
        }

        public async Task FillCurrencyRateFormAsync(string fromCurrency, string toCurrency, string exchangeRate)
        
        {
            await _locators.FromCurrencyDropdown.SelectOptionAsync(new[] { fromCurrency });
            await _locators.ToCurrencyDropdown.SelectOptionAsync(new[] { toCurrency });
            await _locators.ExchangeRateInput.FillAsync(exchangeRate);
        }


        public async Task ClickSaveRateButtonAsync()
        {
            await _locators.SubmitButton.ClickAsync();
        }

        public async Task<bool> IsRatePresentAsync()
        {
            //var rowCount = await _locators.RatesList.Locator("//div[@class='alert alert-success alert-dismissible fade show text-center']").CountAsync();
            var rowCount = await _locators.RatesList.Locator("[class='text-truncate col-from-currency']").CountAsync();

            if (rowCount > 0)
            {
                Console.WriteLine(" Ratelist table is displayed ");
                Console.WriteLine("Confirmation message is displayed ");
            }
            else
            {
                Console.WriteLine(" Confimation alert is not displayed ");
            }
       return rowCount > 0;
        }

        public async Task EditCurrencyRateFormAsync(string fromCurrency, string toCurrency, string exchangeRate)

        {
            await _locators.FromCurrencyDropdown.SelectOptionAsync(new[] { fromCurrency });
            await _locators.ToCurrencyDropdown.SelectOptionAsync(new[] { toCurrency });
            await _locators.ExchangeRateInput.ClearAsync();
            await _locators.ExchangeRateInput.FillAsync(exchangeRate);
        }

        public async Task ClickUpdateButtonAsync()
        {
            await Task.Delay(2000);

            await _locators.UpdateRateButton.ClickAsync();
        }

        public async Task ClickUsernameButtonAsync()
        {
            await Task.Delay(2000);
            await _locators.UsernameLink.ClickAsync();

        }


        public async Task NavigateToCustomerTabAsync()
        {
            await _locators.CustomerTab.ClickAsync();
        }

        public async Task SelectActiveStatusAsync()
        { // Select by value
            await _locators.StatusDropdown.SelectOptionAsync("Active");
        }

        public async Task PrintCustomerTableAsync()
        {
            var rows = await _locators.TableRows.AllAsync();
            foreach (var row in rows)
            {
                var cells = await row.Locator("td").AllAsync();
                var rowData = new List<string>();
                foreach (var cell in cells)
                { rowData.Add(await cell.InnerTextAsync()); }
                Console.WriteLine(string.Join(" | ", rowData));
            }
        }
    }
}
