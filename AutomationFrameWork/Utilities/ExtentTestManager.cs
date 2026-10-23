using AventStack.ExtentReports;

namespace AutomationFrameWork.Utilities
{
    public static class ExtentTestManager
    {
        [ThreadStatic]
        private static ExtentTest _test;

        public static ExtentTest Test
        {
            get => _test;
            set => _test = value;
        }
    }
}
