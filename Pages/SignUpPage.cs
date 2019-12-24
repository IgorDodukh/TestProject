using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace TestProject.Pages
{
    public class SignUpPage
    {
        private IWebDriver driver;

        public SignUpPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        private readonly By _nameField = By.Name("title");
        private readonly By _loginField = By.Name("login");
        private readonly By _passwordField = By.Name("password");
        private readonly By _signUpButton = By.ClassName("btn-link-sign");


        private void EnterUserName(string name)
        {
            driver.FindElement(_nameField).SendKeys(name);
        }

        private void EnterLogin(string login)
        {
            driver.FindElement(_loginField).SendKeys(login);
        }

        private void EnterPassword(string password)
        {
            driver.FindElement(_passwordField).SendKeys(password);
        }

        public void ClickSignUpButton()
        {
            driver.FindElement(_signUpButton).Click();
            var profilePage = new UserProfilePage(driver);
            Assert.IsTrue(profilePage.IsProfileContentDisplayed(), "Profile page is not displayed after registration");
        }

        public void FillUserData(AppUser user)
        {
            EnterUserName(user.Name);
            EnterLogin(user.Login);
            EnterPassword(user.Password);
        }
    }
}