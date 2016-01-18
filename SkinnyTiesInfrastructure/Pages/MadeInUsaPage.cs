using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace SkinnyTiesInfrastructure.Pages
{
    public class MadeInUsaPage : IApplicationPage
    {
        internal RemoteWebDriver Driver { get; private set; }
        internal string Url { get; private set; }

        public MadeInUsaPage(RemoteWebDriver driver)
        {
            Driver = driver;
            Url = "http://skinnyties.com/collection/made-in-usa/";
        }

        public void LoadPage()
        {
            Driver.Url = Url;
        }

        public void ClickItem(string itemName)
        {
            var productContainer = Driver.FindElement(By.ClassName("product-grid"));

            var listItems = productContainer.FindElements(By.TagName("li"));

            foreach (var item in listItems)
            {
                var details = item.FindElement(By.ClassName("product-name"));

                if(details.Text.Contains(itemName))
                {
                    details.Click();
                    break;
                }
            }
        }

        public bool IsDisplayed()
        {
            try
            {
                var category = Driver.FindElementById("category-landing");
                var header = category.FindElement(By.TagName("h1"));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
