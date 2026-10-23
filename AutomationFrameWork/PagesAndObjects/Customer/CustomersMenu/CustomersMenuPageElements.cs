using Microsoft.Playwright;

namespace AutomationFrameWork.PagesAndObjects.CustomerTab
{
    public class CustomersMenuPageElements
    {
        private readonly IPage page;
        public CustomersMenuPageElements(IPage page)
        {

            this.page = page;   //This stores the Playwright browser page
        }

        public ILocator logo => page.Locator("(//img[@alt='logo'])[1]");
        public ILocator WelcomeText => page.Locator("//h3[@class='font-weight-bold px-2']");


        public ILocator Customerstab => page.Locator("(//a[normalize-space()='Customers'])[1]");
        public ILocator Addcustomerbutton => page.Locator("//h3[normalize-space()='List of Customers']//following-sibling::a[@class='btn btn-primary btn-sm']");
        public ILocator CustomerName => page.Locator("//input[@name='name']");
        public ILocator CustomerCity => page.Locator("//input[@name='city']");
        public ILocator Country => page.Locator("//select[@name='country']");
        public ILocator CountryCode => page.Locator("//input[@id='countrycode']");
        public ILocator NameLink => page.Locator("(//a[@class='text-primary text-decoration-none fw-medium'])[1]");


        public ILocator CTelephone1 => page.Locator("//input[@name='telephone']");
        public ILocator CEmail => page.Locator("//input[@name='email']");
        public ILocator CustomerAdress => page.Locator("//textarea[@name='address']");
        public ILocator CPostcode => page.Locator("//input[@name='postcode']");
        public ILocator CMobile => page.Locator("//input[@name='mobile']");
        public ILocator CTelephone2 => page.Locator("//input[@name='telephone1']");
        public ILocator CAddcustomer => page.Locator("(//button[@type='submit'])[3]");
        public ILocator CBack => page.Locator("//a[@class=\"btn btn-primary btn-sm\"]");
        public ILocator CSearchBy => page.Locator("//input[@name=\"search\"]");
        public ILocator active => page.Locator("//select[@name='status']");
        public ILocator CSearchButton => page.Locator("(//button[@type='submit'])[3]");

        public ILocator TableRows => page.Locator("table tbody tr");
        public ILocator DeleteIcon => page.Locator("//i[contains(@class,'fa-trash')]");
        public ILocator refresh => page.Locator("//button[@id='resetBtn']");

        public ILocator activedropdown => page.Locator("//select[@name='status']");
        public ILocator StatusCells => page.Locator("//table//tbody//tr/td[8]");
        //Dashboard Search Customer
        public ILocator searchCustomer => page.Locator("(//input[@type=\"text\"])[1]");
        public ILocator searcharrow => page.Locator("(//button[@type=\"submit\"])[1]");

        public ILocator customerTableHeader => page.Locator("//thead[@class='table-primary text-white sticky-top']//th");
        public ILocator tableid => page.Locator("//tbody[@id=\"customersTableBody\"]");


        public ILocator Status => page.Locator("(//table//tr[1]//td[10])");
        public ILocator Activate => page.Locator("(//button[normalize-space()='Activate'])[1]");
        public ILocator Deactivate => page.Locator("(//button[normalize-space()='Deactivate'])[1]");

      


    }
}
