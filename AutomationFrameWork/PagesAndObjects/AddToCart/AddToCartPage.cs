using AutomationFrameWork.PagesAndObjects.Login;
using AutomationFrameWork.Utilities;
using Microsoft.Playwright;
using ReqnRoll_Project_Demo.Objects;
using static Microsoft.Playwright.Assertions;

namespace ReqnRoll_Project_Demo.Pages
{
    public class AddToCartPage
    {
        private readonly TestContext _context;
        private readonly AddToCartObjects _a2cObjects;

        public AddToCartPage(TestContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _a2cObjects = new AddToCartObjects(_context.Page); // Page object tied to IPage
        }

        public async Task AddtoCartAsync()
        {
            await _a2cObjects.SauceLabsBackPack.ClickAsync();
            await _a2cObjects.SauceLabsBackPack_AddToCart.ClickAsync();
            await Expect(_a2cObjects.SauceLabsBackPack_RemoveBUtton).ToBeVisibleAsync();
            await _a2cObjects.SauceLabsBackPack_RemoveBUtton.IsEnabledAsync();
        }


    }
}
