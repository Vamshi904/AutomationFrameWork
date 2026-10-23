using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AutomationFrameWork.Pages;

namespace AutomationFrameWork.Utilities
{
    public static class ExtentReportManager
    {
        private static ExtentReports _extentReports;
        private static readonly object SyncLock = new();

        public static ExtentReports GetExtentReport()
        {
            if (_extentReports == null)
            {
                lock (SyncLock)
                {
                    if (_extentReports == null)
                    {
                        // ✅ Reusing CommonActionsPage method
                        var projectRoot = CommonActionsPage.GetProjectRoot();

                        var reportsDirectory = Path.Combine(projectRoot, "Reports");
                        Directory.CreateDirectory(reportsDirectory);

                        var reportFilePath = Path.Combine(
                            reportsDirectory,
                            $"ExtentReport_{DateTime.Now:yyyyMMdd_HHmmss}.html"
                        );

                        var sparkReporter = new ExtentSparkReporter(reportFilePath);

                        sparkReporter.Config.ReportName = "Reqnroll Automation Report";
                        sparkReporter.Config.DocumentTitle = "Test Execution Report";
                        sparkReporter.Config.Theme =
                            AventStack.ExtentReports.Reporter.Config.Theme.Standard;

                        _extentReports = new ExtentReports();
                        _extentReports.AttachReporter(sparkReporter);
                    }
                }
            }

            return _extentReports;
        }
    }
}
