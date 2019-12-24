﻿using OpenQA.Selenium;

namespace TestProject.Pages
{
    public class UserProfilePage
    {
        private IWebDriver driver;

        public UserProfilePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        private readonly By _profileContent = By.ClassName("profile-content");

        private IWebElement GetProfileContent()
        {
            return driver.FindElement(_profileContent);
        }
        
        public bool IsProfileContentDisplayed()
        {
            return GetProfileContent().Displayed;
        }
        
    }
}