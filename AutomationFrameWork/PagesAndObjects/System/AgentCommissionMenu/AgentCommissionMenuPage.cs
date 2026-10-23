using System.Threading.Tasks;
using Microsoft.Playwright;

namespace AutomationFrameWork.PagesAndObjects.System
{
    public class AgentCommissionMenuPage
    {
        private readonly AgentCommissionMenuPageElements _elements;

        public AgentCommissionMenuPage(IPage page)
        {
            _elements = new AgentCommissionMenuPageElements(page);
        }

        public async Task ClickAgentCommissionsMenu()
        {
            await _elements.AgentsMenu.ClickAsync();
        }

        public async Task ClickAddAgentCommissionButton()
        {
            await _elements.AddAgentCommisionButton.ClickAsync();
        }

        public async Task FillAgentCommissionForm(string agentName, string currency, string rangeFrom, string rangeTo, string flatCommission, string percentage)
        {
            try
            {
                await _elements.RangeFrom.ClearAsync();
                await _elements.RangeFrom.FillAsync(rangeFrom);
                await _elements.RangeTo.ClearAsync();
                await _elements.RangeTo.FillAsync(rangeTo);
                await _elements.agentnameDropDown.SelectOptionAsync(agentName);
                await _elements.CurrencyDropdown.SelectOptionAsync(currency);
                //await _elements.FlatCommission.ClearAsync();
                //await _elements.FlatCommission.FillAsync(flatCommission);
                await _elements.Percentage.ClearAsync();
                await _elements.Percentage.FillAsync(percentage);
            }
            catch (Exception ex)
            {
            }
        }

        public async Task SubmitAgentCommissionForm()
        {
            await Task.Delay(2000);

            await _elements.SubmitInformationButton.ClickAsync();
        }

        public async Task<bool> IsConfirmationMessageDisplayed()
        {
            // Example: check table rows or toast message
            return await _elements.TableRows.IsVisibleAsync();
        }

        // Edit/Delete actions
        public async Task ClickAgentNameLink()
        {
            await Task.Delay(2000);

            await _elements.AgemntNameLink.ClickAsync();
        }

        public async Task ClickDeleteIcon()
        {
            await Task.Delay(2000);

            await _elements.Deleteicon.ClickAsync();
        }
        public async Task FillName()
        {
            await Task.Delay(2000);
            await _elements.SearchField.FillAsync("MADAGASCAR ANTANANARIVO - Ivoto Inter Airport");

        }
    }
}
