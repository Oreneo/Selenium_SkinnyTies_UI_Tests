using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace SkinnyTiesInfrastructure.Pages
{
    public class HomePage : IApplicationPage
    {
        internal RemoteWebDriver Driver { get; private set; }
        internal string Url { get; private set; }

        public HomePage(RemoteWebDriver driver)
        {
            Driver = driver;
            Url = "http://skinnyties.com/";
        }

        public void LoadPage()
        {
            Driver.Url = Url;
        }

        public void ClickSignInButton()
        {
            var signInButton = Driver.FindElementById("header-account").FindElement(By.TagName("a"));
            signInButton.Click();
        }

        public bool IsDisplayed()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(3));

            try
            {
                IWebElement element = wait.Until(ExpectedConditions.ElementExists(By.ClassName("home-hero")));
                return element.Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
    }
}
