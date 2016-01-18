using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace SkinnyTiesInfrastructure.Pages
{
    public class AccountOverviewPage : IApplicationPage
    {
        internal RemoteWebDriver Driver { get; private set; }
        internal string Url { get; private set; }

        public AccountOverviewPage(RemoteWebDriver driver)
        {
            Driver = driver;
            Url = "https://skinnyties.com/customer/account/login/";
        }

        public void LoadPage()
        {
            Driver.Url = Url;
        }

        public bool IsDisplayed()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(3));

            try
            {
                IWebElement element = wait.Until(ExpectedConditions.ElementExists(By.Id("dashboard-hello")));
                return element.Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
    }
}
