using AutomationFrameWork.Utilities;
using AventStack.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFrameWork.PagesAndObjects.System.TransitionPointMenu
{
    public class TransitionPointPage
    {
        private readonly TestContext _testContext;
        private readonly TransitionPointPageElement _elements;

        public TransitionPointPage(TestContext testContext)
        {
            _testContext = testContext;
            _elements = new TransitionPointPageElement(_testContext.Page);
        }


        public async Task ClickTransitionPointMenu()
        {
            await Task.Delay(500);
            await _elements.TransitionpointMenu.ClickAsync();
        }

        public async Task ClickNewTransitionbutton()
        {
            await Task.Delay(500);
            await _elements.Transitionbutton.ClickAsync();
        }

        public async Task FilltheForms(string TransitionPointName, string UserType, string Office, string TransitionStatuses)
        {
            try
            {
                await Task.Delay(500);
                await _elements.Transitionpointname.ClearAsync();
                await _elements.Transitionpointname.FillAsync(TransitionPointName);
                await _elements.UsertypeDropdown.SelectOptionAsync(UserType);
                await _elements.UserOfficeDropdown.SelectOptionAsync(Office);
                await Task.Delay(500);
                if (!await _elements.SelectTransitionStatus.IsCheckedAsync())
                {
                    await _elements.SelectTransitionStatus.CheckAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        public async Task ClickSubmitbutton()
        {
            await Task.Delay(500);
            await _elements.SubmitInformationbutton.ClickAsync();
        }

        public async Task ClickTransitionPointNamelink()
        {
            await Task.Delay(500);
            await _elements.NameLink.ClickAsync();
        }

        public async Task SearchField(string name)
        {
            await Task.Delay(500);
            await _elements.SearchName.FillAsync(name);
            await _elements.SearchName.PressAsync("Enter"); 

        }






    }
}
