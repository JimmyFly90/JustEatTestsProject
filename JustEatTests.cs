using Microsoft.Playwright;

namespace JustEatTestsProject
{
    public class JustEatTests
    {
        private readonly string address = "AR511AA";
        private readonly string expectedAddress = "Testville, AR51 1AA";
        private readonly string category = "Alcohol";

        private async Task <(IPage, IBrowser)> Setup()
        {
            // ARRANGE
            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });

            // ACT
            var page = await browser.NewPageAsync();
            await page.GotoAsync("https://www.just-eat.co.uk/");
            await page.Locator("[data-test-id='actions-necessary-only']").ClickAsync(); // Accept cookies

            return (page, browser);
        }

        [Test]
        public async Task NavigateToJustEatHomePage()
        {
            var (page, browser) = await Setup();

            // ASSERT
            Assert.That(await page.TitleAsync(), Is.EqualTo("Order takeaway online from 30,000+ food delivery restaurants | Just Eat"));
            await browser.CloseAsync();
        }

        [Test]
        public async Task ShowAddressSearchResults()
        {
            // ARRANGE
            var (page, browser) = await Setup();

            // ACT
            await page.Locator("text='Search'").ClickAsync();
            await page.Locator(".VO0jE").FillAsync(address);
            await page.Locator($"text='{expectedAddress}'").ClickAsync();
            await page.Locator("text='Order from 3871 places'").WaitForAsync();

            // ASSERT
            Assert.That(await page.TitleAsync(), Is.EqualTo("Restaurants and takeaways in , Testville | Just Eat"));
            await browser.CloseAsync();
        }

        [Test]
        public async Task TryOrderPizza()
        {
            // ARRANGE
            var (page, browser) = await Setup();

            // ACT
            await page.GetByPlaceholder("Full address").FillAsync(address);
            await page.Locator($"text='{expectedAddress}'").ClickAsync();
            await page.GetByText("CrossSelling Pizzas Test - Do Not Edit").ClickAsync();
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

        [Test]
        public async Task FilterCuisine()
        {
            //ARRANGE 
            var (page, browser) = await Setup();

            // ACT
            await page.GetByPlaceholder("Full address").FillAsync(address);
            await page.Locator($"text='{expectedAddress}'").ClickAsync();
            await page.Locator($"text='{category}'").ClickAsync();

            // ASSERT
            Assert.That(await page.Locator("[data-qa='list-all-open-title']").InnerTextAsync(), Does.Contain($"{category}"));
            await browser.CloseAsync();
        }
    }
}