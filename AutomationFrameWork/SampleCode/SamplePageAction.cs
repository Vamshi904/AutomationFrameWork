using System.Text.RegularExpressions;
using AutomationFrameWork.Drivers;
using AutomationFrameWork.Utilities;
using Microsoft.Playwright;

namespace AutomationFrameWork.SampleCode
{
    public class SamplePageAction : SamplePageObjects
    {
        private readonly TestContext _testContext;

        public SamplePageAction(TestContext context, IPage page) : base(page)
        {
            _testContext = context;
        }


    }
}
