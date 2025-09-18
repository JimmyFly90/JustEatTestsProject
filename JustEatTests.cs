using Microsoft.Playwright;

namespace JustEatTestsProject
{
    public class JustEatTests
    {
        [Test]
        public async Task NavigateToJustEatHomePage()
        {
            // ARRANGE
            using var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });

            // ACT
            var page = await browser.NewPageAsync();
            await page.GotoAsync("https://www.just-eat.co.uk/");

            // ASSERT
            Assert.That(await page.TitleAsync(), Is.EqualTo("Order takeaway online from 30,000+ food delivery restaurants | Just Eat"));
            await browser.CloseAsync();
        }

        [Test]
        public async Task ShowSearchResultsTestville()
        {
            //ARRANGE 
            using var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });

            // ACT
            var page = await browser.NewPageAsync();
            await page.GotoAsync("https://www.just-eat.co.uk/");
            await Task.Delay(2000); // Wait for 2 seconds to ensure the page is fully loaded
            await page.Locator("[data-test-id='actions-necessary-only']").ClickAsync(); // Accept cookies
            await page.Locator("text='Search'").ClickAsync();
            await page.Locator(".VO0jE").FillAsync("AR511AA");
            await page.Locator("text='Testville, AR51 1AA'").ClickAsync();
            await page.Locator("text='Order from 3869 places'").WaitForAsync();

            // ASSERT
            Assert.That(await page.TitleAsync(), Is.EqualTo("Restaurants and takeaways in , Testville | Just Eat"));
            await browser.CloseAsync();
        }

        [Test]
        public async Task TryOrderPizza()
        {
            //ARRANGE 
            using var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });

            // ACT
            var page = await browser.NewPageAsync();
            await page.GotoAsync("https://www.just-eat.co.uk/");
            await page.Locator("[data-test-id='actions-necessary-only']").ClickAsync(); // Accept cookies
            await page.Locator("text='Search'").ClickAsync();
            await page.Locator(".VO0jE").FillAsync("AR511AA");
            await page.Locator("text='Testville, AR51 1AA'").ClickAsync();
            await page.Locator("text='Order from 3869 places'").WaitForAsync();
            await page.Locator("text='CrossSelling Pizzas Test - Do Not Edit'").ClickAsync();
            await page.Locator("._3wa4B:has-text('Pizzas')").ClickAsync();
            await page.Locator("text='Margherita'").ClickAsync();
            await page.Locator(".single-selection-style_text__EFHuZ:has-text('13\"')").ClickAsync();
            await page.Locator("text='Add'").ClickAsync();
            await page.Locator("text='Checkout ('").ClickAsync();
            await page.Locator("text='Log in or create account'").WaitForAsync();


            // ASSERT
            Assert.That(await page.TitleAsync(), Is.EqualTo("Just Eat | Sign in/Login")); // screen after checkout clicked
            await browser.CloseAsync();
        }
    }
}