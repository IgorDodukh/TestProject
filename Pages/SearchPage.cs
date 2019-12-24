﻿using System.Collections.ObjectModel;
 using NUnit.Framework;
 using OpenQA.Selenium;

namespace TestProject.Pages
{
    public class SearchPage
    {
        
        private IWebDriver driver;

        public SearchPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        private readonly By _inputField = By.ClassName("rz-header-search-input-text");
        private readonly By _resultsHeader = By.ClassName("catalog-heading");
        private readonly By _foundProductsColors = By.ClassName("goods-tile__colors-item");


        public void SendSearchRequest(string request)
        {
            var searchField = driver.FindElement(_inputField);
            searchField.SendKeys(request);
            searchField.SendKeys(Keys.Enter);
            var resultPageTitle = GetPageTitle();
            Assert.IsTrue(resultPageTitle.Contains(request), "Search result title doesn't contain requested text");

        }
        
        public string GetPageTitle()
        {
            return driver.FindElement(_resultsHeader).Text;
        }

        public void ClickOnFirstFoundProductColor()
        {
            var allProductsColorsList = driver.FindElements(_foundProductsColors);
            allProductsColorsList[1].Click();
        }
    }
}