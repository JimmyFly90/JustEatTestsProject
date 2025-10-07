using Microsoft.Playwright;

namespace JustEatTestsProject.Pages
{
    public class SearchResultsPage
    {
        private readonly IPage _page;
        private ILocator _listResultsTitle => _page.Locator("[data-qa='list-all-open-title']");

        private ILocator _restaurant => _page.GetByText("CrossSelling Pizzas Test - Do Not Edit");

        public SearchResultsPage(IPage page) {  _page = page; }
        
        public async Task LoadSearchResults()
        {
            await _listResultsTitle.WaitForAsync();
        }
        public async Task ShowSearchResults()
        {
            await Assertions.Expect(_page).ToHaveTitleAsync("Restaurants and takeaways in , Testville | Just Eat");
        }

        public async Task ChooseRestaurant()
        {
            await _restaurant.ClickAsync();
        }
    }


}

