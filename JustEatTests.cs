using Microsoft.Playwright;
using JustEatTestsProject.Pages;

namespace JustEatTestsProject
{
    public class JustEatTests
    {
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IPage _page;

        [SetUp]
        public async Task Setup()
        {
            // ARRANGE
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });

            // ACT
            _page = await _browser.NewPageAsync();
            await _page.GotoAsync("https://www.just-eat.co.uk/");
            await _page.Locator("[data-test-id='actions-necessary-only']").ClickAsync(); // Accept cookies (needed for TryOrderPizza test to work)
        }

        [TearDown]
        public async Task TearDown()
        {
            await _browser.CloseAsync();
            _playwright.Dispose();
        }

        [Test]
        public async Task NavigateToJustEatHomePage()
        {
            var homePage = new HomePage(_page);
            await homePage.VerifyTitle();
        }

        [Test]
        public async Task VerifyAddressSearchResults()
        {
            var homePage = new HomePage(_page);
            var searchResultsPage = new SearchResultsPage(_page);
            await homePage.VerifyTitle();
            await homePage.SearchLocation();
            await searchResultsPage.ShowSearchResults();
        }

        [Test]
        public async Task TryOrderPizza()
        {
            var homePage = new HomePage(_page);
            var searchResultsPage = new SearchResultsPage(_page);
            var restaurantPage = new RestaurantPage(_page);
            var loginPage = new LoginPage(_page);
            await homePage.VerifyTitle();
            await homePage.SearchLocation();
            await searchResultsPage.ShowSearchResults();
            await searchResultsPage.ChooseRestaurant();
            await restaurantPage.LoadRestaurant();
            await restaurantPage.AddFood();
            await restaurantPage.CheckoutOrder();
            await loginPage.LoadLoginPage();
            await loginPage.ConfirmCheckout();
        }

        [Test]
        public async Task FilterCuisine()
        {
            var homePage = new HomePage(_page);
            var searchResultsPage = new SearchResultsPage(_page);
            var restaurantPage = new RestaurantPage(_page);
            await homePage.SearchLocation();
            await searchResultsPage.ShowSearchResults();
            await restaurantPage.AddFilter();
            await restaurantPage.ConfirmFilter();
        }
    }
}