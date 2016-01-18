using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace SkinnyTiesInfrastructure.Pages
{
    public class CollectionsPage : IApplicationPage
    {
        internal RemoteWebDriver Driver { get; private set; }
        internal string Url { get; private set; }

        public CollectionsPage(RemoteWebDriver driver)
        {
            Driver = driver;
            Url = "http://skinnyties.com/collection/";
        }

        public void LoadPage()
        {
            Driver.Url = Url;
        }

        public void ClickTab(string tabName)
        {
            var matterDiv = Driver.FindElementById("matter");

            var categoryList = matterDiv.FindElement(By.ClassName("category-list"));
            
            var listItems = categoryList.FindElements(By.TagName("li"));

            foreach (var item in listItems)
            {
                var requestedTab = item.FindElement(By.ClassName("category-name"));

                if (requestedTab.Text == tabName)
                {
                    requestedTab.Click();
                    break;
                }
            }
        }

        public bool IsDisplayed()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(3));

            try
            {
                IWebElement element = wait.Until(ExpectedConditions.ElementExists(By.Id("category-landing")));
                return element.Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
    }
}
