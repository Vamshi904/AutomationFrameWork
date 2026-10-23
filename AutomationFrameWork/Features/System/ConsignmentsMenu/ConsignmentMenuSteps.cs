using AutomationFrameWork.Drivers;
using AutomationFrameWork.Pages;
using AutomationFrameWork.PagesAndObjects.System;
using AutomationFrameWork.PagesAndObjects.System.ConsignmentsMenu;
using AutomationFrameWork.Utilities;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFrameWork.Features.System.ConsignmentsMenu
{
    [Binding]
    public class ConsignmentMenuSteps
    {
        private readonly TestContext _testContext;
        private readonly ConsignmentMenuPage _consignmentMenuPage;
        private readonly CommonActionsPage commonactionpage;

        public ConsignmentMenuSteps(PlaywrightDriver playwrightDriver, ScenarioContext scenarioContext)
        {
            if (playwrightDriver == null) throw new ArgumentNullException(nameof(playwrightDriver));
            if (scenarioContext == null) throw new ArgumentNullException(nameof(scenarioContext));

            _testContext = new TestContext(playwrightDriver.Page, scenarioContext);
            _consignmentMenuPage = new ConsignmentMenuPage(_testContext);
            commonactionpage = new CommonActionsPage(_testContext.Page);
        }

        [When("Click on the Consignments  menu")]
        public async Task WhenClickOnTheConsignmentsMenuAsync()
        {
            await _consignmentMenuPage.ClickConsignmentsMenuAsync();
        }

        [When("click on the Add new Consignments button")]
        public async Task WhenClickOnTheAddNewConsignmentsButtonAsync()
        {
            await _consignmentMenuPage.ClickAddConsignmentButtonAsync();
        }

        [When("fills in the Transition Point form with {string} {string} {string}")]
        public async Task WhenFillsInTheTransitionPointFormWithAsync(string name, string shortCode, string courierType)
        {
            await _consignmentMenuPage.FillTransitionPointFormAsync(name, shortCode, courierType);
        }

        [When("Click on the submit information button")]
        public async Task WhenClickOnTheSubmitInformationButtonAsync()
        {
            await _consignmentMenuPage.SubmitInformationAsync();
        }

        [Then("the new Consignments  should appear in the user list")]
        public async Task ThenTheNewConsignmentsShouldAppearInTheUserListAsync()
        {
            string successmessage = await commonactionpage.GetSuccessNotificationText();
            Console.WriteLine(successmessage);
            Assert.Contains("Consignment information has been successfully added.", successmessage);
        }
        [When("Fill the consignment name in the search field")]
        public async Task WhenFillTheConsignmentNameInTheSearchField()
        {
            await _consignmentMenuPage.IsConsignmentVisibleAsync("testing");
        }

        [Then("Then print the details in the excel")]
        public async Task ThenThenPrintTheDetailsInTheExcelAsync()
        {
            string safeDate = DateTime.Now.ToString("yyyyMMdd");
            string sheetName = $"ConsignmentMenu{safeDate}";

            await commonactionpage.PrintTableRecords(@"C:\Users\vannam\source\repos\AutomationFrameWork\AutomationFrameWork\TestData\ConsignmentMenu.xlsx", sheetName);
        }



    }
}
