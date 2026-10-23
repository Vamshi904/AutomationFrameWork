using AutomationFrameWork.Pages;
using AutomationFrameWork.Utilities;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Playwright;


namespace AutomationFrameWork.PagesAndObjects.CustomerTab
{
    public class CustomerMenuPage
    {
        private readonly TestContext _context;  //TestContext shares common objects across the scenario.
        private readonly CustomersMenuPageElements customerpage;
        public CustomerMenuPage(TestContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            customerpage = new CustomersMenuPageElements(_context.Page); // Page object tied to IPage
        }
        public async Task ValidateApplicationNameDisplayed()
        {
            await Assertions.Expect(customerpage.logo).ToBeVisibleAsync();
        }
       public async Task Customers() => await customerpage.Customerstab.ClickAsync();
        public async Task Addcustomer() => await customerpage.Addcustomerbutton.ClickAsync();
        public async Task AddCustomerbutton() => await customerpage.CAddcustomer.ClickAsync();
        public async Task Back() => await customerpage.CBack.ClickAsync();
        public async Task SearchByName() => await customerpage.CSearchBy.FillAsync("Vamshi");
        public async Task SearchByID() => await customerpage.CSearchBy.FillAsync("Va1234");
        public async Task SearchByEmail() => await customerpage.CSearchBy.FillAsync("Vamshi@gamail.com");
        public async Task Search() => await customerpage.CSearchButton.ClickAsync();
        //public async Task FillForms()
        //{
        //    await customerpage.CName.FillAsync("Chandrashekar");
        //    await customerpage.CCity.FillAsync("Hyderabad");
        //    Thread.Sleep(1000);
        //    await customerpage.CCountry.SelectOptionAsync("INDIA");
        //    Thread.Sleep(1000);
        //    await customerpage.CTelephone1.FillAsync("9989123456");
        //    await customerpage.CEmail.FillAsync("customer@gmail.com");
        //    await customerpage.CustomerAdress.FillAsync("123 Test Street, Hyderabad");
        //    await customerpage.CPostcode.FillAsync("500082");
        //    await customerpage.CMobile.FillAsync("9703123456");
        //    await Task.Delay(2000);
        //    await customerpage.CTelephone2.FillAsync("9989001122");
        //}
        //public async Task Updatefoms()
        //{
        //    await customerpage.CName.FillAsync("Chanrashekar");
        //    await customerpage.CCity.FillAsync("Karimnagar");
        //    Thread.Sleep(1000);
        //    await customerpage.CCountry.SelectOptionAsync("INDIA");
        //    Thread.Sleep(1000);
        //    await customerpage.CTelephone1.FillAsync("9982123456");
        //    await customerpage.CEmail.FillAsync("customer2@gmail.com");
        //    await customerpage.CustomerAdress.FillAsync("123 Test Street, Hyderabad");
        //    await customerpage.CPostcode.FillAsync("500082");
        //    await customerpage.CMobile.FillAsync("970311111");
        //    await Task.Delay(2000);
        //    await customerpage.CTelephone2.FillAsync("9111001122");
        //}

        public async Task FillForms()
        {
            string uniqueNumber = DateTime.Now.Ticks.ToString();
            string phone1 = "9" + uniqueNumber.Substring(uniqueNumber.Length - 9);
            string phone2 = "8" + uniqueNumber.Substring(uniqueNumber.Length - 9);
            string email = $"customer{uniqueNumber}@gmail.com";

            await customerpage.CustomerName.FillAsync("Vamshi");
            await customerpage.CustomerCity.FillAsync("Kamareddy");
            Thread.Sleep(1000);
            await customerpage.Country.SelectOptionAsync("USA");
            Thread.Sleep(1000);
            await customerpage.CTelephone1.FillAsync(phone1);
            await customerpage.CEmail.FillAsync(email);
            await customerpage.CustomerAdress.FillAsync("123 Test Street, Hyderabad");
            await customerpage.CPostcode.FillAsync("500082");
            await customerpage.CMobile.FillAsync(phone1);
            await customerpage.CTelephone2.FillAsync(phone2);
        }


        public async Task UpdateForms()
        {
            string uniqueNumber = DateTime.Now.Ticks.ToString();
            string phone1 = "9" + uniqueNumber.Substring(uniqueNumber.Length - 9);
            string phone2 = "8" + uniqueNumber.Substring(uniqueNumber.Length - 9);
            string email = $"customer{uniqueNumber}@gmail.com";
            await customerpage.CustomerName.FillAsync("Akhil");
            await customerpage.CustomerCity.FillAsync("kamareddy");
            Thread.Sleep(1000);
            await customerpage.Country.SelectOptionAsync("USA");
            Thread.Sleep(1000);
            await customerpage.CTelephone1.FillAsync(phone1);
            await customerpage.CEmail.FillAsync(email);
            await customerpage.CustomerAdress.FillAsync("123 Test Street, Hyderabad");
            await customerpage.CPostcode.FillAsync("5003111");
            await customerpage.CMobile.FillAsync(phone1);
            await customerpage.CTelephone2.FillAsync(phone2);
        }

        //public async Task NewBeneficiaryDetails()
        //{
        //    await customerpage.CName.FillAsync("Kiran");
        //    await customerpage.CCity.FillAsync("Karimnagar");
        //    Thread.Sleep(1000);
        //    await customerpage.CCountry.SelectOptionAsync("INDIA");
        //    Thread.Sleep(1000);
        //    await customerpage.CTelephone1.FillAsync("9989123456");
        //    await customerpage.CEmail.FillAsync("customer@gmail.com");
        //    await customerpage.CustomerAdress.FillAsync("123 Test Street, Hyderabad");
        //    await customerpage.CPostcode.FillAsync("500082");
        //    await customerpage.CMobile.FillAsync("9703123456");
        //    await Task.Delay(2000);
        //    await customerpage.CTelephone2.FillAsync("9989001122");
        //}

        public async Task FillNewBeneficiaryDetails()
        {
            string uniqueNumber = DateTime.Now.Ticks.ToString();
            string phone1 = "9" + uniqueNumber.Substring(uniqueNumber.Length - 9);
            string phone2 = "8" + uniqueNumber.Substring(uniqueNumber.Length - 9);
            string email = $"beneficiary{uniqueNumber}@gmail.com";

            await customerpage.CustomerName.FillAsync("Shekar");
            await customerpage.CustomerCity.FillAsync("Karimnagar");
            Thread.Sleep(1000);
            await customerpage.Country.SelectOptionAsync("USA");
            Thread.Sleep(1000);
            await customerpage.CTelephone1.FillAsync(phone1);
            await customerpage.CEmail.FillAsync(email);
            await customerpage.CustomerAdress.FillAsync("123 Test Street, Hyderabad");
            await customerpage.CPostcode.FillAsync("500082");
            await customerpage.CMobile.FillAsync(phone1);
            await customerpage.CTelephone2.FillAsync(phone2);
        }

        public async Task Namelink() => await customerpage.NameLink.ClickAsync();


        public async Task Refresh() => await customerpage.refresh.ClickAsync();

        public async Task Active() => await customerpage.activedropdown.SelectOptionAsync("Active");
        public async Task InActive() => await customerpage.activedropdown.SelectOptionAsync("Inactive");

        public async Task SearchCustomer() => await customerpage.searchCustomer.FillAsync("Advaith");
        public async Task SearchArrow() => await customerpage.searcharrow.ClickAsync();





        
    }
}
















