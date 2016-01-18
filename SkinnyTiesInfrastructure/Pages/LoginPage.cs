using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace SkinnyTiesInfrastructure.Pages
{
    public class LoginPage : IApplicationPage
    {
        internal RemoteWebDriver Driver { get; private set; }
        internal string Url { get; private set; }

        public LoginPage(RemoteWebDriver driver)
        {
            Driver = driver;
            Url = "https://skinnyties.com/customer/account/login/";
        }

        public void LoadPage()
        {
            Driver.Url = Url;
        }

        public void EnterEmail(string email)
        {
            var emailTextField = Driver.FindElementById("email");
            emailTextField.SendKeys(email);
        }

        public void EnterPassword(string password)
        {
            var passwordTextField = Driver.FindElementById("pass");
            passwordTextField.SendKeys(password);
        }

        public void ClickSignInButton()
        {
            var btn = Driver.FindElementById("login-form").FindElement(By.TagName("button"));
            btn.Click();
        }

        public bool IsDisplayed()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(3));

            try
            {
                IWebElement element = wait.Until(ExpectedConditions.ElementExists(By.Id("account-login")));
                return element.Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
    }
}
