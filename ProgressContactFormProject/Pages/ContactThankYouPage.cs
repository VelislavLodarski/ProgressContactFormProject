using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ProgressContactFormProject.Pages
{
    public class ContactThankYouPage
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;
        protected static string succesfullySubmitFormURL = "https://www.progress.com/company/contact-thank-you";

        public ContactThankYouPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
        public bool IsThankYouMessageVisible(string expectedHeaderXPath)
        {
            try
            {
                var header = driver.FindElement(By.XPath("//h1[normalize-space()='Thanks! We received your request.']"));
                return header.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
            public bool IsLanguageAndPageValid(string languageIndicator)
            {
                // Get the current URL
                string currentUrl = driver.Url;

                // Check if the URL contains the language indicator and ends with "contact-thank-you"
                return currentUrl.Contains($"/{languageIndicator}/") && currentUrl.EndsWith("contact-thank-you");
            }
        public bool IsContactThankYouPage()
        {
            // Get the current URL
            string currentUrl = driver.Url;

            // Check if the URL ends with "contact-thank-you"
            return currentUrl.EndsWith("contact-thank-you");
        }
    }

}
