using AutomationFrameWork.PagesAndObjects.Login;
using AutomationFrameWork.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFrameWork.PagesAndObjects.System
{
    public class LocationPage
    {
        private readonly TestContext _testContext;
        private readonly LocationPageElements LocationPageElement;

        public LocationPage(TestContext testContext)
        {
            _testContext = testContext;
            LocationPageElement = new LocationPageElements(_testContext.Page);
        }
        public async Task ClickLocationMenu()
        {
           //wait LocationPageElement.Locatormenu.HoverAsync();
            await Task.Delay(2000);
            await LocationPageElement.Locatormenu.ClickAsync();

        }
        public async Task ClickAddLocationButton()
        {

            await Task.Delay(2000);
            await LocationPageElement.AddLocationButton.ClickAsync();
        }

        public async Task FillLocationForm(string locationName, string address, string status)
        {
            await LocationPageElement.LocationName.FillAsync(locationName);
            await Task.Delay(2000);
            await LocationPageElement.ShortCode.FillAsync(address);
            await Task.Delay(2000);
            await LocationPageElement.SelectOfficedropdown.SelectOptionAsync(status);

        }
        public async Task SubmitLocation()
        {
            await LocationPageElement.Submitinformationbutton.HoverAsync();
            await Task.Delay(2000);
            await LocationPageElement.Submitinformationbutton.ClickAsync();

        }
        //Delete functionality step 
        public async Task ClickDeleteicon()
        {
            await LocationPageElement.Deleteicon.ClickAsync();


        }

        public async Task ClickLocationName()
        {
            await Task.Delay(2000);

            await LocationPageElement.LocationNamelink.ClickAsync();  
        }

        public async Task ClickExportExcelbutton()
        {

            await LocationPageElement.ExportExcelButton.HoverAsync();
            await Task.Delay(2000);
            await LocationPageElement.ExportExcelButton.ClickAsync();


        }

    }
}