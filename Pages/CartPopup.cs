﻿using System;
 using System.Collections.Generic;
 using System.Collections.ObjectModel;
 using System.Linq;
 using NUnit.Framework;
 using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestProject.Pages
{
    public class CartPopup : BasePage
    {
        
        private IWebDriver driver;
        private IWait<IWebDriver> wait;

        public CartPopup(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(10));
        }

        private readonly By _productNames = By.CssSelector("a.cart-modal__title");
        private readonly By _closeModalButton = By.ClassName("js-svg-close");
        private readonly By _cartTotalPriceTitle = By.ClassName("cart-modal__check-digits");
        private readonly By _createOrderButton = By.ClassName("cart-modal__check-button");

        
        public void CloseModalWindow()
        {
            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(_closeModalButton));
            element.Click();
        }

        private ReadOnlyCollection<IWebElement> GetProductsInCart()
        {
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(_productNames));
        }

        private double GetTotalPrice()
        {
            var price = driver.FindElement(_cartTotalPriceTitle).Text;
            return ParseProductPrice(price);
        }

        public void ClickCreateOrder()
        {
            driver.FindElement(_createOrderButton).Click();
        }

        public void CheckProductsInCartAreExpected(List<Product> boughtProducts)
        {
            var productsInCart = GetProductsInCart();
            foreach (var cartProduct in productsInCart)
            {
                var addedProduct = boughtProducts[productsInCart.IndexOf(cartProduct)];
                var cartProductName = cartProduct.Text;
                Assert.AreEqual(cartProductName, addedProduct.Name,
                    "Added product name doesn't match displayed in cart");
                Assert.True(cartProductName.Contains(addedProduct.Color));
            }
        }

        public void CheckTotalPriceIsExpected(List<Product> boughtProducts)
        {
            var addedProductsSum = boughtProducts.Sum(product => product.Price);
            var cartTotalPrice = GetTotalPrice();
            Assert.AreEqual(addedProductsSum, cartTotalPrice,
                "Sum of product prices doesn't match total price from Cart");

        }
    }
}