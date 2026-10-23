using Microsoft.Playwright;

namespace AutomationFrameWork.PagesAndObjects.System
{
    public class AgentCommissionMenuPageElements
    {
        private readonly IPage _page;
        public AgentCommissionMenuPageElements(IPage page)
        {
            _page = page;
        }

        public ILocator AgentsMenu => _page.Locator("(//a[normalize-space()='Agent Commissions'])[1]");

        public ILocator AddAgentCommisionButton => _page.Locator("//a[contains(@class,'btn btn-primary')]");


        public ILocator RangeFrom => _page.Locator("//input[@id='rangefrom']");
        public ILocator RangeTo => _page.Locator("//input[@id='rangeto']");


        public ILocator agentnameDropDown => _page.Locator("//select[@id='agentname']");

        public ILocator CurrencyDropdown => _page.Locator("//select[@id='couriertype']");

        public ILocator FlatCommission => _page.Locator("//input[@id='commission']");

        public ILocator Percentage => _page.Locator("//input[@id='percentage']");


        public ILocator SubmitInformationButton => _page.Locator("//button[contains(@class,'btn bg-primary')]");

        //Edit Locators 
        public ILocator AgemntNameLink => _page.Locator("(//a[@class='text-decoration-none text-primary fw-medium'])[1]");


        public ILocator Deleteicon => _page.Locator("(//td[@class='text-center']//a)[2]");

        public ILocator SearchField => _page.Locator("//input[@id='searchInput']");

        public ILocator TableRows => _page.Locator("//div[@id='tableWrapper']");
    }
}
