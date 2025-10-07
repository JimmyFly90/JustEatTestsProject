using Microsoft.Playwright;

namespace JustEatTestsProject.Pages
{
    public class HomePage
    {
        private readonly IPage _page;
        private ILocator _searchButton => _page.GetByRole(AriaRole.Button, new() { Name = "Search" });
        private ILocator _addressInput => _page.GetByPlaceholder("Full Address");
        private ILocator _expectedAddress => _page.GetByText("Testville, AR51 1AA");

        public HomePage(IPage page) { _page = page; }
        public async Task VerifyTitle()
        {
            await Assertions.Expect(_page).ToHaveTitleAsync("Order takeaway online from 30,000+ food delivery restaurants | Just Eat");
        }

        public async Task SearchLocation()
        {
            await _searchButton.ClickAsync();
            await _addressInput.FillAsync("AR51 1AA");
            await _expectedAddress.ClickAsync();
        }
        
    }
}
