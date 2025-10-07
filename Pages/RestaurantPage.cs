using Microsoft.Playwright;

namespace JustEatTestsProject.Pages
{
    public class RestaurantPage
    {
        private readonly IPage _page;
        private ILocator _restaurantMenu => _page.Locator("._3wa4B:has-text('Pizzas')");
        private ILocator _foodChoice => _page.Locator("text='Margherita'");
        private ILocator _foodSize => _page.Locator(".single-selection-style_text__EFHuZ:has-text('13\"')");
        private ILocator _addToOrderButton => _page.Locator("text='Add'");
        private ILocator _checkoutButton => _page.Locator("text='Checkout ('");
        private ILocator _filterCuisine => _page.Locator("[data-qa-id='alcohol']");
        

        public RestaurantPage(IPage page) { _page = page; }

        public async Task LoadRestaurant()
        {
            await _restaurantMenu.WaitForAsync();
        }

        public async Task AddFood()
        {
            await _restaurantMenu.ClickAsync();
            await _foodChoice.ClickAsync();
            await _foodSize.ClickAsync();
            await _addToOrderButton.ClickAsync();
        }

        public async Task CheckoutOrder()
        {
            await _checkoutButton.ClickAsync();
        }

        public async Task AddFilter()
        {
            await _filterCuisine.ClickAsync();
        }

        public async Task ConfirmFilter()
        {
            await Assertions.Expect(_page.Locator("[data-qa='list-all-open-title']")).ToContainTextAsync("Alcohol");
        }  
    }
}