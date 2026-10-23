using AutomationFrameWork.Utilities;
using Microsoft.Playwright;
using System.Threading.Tasks;

namespace AutomationFrameWork.PagesAndObjects.System
{
    internal class AgentMenuPage
    {
        private readonly AgentmenuElements _elements;
        private readonly TestContext _testContext;
        internal readonly object TableRows;

        public AgentMenuPage(TestContext testContext)
        {
            _testContext = testContext;
            _elements = new AgentmenuElements(_testContext.Page); 
        }

        // Actions
        public async Task ClickAgentsMenu() { 
            await Task.Delay(500);
            await _elements.AgentsMenu.ClickAsync(); 
        }


        public async Task ClickAddAgentButton() {
            await Task.Delay(500);

            await _elements.AddAgentButton.ClickAsync();
        }

        public async Task FillAgentForm(string name, string prefixCode, string country, string currency, string city, string state, string mobileCode, string mobileNumber, string email)

        {

            try
            {
                await _elements.AgentName.FillAsync(name);
                await _elements.PrefixcodeinputFiled.FillAsync(prefixCode);
                await _elements.CountryDropdown.SelectOptionAsync(new[] { country });
                await _elements.CurrencyDropdown.SelectOptionAsync(new[] { currency });
                await _elements.City.FillAsync(city);
                await _elements.State.FillAsync(state);
                await _elements.Mobilecode.FillAsync(mobileCode);
                await _elements.MobileNumber.FillAsync(mobileNumber);
                await _elements.Email.FillAsync(email);
            }
            catch (Exception ex)
            {
            }

        }

        public async Task SubmitAgentForm() { 
            await _elements.SubmitInformationButton.ClickAsync();
        }


        public async Task<string> GetConfirmationMessage()
        {
            // Adjust locator if confirmation message differs
            return await _elements.SubmitInformationButton.InnerTextAsync();
        }

        public async Task ClickUsernamelink()
        {
            await _elements.NameLink.ClickAsync();
        }


        public async Task DeleteAgent() => await _elements.Deleteicon.ClickAsync();


        public async Task EditFillAgentForm(string name, string prefixCode, string country, string currency, string city, string state, string mobileCode, string mobileNumber, string email)
        {
            await _elements.AgentName.ClearAsync();
            await _elements.AgentName.FillAsync(name);
            await _elements.PrefixcodeinputFiled.ClearAsync();
            await _elements.PrefixcodeinputFiled.FillAsync(prefixCode);
            await _elements.CountryDropdown.SelectOptionAsync(new[] { country });
            await _elements.CurrencyDropdown.SelectOptionAsync(new[] { currency });
            await _elements.City.ClearAsync();
            await _elements.City.FillAsync(city);
            await _elements.State.ClearAsync();
            await _elements.State.FillAsync(state);
            await _elements.Mobilecode.ClearAsync();
            await _elements.Mobilecode.FillAsync(mobileCode);
            await _elements.Email.ClearAsync();
            await _elements.MobileNumber.FillAsync(mobileNumber);
            await _elements.Email.FillAsync(email);


        }

        public async Task PrintCustomerTableAsync()
        {
            var rows = await _elements.TableRows.AllAsync();
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

