using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace TestProject.Pages
{
    public class UserMenuPopup
    {
        private IWebDriver driver;
        private IWait<IWebDriver> wait;

        public UserMenuPopup(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(10));
        }

        private readonly By _closeBannerButton = By.ClassName("exponea-close-cross");
        private readonly By _headerUserMenu = By.Id("header_user_menu_parent");
        private readonly By _userMenuTitle = By.ClassName("auth-title");
        private readonly By _userMenuPopup = By.ClassName("popup-auth");
        private readonly By _signUpButton = By.ClassName("auth-f-signup");

        private readonly By _signUpPageTitle = By.ClassName("signup-title");

        private bool IsSignUpPageTitleDisplayed()
        {
            return driver.FindElement(_signUpPageTitle).Displayed;
        }

        public void CloseBanner()
        {
            wait.Until(
                    ExpectedConditions.ElementToBeClickable(
                        _closeBannerButton))
                .Click();
        }

        public void OpenUserMenu()
        {
            driver.FindElement(_headerUserMenu).Click();
            Assert.IsTrue(IsMenuPopupDisplayed(), "Authorization popup did not appear");
        }

        public string GetMenuTitle()
        {
            return driver.FindElement(_userMenuTitle).Text;
        }

        public IWebElement GetUserMenuPopup()
        {
            return driver.FindElement(_userMenuPopup);
        }

        public bool IsMenuPopupDisplayed()
        {
            return GetUserMenuPopup().Displayed;
        }

        public void OpenSignUpPage()
        {
            driver.FindElement(_signUpButton).Click();
            Assert.IsTrue(IsSignUpPageTitleDisplayed(), "Registration page title didn't appear in time");
        }
    }
}