using AutomationFrameWork.Utilities;
using System.Threading.Tasks;

namespace AutomationFrameWork.PagesAndObjects.System
{
    public class TrackerUserMenuPage
    {
        private readonly TestContext _testContext;
        private readonly TrackerUserMenuElements _elements;

        public TrackerUserMenuPage(TestContext testContext)
        {
            _testContext = testContext;
            _elements = new TrackerUserMenuElements(_testContext.Page);
        }

        public async Task ClickTrackerUserMenu()
        {
            await Task.Delay(500);
            await _elements.TrackerUserMenu.ClickAsync();
        }

        public async Task ClickAddTrackerUserAsync()
        {
            await Task.Delay(500);
            await _elements.AddTrackerUserbutton.ClickAsync();
        }

        public async Task FillTrackerUserFormAsync(string name, string email, string phone, String optionvalue)
        {

            await _elements.Name.FillAsync(name);
            await Task.Delay(500);
            await _elements.UserName.FillAsync(email);
            await Task.Delay(500);
            await _elements.password.FillAsync(phone);
            await Task.Delay(500);
            await _elements.SelectTransitionPointdropdown.SelectOptionAsync(optionvalue);
        }

        public async Task SubmitInformationAsync()
        {
            await Task.Delay(500);
            await _elements.SubmitInformationbutton.ClickAsync();
        }


        public async Task ClickNamelink()
        {
            await Task.Delay(500);
            await _elements.NameLink.ClickAsync();
        }


        public async Task ClickDeleteteicon()
        {
            await Task.Delay(500);
            await _elements.DeleteIcon.ClickAsync();
        }


        public async Task EditTrackerUserFormAsync(string name, string email, string phone, String optionvalue)
        {
            await _elements.Name.ClearAsync();
            await _elements.Name.FillAsync(name);
            await Task.Delay(500);
            await _elements.UserName.ClearAsync();
            await _elements.UserName.FillAsync(email);
            await Task.Delay(500);
            await _elements.password.ClearAsync();
            await _elements.password.FillAsync(phone);
            await Task.Delay(500);
            await _elements.SelectTransitionPointdropdown.SelectOptionAsync(optionvalue);
        }
        public async Task SearchField()
        {
            await _elements.SearchName.FillAsync("Test Altn User");
            await _elements.SearchName.PressAsync("Enter");
        }


    }
}
