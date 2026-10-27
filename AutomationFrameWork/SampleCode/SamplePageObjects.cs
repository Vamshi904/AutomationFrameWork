using Microsoft.Playwright;

namespace AutomationFrameWork.SampleCode
{
    public class SamplePageObjects
    {

        private readonly IPage _page;
        public SamplePageObjects(IPage page)
        {
            _page = page;
        }

        
    }
}
