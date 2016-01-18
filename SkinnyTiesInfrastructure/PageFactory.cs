using System;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace SkinnyTiesInfrastructure
{
    public class PageFactory
    {
        internal RemoteWebDriver Driver { get; private set; }

        public PageFactory()
        {
            Driver = new FirefoxDriver();
            Driver.Manage().Window.Maximize();
        }

        public T GetPage<T>(Type pageType)
        {
            return (T)Activator.CreateInstance(pageType, Driver);
        }

        public void ClosePage()
        {
            Driver.Close();
        }
    }
}
