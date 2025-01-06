using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ProgressContactFormProject.Pages
{
    public class GlobalPartnerDirectoryPage
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;
        protected static string succesfullySubmitFormURL = "https://www.progress.com/company/contact-thank-you";

        public GlobalPartnerDirectoryPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public bool IsGlobalPartnerLocatorHeaderVisible()
        {
            try
            {
                var header = driver.FindElement(By.XPath("//h1[normalize-space()='Global Partner Locator']"));
                return header.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

    }

}
