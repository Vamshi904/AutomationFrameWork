using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Playwright;

public static class ScreenshotHelper
{
    public static async Task TakeScreenshotAsync(IPage page, string testName)
    {
        // Project root (same location as .csproj)
        
         var projectRoot = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.FullName;

        // Screenshots folder
        var screenshotFolder = Path.Combine(projectRoot, "Screenshots");
        Directory.CreateDirectory(screenshotFolder);

        // Timestamped filename
        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        var screenshotPath = Path.Combine(screenshotFolder, $"{testName}_{timestamp}.png");

        // Capture screenshot
        await page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = screenshotPath,
            FullPage = true
        });

        Console.WriteLine($"Screenshot saved: {screenshotPath}");
    }
}
