using AutomationFrameWork.Utilities;
using Microsoft.Playwright;
using System.Threading.Tasks;

namespace AutomationFrameWork.PagesAndObjects.System.ConsignmentsMenu
{
    public class ConsignmentMenuPage
    {
        private readonly ConsignmentPageElement _elements;

        private readonly TestContext _testContext;

        public ConsignmentMenuPage(TestContext testContext)
        {
            _testContext = testContext;
            _elements = new ConsignmentPageElement(_testContext.Page);
        }

        public async Task ClickConsignmentsMenuAsync()
        {
            await _elements.ConsignmentMenu.ClickAsync();
        }

        public async Task ClickAddConsignmentButtonAsync()
        {
            await _elements.AddConsignmentbutton.ClickAsync();
        }

        public async Task FillTransitionPointFormAsync(string consignmentName, string shortCode, string courierType)
        {
            await _elements.ConsignmentName.FillAsync(consignmentName);
            await _elements.ShortCode.FillAsync(shortCode);
            await _elements.ChooseCouierType.SelectOptionAsync(new[] { courierType });
        }

        public async Task SubmitInformationAsync()
        {
            await _elements.SubmitButton.ClickAsync();
            await _elements.SubmitButton.DblClickAsync();
        }

        public async Task<bool> IsConsignmentVisibleAsync(string consignmentName)
        {
            await _elements.SearchName.FillAsync(consignmentName);
            // Adjust wait/assert logic depending on your app’s behavior
            return await _elements.ConsignmentNameLink.IsVisibleAsync();
        }
    }
}
