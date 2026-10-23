using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace AutomationFrameWork.PagesAndObjects.System
{
    public class LocationPageElements
    {

        private readonly IPage _page;

        public LocationPageElements(IPage page)
        {
            _page = page;
        }

        public ILocator Locatormenu => _page.Locator("(//a [normalize-space()='Locations'])[1]");

        public ILocator AddLocationButton => _page.Locator("//a[contains(@class,'btn btn-primary')]");


        public ILocator LocationName => _page.Locator("//input[@id='locationname']");

        public ILocator ShortCode => _page.Locator("//input[@id='shortcodename']");


        public ILocator SelectOfficedropdown => _page.Locator("//select[@id='officeid']");


        public ILocator Deleteicon => _page.Locator("(//td[@class='text-center']//a)[1]");


        public ILocator LocationNamelink => _page.Locator("(//a[@class='text-decoration-none text-primary fw-medium'])[1]");

        public ILocator ExportExcelButton => _page.Locator("//a[@class='btn btn-success btn-sm']");

        public ILocator Submitinformationbutton => _page.Locator("//button[contains(@class,'btn bg-primary')]");








    }
}
