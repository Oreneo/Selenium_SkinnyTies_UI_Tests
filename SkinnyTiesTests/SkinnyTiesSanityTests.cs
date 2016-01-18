using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkinnyTiesInfrastructure;
using SkinnyTiesInfrastructure.Pages;

namespace SkinnyTiesTests
{
    [TestClass]
    public class SkinnyTiesSanityTests
    {
        public PageFactory PageFactory { get; private set; }
        public HomePage HomePage { get; private set; }
        public LoginPage LoginPage { get; private set; }
        public AccountOverviewPage AccountOverviewPage { get; private set; }
        public CollectionsPage CollectionsPage { get; private set; }
        public NavigationBarPage NavigationBarPage { get; private set; }
        public CheckoutCartPage CheckoutCartPage { get; private set; }
        public MadeInUsaPage MadeInUsaPage { get; private set; }
        public TieItemPage TieItemPage { get; private set; }

        [TestMethod]
        public void LoginWithValidUserNameShouldSucceed()
        {
            HomePage.LoadPage();
            HomePage.ClickSignInButton();
            Assert.IsTrue(LoginPage.IsDisplayed());

            LoginPage.EnterEmail("TestUser1@codevalue.net");
            LoginPage.EnterPassword("123456");
            LoginPage.ClickSignInButton();

            Assert.IsTrue(AccountOverviewPage.IsDisplayed());
        }

        [TestMethod]
        public void LoginWithInvalidUserNameShouldFail()
        {
            HomePage.LoadPage();
            Assert.IsTrue(HomePage.IsDisplayed());
            HomePage.ClickSignInButton();
            Assert.IsTrue(LoginPage.IsDisplayed());

            LoginPage.EnterEmail("TestUser133@codevalue.net");
            LoginPage.EnterPassword("123456");
            LoginPage.ClickSignInButton();

            Assert.IsFalse(AccountOverviewPage.IsDisplayed());
        }

        [TestMethod]
        public void AddTieToCart_VerifyCorrectQuantity()
        {
            HomePage.LoadPage();
            NavigationBarPage.ClickTab("Collection");
            CollectionsPage.ClickTab("Made in USA");

            MadeInUsaPage.ClickItem("Green Cotton");
            TieItemPage.ClickAddToCartButton();
            Assert.IsTrue(CheckoutCartPage.QuantityEquals(1));
        }

        // ### Meaningful test 
        // Click on 'Collection' tab, Click on 'Made in USA' tab
        // Click on '2" Beige, Tan, Olive Green, and Burgundy Wool Skinny Tie. Made in the USA'
        // Update quantity to 4, Click Add to cart
        // Click 'Update quantity', Change to 3
        // Click checkout, verify 3 in cart.
        [TestMethod]
        public void AddFourOliveGreenTiesToCart_UpdateToThree_VerifyQuantity()
        {
            HomePage.LoadPage();
            NavigationBarPage.ClickTab("Collection");
            Assert.IsTrue(CollectionsPage.IsDisplayed());

            CollectionsPage.ClickTab("Made in USA");

            MadeInUsaPage.ClickItem("Olive Green");
            Assert.IsTrue(TieItemPage.IsDisplayed());

            TieItemPage.AddToQuantity(3);
            TieItemPage.ClickAddToCartButton();
            Assert.IsTrue(CheckoutCartPage.IsDisplayed());

            CheckoutCartPage.UpdateQuantityOfItem("Olive Green", 3);
            CheckoutCartPage.ClickUpdateQuantityButton();
            Assert.IsTrue(CheckoutCartPage.QuantityEquals(3));
        }

        [TestInitialize]
        public void TestInit()
        {
            PageFactory = new PageFactory();
            HomePage = PageFactory.GetPage<HomePage>(typeof(HomePage));
            LoginPage = PageFactory.GetPage<LoginPage>(typeof(LoginPage));
            AccountOverviewPage = PageFactory.GetPage<AccountOverviewPage>(typeof(AccountOverviewPage));
            CollectionsPage = PageFactory.GetPage<CollectionsPage>(typeof(CollectionsPage));
            NavigationBarPage = PageFactory.GetPage<NavigationBarPage>(typeof(NavigationBarPage));
            CheckoutCartPage = PageFactory.GetPage<CheckoutCartPage>(typeof(CheckoutCartPage));
            MadeInUsaPage = PageFactory.GetPage<MadeInUsaPage>(typeof(MadeInUsaPage));
            TieItemPage = PageFactory.GetPage<TieItemPage>(typeof(TieItemPage));
        }

        [TestCleanup]
        public void TestCleanup()
        {
            PageFactory.ClosePage();
        }
    }
}
