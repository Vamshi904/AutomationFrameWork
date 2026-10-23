using AutomationFrameWork.PagesAndObjects.System;
using AutomationFrameWork.Utilities;

namespace AutomationFrameWork.Pages
{
    public class TransitionStatusPage
    {
        private readonly TransitionStatusPageElements _elements;
        private readonly TestContext _testContext;


        public TransitionStatusPage(TestContext testContext)
        {
            _testContext = testContext;

            _elements = new TransitionStatusPageElements(_testContext.Page);
        }

        public async Task ClickTransitionStatusMenuAsync()
        {
            await _elements.TransitionStatusMenu.ClickAsync();
        }

        public async Task ClickAddNewStatusButtonAsync()
        {
            await _elements.AddNewStatusButton.ClickAsync();
        }

        public async Task FillTransitionStatusFormAsync(string name, string userType, string repeatStatus, string consignmentStatus)
        {
            await _elements.TransitionStatusName.ClearAsync();
            await _elements.TransitionStatusName.FillAsync(name);
            await _elements.SelectUsertypeDropdown.SelectOptionAsync(userType);
            await _elements.StatusBeUpdatedDropdown.SelectOptionAsync(repeatStatus);
            await _elements.IsConsignmentStatusDropdown.SelectOptionAsync(consignmentStatus);
        }

        public async Task ClickSubmitInformationButtonAsync()
        {
            await _elements.SubmitInformationbutton.ClickAsync();
        }

        public async Task ClickNameLinkAsync()
        {
            await _elements.NameLink.ClickAsync();
        }

        public async Task ClickDeleteIconAsync()
        {
            await _elements.DeleteIcon.ClickAsync();
        }
    }
}
