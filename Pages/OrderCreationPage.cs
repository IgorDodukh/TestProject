using System;
using System.Threading;
using OpenQA.Selenium;

namespace TestProject.Pages
{
    public class OrderCreationPage
    {
        private IWebDriver driver;

        public OrderCreationPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        private readonly By _nameField = By.ClassName("check-suggest-input-text");
        private readonly By _cityField = By.ClassName("check-suggest-input-text");
        private readonly By _suggestedCitiesList = By.ClassName("suggestion-i");
        private readonly By _suggestedCityLink = By.CssSelector(".f-i-sign .lightblue");
        private readonly By _phoneField = By.ClassName("check-phone-input-full-text");
        private readonly By _continueButton = By.CssSelector(".grid-box-top > div > span > button");
        private readonly By _deliveryMethodOptions = By.CssSelector(".check-method-subl-i-inner label > div");
        private readonly By _deliveryStreet = By.ClassName("check-street-input-text");
        private readonly By _deliveryPlace = By.ClassName("check-place-input-text");
        private readonly By _deliveryRoom = By.ClassName("check-room-input-text");

        private readonly By _recipientFirstName =
            By.CssSelector("div:nth-child(2) > div > .check-order-i div:nth-child(7) input");

        private readonly By _recipientName =
            By.CssSelector("div:nth-child(2) > div > .check-order-i div:nth-child(8) input");

        private void SelectFirstCity()
        {
            driver.FindElement(_cityField).Click();
            driver.FindElements(_suggestedCitiesList)[0].Click();
        }

        private void EnterPhone(string phoneNumber)
        {
            driver.FindElement(_phoneField).SendKeys(phoneNumber);
        }

        private void ClickContinueButton()
        {
            // workaround to wait until the Continue button become available.
            // This implementation MUST be changed to the better one.
            var attempts = 10;
            while (attempts > 0)
            {
                var button = driver.FindElement(_continueButton);
                var classText = button.GetAttribute("class");
                if (!classText.Contains("disabled"))
                {
                    button.Click();
                    break;
                }
                Thread.Sleep(1);
                attempts -= 1;
            }
        }

        public void EnterOrderDetails(AppUser user)
        {
            SelectFirstCity();
            EnterPhone(user.PhoneNumber);
            ClickContinueButton();
        }


        public void SelectDeliveryMethod(string methodName)
        {
            var deliveryMethods = driver.FindElements(_deliveryMethodOptions);
            foreach (var deliveryMethod in deliveryMethods)
            {
                if (deliveryMethod.Text.Contains(methodName))
                {
                    deliveryMethod.Click();
                }
            }
        }

        private void EnterStreet(string street)
        {
            driver.FindElement(_deliveryStreet).SendKeys(street);
        }

        private void EnterPlace(string place)
        {
            driver.FindElement(_deliveryPlace).SendKeys(place);
        }

        private void EnterRoom(string room)
        {
            driver.FindElement(_deliveryRoom).SendKeys(room);
        }

        private void EnterRecipientFirstName(string firstName)
        {
            driver.FindElement(_recipientFirstName).SendKeys(firstName);
        }

        public bool IsRecipientNameFieldDisplayed()
        {
            return driver.FindElement(_recipientFirstName).Displayed;
        }

        private void EnterRecipientName(string name)
        {
            driver.FindElement(_recipientName).SendKeys(name);
        }

        public void EnterDeliveryInfo(AppUser user)
        {
            if (IsRecipientNameFieldDisplayed())
            {
                EnterStreet(user.Street);
                EnterPlace(user.Place);
                EnterRoom(user.Room);
                EnterRecipientFirstName(user.Name);
                EnterRecipientName(user.Name);
            }
            else
            {
                Console.Out.WriteLine("Filling delivery info has been skipped.");
            }
        }
    }
}