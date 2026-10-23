using Microsoft.Playwright;

public class PlaywrightModel
{
    // Root Playwright instance
    public IPlaywright PlaywrightInstance { get; set; }

    // Separate browser instances
    public IBrowser ChromiumBrowser { get; set; }
    public IBrowser FirefoxBrowser { get; set; }
    public IBrowser WebkitBrowser { get; set; }

    // Separate pages
    public IPage ChromiumPage { get; set; }
    public IPage FirefoxPage { get; set; }
    public IPage WebkitPage { get; set; }
}
