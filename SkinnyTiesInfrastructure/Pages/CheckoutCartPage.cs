using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace SkinnyTiesInfrastructure.Pages
{
    public class CheckoutCartPage : IApplicationPage
    {
        internal RemoteWebDriver Driver { get; private set; }
        internal string Url { get; private set; }

        public CheckoutCartPage(RemoteWebDriver driver)
        {
            Driver = driver;
            Url = "http://skinnyties.com/checkout/cart/";
        }

        public void LoadPage()
        {
            Driver.Url = Url;
        }

        public void UpdateQuantityOfItem(string itemName, int newQuantity)
        {
            var cartTable = Driver.FindElementById("cart-table");
            var tableBody = cartTable.FindElement(By.TagName("tbody"));
            var tableRows = tableBody.FindElements(By.TagName("tr"));

            foreach (var row in tableRows)
            {
                var productName = row.FindElement(By.ClassName("product-name"));

                if (productName.Text.Contains(itemName))
                {
                    var quantityTextField = row.FindElement(By.TagName("input"));
                    quantityTextField.SendKeys(Keys.Backspace);
                    quantityTextField.SendKeys(newQuantity.ToString());
                    break;
                }
            }
        }

        public void ClickUpdateQuantityButton()
        {
            var cartTable = Driver.FindElementById("cart-table");
            var tableFoot = cartTable.FindElement(By.TagName("tfoot"));
            var updateButton = tableFoot.FindElement(By.Name("update_cart_action"));

            updateButton.Click();
        }

        public bool QuantityEquals(int num)
        {
            var cartTable = Driver.FindElementById("cart-table");
            var tableBody = cartTable.FindElement(By.TagName("tbody"));
            var tableRows = tableBody.FindElement(By.TagName("tr"));

            var tableData = tableRows.FindElements(By.TagName("td"));
            var unitPrice = tableData[2].FindElement(By.ClassName("price")).Text.Substring(1);
            var totalPrice = tableData[3].FindElement(By.ClassName("price")).Text.Substring(1);

            return (Convert.ToDouble(totalPrice) / Convert.ToDouble(unitPrice)).Equals(num);
        }

        public bool IsDisplayed()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(3));

            try
            {
                IWebElement element = wait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div/div[2]/div[1]/div/h1")));
                return element.Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
    }
}
