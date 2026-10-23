using AutomationFrameWork.Utilities;
using System.Threading.Tasks;

namespace AutomationFrameWork.PagesAndObjects.System
{
    public class CountriesPage
    {
        private readonly TestContext _testContext;
        private readonly CountriesPageElements CountriesPageElements;

        public CountriesPage(TestContext testContext)
        {
            _testContext = testContext;
            CountriesPageElements = new CountriesPageElements(_testContext.Page);
        }

        public async Task ClickCountryMenu()
        {
            await Task.Delay(2000);
            await CountriesPageElements.Countrymenu.ClickAsync();
        }

        public async Task ClickAddCountryButton()
        {
            await Task.Delay(2000);
            await CountriesPageElements.AddCountrybutton.ClickAsync();
        }

        public async Task FillCountryForm(string countryName, string countrycode, string M, string v)
        {
            await CountriesPageElements.Countryname.FillAsync(countryName);
            await Task.Delay(2000);
            await CountriesPageElements.CurrencyCodeField.FillAsync(countrycode);
            await Task.Delay(2000);
            await CountriesPageElements.measurementsDropdown.SelectOptionAsync(new[] { M });
            await Task.Delay(2000);
            await CountriesPageElements.courier_typedropdown.SelectOptionAsync(new[] { v });



        }

        public async Task SubmitCountry()
        {
            await CountriesPageElements.SubmitButton.HoverAsync();
            await Task.Delay(2000);
            await CountriesPageElements.SubmitButton.ClickAsync();
        }

       

        public async Task ClickCountryNameLink()
        {
            await Task.Delay(2000);
            await CountriesPageElements.CurrencyName.ClickAsync();
        }

        public async Task SearchName()
        {
            await CountriesPageElements.SearchName.FillAsync("Algerian Dinar");
            await CountriesPageElements.SearchName.PressAsync("Enter");


        }


    }
}
