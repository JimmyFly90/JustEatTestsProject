using Microsoft.Playwright;

namespace JustEatTestsProject.Pages
{
    public class LoginPage
    {
        private readonly IPage _page;
        private ILocator _loginPage => _page.Locator("text='Log in or create account'");


        public LoginPage(IPage page) { _page = page; }

        public async Task LoadLoginPage()
        {
            await _loginPage.WaitForAsync();
        }

        public async Task ConfirmCheckout()
        {
            await Assertions.Expect(_page).ToHaveTitleAsync("Just Eat | Sign in/Login");
        }
    }
}

