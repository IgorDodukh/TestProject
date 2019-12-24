﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestProject.Pages
{
    public class ProductPage : BasePage
    {
        private IWebDriver driver;
        private IWait<IWebDriver> wait;

        public ProductPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(10));
        }
        
        private readonly By _productTitle = By.ClassName("product__title");
        private readonly By _selectedColorTitle = By.ClassName("product-main__color_state_active");
        private readonly By _priceTitle = By.ClassName("product-prices__big");
        private readonly By _buyButton = By.CssSelector(".product-trade .buy-button");
        private readonly By _colorsIconsList = By.ClassName("product-main__color");
        private readonly By _statusTitle = By.ClassName("product__status");

        private string GetProductName()
        {
            return driver.FindElement(_productTitle).Text;
        }

        /**
         * Color parsing allows to handle products with the same color.
         * E.g. Gray and Space Gray.
         */
        private string GetSelectedColor()
        {
            var colorName = driver.FindElement(_selectedColorTitle).Text;
            var parsedColor = colorName.Split();
            return parsedColor.Length > 1 ? parsedColor[1] : parsedColor[0];
        }

        private double GetPrice()
        {
            var price = driver.FindElement(_priceTitle).Text;
            return ParseProductPrice(price);
        }

        private void ClickBuyButton()
        {
            wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                    _buyButton)).Click();
        }

        private string GetStatus()
        {
            return driver.FindElement(_statusTitle).Text;
        }

        private IEnumerable<IWebElement> GetAvailableColors()
        {
            // return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(_colorsIconsList));
            return driver.FindElements(_colorsIconsList);
            
        }

        private Product BuyProduct()
        {
            var productPage = new ProductPage(driver);
            var productName = productPage.GetProductName();
            var firstProductColor = productPage.GetSelectedColor();
            var firstProductPrice = productPage.GetPrice();
            var product1 = new Product(productName, firstProductColor, firstProductPrice);
            productPage.ClickBuyButton();
            return product1;
        }

        public List<Product> BuyTwoProductsDifferentColor()
        {
            var boughtProducts = new List<Product>();
            var product1 = BuyProduct();
            boughtProducts.Add(product1);
            
            var cartPopup = new CartPopup(driver);
            cartPopup.CloseModalWindow();

            var productColorsList = GetAvailableColors();

            foreach (var color in productColorsList)
            {
                color.Click();
                var productStatus = GetStatus();

                var currentProductColor = GetSelectedColor();
                if (!productStatus.Contains("Нет в наличии") && !currentProductColor.Contains(product1.Color))
                {
                    var product2 = BuyProduct();
                    boughtProducts.Add(product2);
                    break;
                }
            }
            return boughtProducts;
        }

    }
}