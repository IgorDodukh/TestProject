using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using TestProject.Pages;

namespace TestProject.Tests
{
    [TestFixture]
    public class TestClass
    {
        private const string SearchRequest = "iphone xs max 512";
        private const string MainUrl = "https://my.rozetka.com.ua/";

        private IWait<IWebDriver> wait;
        private static IWebDriver _driver;
        private AppUser _user;


        [OneTimeSetUp]
        public void Setup()
        {
            _user = new AppUser();
            var options = new ChromeOptions();
            options.AddArguments("--disable-notifications");
            _driver = new ChromeDriver(options);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(MainUrl);
            wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void TestCreateOrderWithTwoPhones()
        {
            var userMenuPop = new UserMenuPopup(_driver);
            userMenuPop.CloseBanner();
            userMenuPop.OpenUserMenu();
            userMenuPop.OpenSignUpPage();

            var signUpPage = new SignUpPage(_driver);
            signUpPage.FillUserData(_user);
            signUpPage.ClickSignUpButton();

            var searchPage = new SearchPage(_driver);
            searchPage.SendSearchRequest(SearchRequest);
            searchPage.ClickOnFirstFoundProductColor();

            var productPage = new ProductPage(_driver);
            var boughtProducts = productPage.BuyTwoProductsDifferentColor();
            boughtProducts.Reverse();

            var cartPopup = new CartPopup(_driver);
            cartPopup.CheckProductsInCartAreExpected(boughtProducts);
            cartPopup.CheckTotalPriceIsExpected(boughtProducts);
            cartPopup.ClickCreateOrder();

            var orderCreationPage = new OrderCreationPage(_driver);
            orderCreationPage.EnterOrderDetails(_user);
            orderCreationPage.SelectDeliveryMethod("курьер Новая Почта");
            orderCreationPage.EnterDeliveryInfo(_user);
            
        }

        [OneTimeTearDown]
        public void Close()
        {
            _driver.Close();
        }
        
    }
}