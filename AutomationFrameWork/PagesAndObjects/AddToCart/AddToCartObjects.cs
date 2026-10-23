using Microsoft.Playwright;

namespace ReqnRoll_Project_Demo.Objects
{
    public class AddToCartObjects
    {
        private readonly IPage _page;
        public AddToCartObjects(IPage page)
        {
            _page = page;
        }

        public ILocator SauceLabsBackPack => _page.Locator("//a[@data-test='item-4-title-link']//div[text()='Sauce Labs Backpack']");
        public ILocator SauceLabsBackPack_AddToCart => _page.Locator("[data-test='add-to-cart']");
        public ILocator SauceLabsBackPack_RemoveBUtton => _page.Locator("//div[@data-test='inventory-item-price']//following-sibling::button[text()='Remove']");
        public ILocator ShopingCartLink=> _page.Locator("//a[@data-test='shopping-cart-link']/span");

    }
}
