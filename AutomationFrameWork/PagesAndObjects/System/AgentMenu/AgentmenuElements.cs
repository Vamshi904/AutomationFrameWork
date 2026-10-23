using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFrameWork.PagesAndObjects.System;

internal class AgentmenuElements
{

    private readonly IPage _page;

    public AgentmenuElements(IPage page)
    {
        _page = page;
    }

    public ILocator AgentsMenu => _page.Locator("(//a [normalize-space()='Agents'])[2]");

    public ILocator AddAgentButton => _page.Locator("//a[contains(@class,'btn btn-primary')]");


    public ILocator AgentName => _page.Locator("//input[@id='agentname']");
    public ILocator PrefixcodeinputFiled  => _page.Locator("//input[@id='agentref']");


    public ILocator CountryDropdown => _page.Locator("//select[@id='country']");

    public ILocator CurrencyDropdown => _page.Locator("//select[@id='currency']");

    public ILocator City => _page.Locator("//input[@id='agent_city']");

    public ILocator State => _page.Locator("//input[@id='agent_state']");

    public ILocator Mobilecode => _page.Locator("(//input[@placeholder='Code'])[3]");

    public ILocator MobileNumber => _page.Locator("//input[@id='agentmobile']");

    public ILocator Email => _page.Locator("//input[@id='agentemails']");

    public ILocator SubmitInformationButton => _page.Locator("//button[contains(@class,'btn bg-primary')]");

    //Edit Locators 
    public ILocator NameLink => _page.Locator("(//a[@class='text-decoration-none text-primary fw-medium'])[1]");


    public ILocator Deleteicon => _page.Locator("(//td[@class='text-center']//a)[1]");
    public ILocator TableRows => _page.Locator("//div[@id='tableWrapper']");


}
