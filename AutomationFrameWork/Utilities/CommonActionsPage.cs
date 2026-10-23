using Microsoft.Playwright;
using System.IO;
using System.Text;
using System.Xml.Linq;
using AutomationFrameWork.Utilities;

namespace AutomationFrameWork.Pages
{
    public class CommonActionsPage
    {
        private readonly IPage _page;
        public CommonActionsPage(IPage page)
        {
            _page = page ?? throw new ArgumentNullException(nameof(page));
        }


        public async Task WaitAsync(float seconds)
        {
            if (seconds > 0)
            {
                await _page.WaitForTimeoutAsync(seconds * 1000);
            }
        }

        public void LogCurrentPageUrl()
        {
            var currentUrl = _page.Url;
            Console.WriteLine("** Navigated to the URL : " + currentUrl);
        }

        public static string GetProjectRoot()
        {
            var directory = new DirectoryInfo(AppContext.BaseDirectory);

            while (directory != null && !directory.GetFiles("*.csproj").Any())
            {
                directory = directory.Parent;
            }

            if (directory == null)
                throw new DirectoryNotFoundException("Could not find project root (.csproj)");

            return directory.FullName;
        }

        public static async Task<string> AcceptAlertAsync(IPage page)
        {
            var tcs = new TaskCompletionSource<string>(); page.Dialog += async (_, dialog) => {
                await dialog.AcceptAsync(); // Always click OK
                tcs.TrySetResult(dialog.Message);
            };
            return await tcs.Task; }
        public async Task<string> GetSuccessNotificationText()
        {
            var locator = _page.Locator("div.alert.alert-success"); //
                                                                    // adjust selector if needed
            return await locator.InnerTextAsync(); }

        public async Task PrintTableRecords1()
        {
            // Get all rows inside the table wrapper
            var records = await _page.Locator("//div[@id='tableWrapper']//tr").AllAsync();

            Console.WriteLine("=== Agent Commission Table Records ===");

            foreach (var row in records) { var cells = await row.Locator("td").AllInnerTextsAsync(); Console.WriteLine(string.Join(" | ", cells)); }

            Console.WriteLine("======================================");
        }

        public async Task PrintTableRecords(string filePath, string sheetName)
        {
            // Get all header cells from the table
            var headerCells = await _page.Locator("//div[@id='tableWrapper']//th").AllInnerTextsAsync();
            var headers = headerCells.Select(h => h.Trim()).ToList();

            var records = await _page.Locator("//div[@id='tableWrapper']//tr").AllAsync();
            var results = new List<Dictionary<string, string>>();

            foreach (var row in records.Skip(1)) // skip header row
            {
                var cells = await row.Locator("td").AllInnerTextsAsync();
                Console.WriteLine(string.Join(" | ", cells));

                var rowDict = new Dictionary<string, string>();
                for (int i = 0; i < headers.Count && i < cells.Count; i++)
                {
                    rowDict[headers[i]] = cells[i];
                }

                results.Add(rowDict);
            }

            Console.WriteLine("======================================");

            // Step 4: Call WriteResults to save into Excel
            if (results.Count > 0)
            {
                ExcelHelper.WriteResults(filePath, sheetName, results);
            }
        }

    }


}