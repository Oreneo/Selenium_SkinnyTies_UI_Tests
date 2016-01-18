using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace SkinnyTiesInfrastructure.Pages
{
    public class TieItemPage : IApplicationPage
    {
        internal RemoteWebDriver Driver { get; private set; }
        internal string OliveGreenTieUrl { get; private set; }
        internal string GreenCottonTieUrl { get; private set; }

        public TieItemPage(RemoteWebDriver driver)
        {
            Driver = driver;
            OliveGreenTieUrl = "http://skinnyties.com/2-inch-beige-tan-olive-green-and-burgundy-wool-usa-skinny-tie";
            GreenCottonTieUrl = "http://skinnyties.com/2-inch-graay-peach-green-cotton-plaid-skinny-tie-made-in-the-usa";
        }

        public void LoadPage(string tieName)
        {
            if(tieName.Contains("Olive Green"))
            {
                Driver.Url = OliveGreenTieUrl;
            }

            if (tieName.Contains("Green Cotton"))
            {
                Driver.Url = GreenCottonTieUrl;
            }
        }

        public void AddToQuantity(int num)
        {
            var quantityIncreaseButton = Driver.FindElementById("qty-increase");

            for (int i = 0; i < num; i++)
            {
                quantityIncreaseButton.Click();
            }
        }

        public void ClickAddToCartButton()
        {
            var addToCartDiv = Driver.FindElementByClassName("add-to-cart");
            var addToCartButton = addToCartDiv.FindElement(By.TagName("button"));
            addToCartButton.Click();
        }

        public bool IsDisplayed()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(3));

            try
            {
                IWebElement element = wait.Until(ExpectedConditions.ElementExists(By.ClassName("hero-image")));
                return element.Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
    }
}
