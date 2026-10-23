using AutomationFrameWork.Utilities;

namespace AutomationFrameWork.PagesAndObjects.System
{
    public class DispatchLocationPage
    {
        private readonly TestContext _testContext;
        private readonly DispatchLocationPageElements _elements;

        public DispatchLocationPage(TestContext testContext)
        {
            _testContext = testContext;
            _elements = new DispatchLocationPageElements(_testContext.Page);
        }

        //Add Dispatch Location details method
        public async Task ClickDispatchLocationsMenu()
        {
            await _elements.DispatchLocationsMenu.ClickAsync();
        }

        public async Task ClickAddDispatchLocationButton()
        {
            await _elements.AddDispatchLocationButton.ClickAsync();
        }

        public async Task FillDispatchLocationForm(string locationName, string agentName)
        {
            await _elements.DispatchLocationInput.FillAsync(locationName);
            await _elements.CorrespondingAgentDropdown.SelectOptionAsync(new[] { agentName });
        }

        public async Task SubmitDispatchLocation()
        {
            await Task.Delay(2000);

            await _elements.SubmitInformationButton.ClickAsync();
        }

        //Edit Methods 
        public async Task ClickDispatchLocationName()
        {
            await Task.Delay(2000);
            await _elements.DispathchLocationName.ClickAsync();

        }


        public async Task EditDispatchLocationDetails(string locationName, string agentName)
        {
            await _elements.DispatchLocationInputField.ClearAsync();
            await Task.Delay(2000);
            await _elements.DispatchLocationInputField.FillAsync(locationName);
            await _elements.CorrespondingAgentDropdown.SelectOptionAsync(new[] { agentName });


        }

        public async Task ClickDeleteicon()
        {
            await Task.Delay(2000);

            await _elements.Deleteicon.ClickAsync();
        }



    }
}
