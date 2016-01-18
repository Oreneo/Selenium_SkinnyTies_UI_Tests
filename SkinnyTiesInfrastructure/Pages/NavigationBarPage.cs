using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace SkinnyTiesInfrastructure.Pages
{
    public class NavigationBarPage : IApplicationPage
    {
        internal RemoteWebDriver Driver { get; private set; }
        internal string Url { get; private set; }

        public NavigationBarPage(RemoteWebDriver driver)
        {
            Driver = driver;
            Url = "https://skinnyties.com/customer/account/login/";
        }
        
        public void LoadPage()
        {
            Driver.Url = Url;
        }

        public void ClickTab(string tabName)
        {
            var navigationDiv = Driver.FindElementById("nav");

            var navCollection = navigationDiv.FindElement(By.ClassName("nav-primary"));

            var navListItems = navCollection.FindElements(By.TagName("li"));

            foreach (var item in navListItems)
            {
                var requestedItem = item.FindElement(By.TagName("a"));

                if (requestedItem.Text.Equals(tabName))
                {
                    requestedItem.Click();
                    break;
                }
            }
        }

        public bool IsDisplayed()
        {
            return true;
        }
    }
}
