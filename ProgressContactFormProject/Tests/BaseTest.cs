using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using ProgressContactFormProject.Pages;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace ProgressContactFormProject.Tests
{
    public class BaseTest
    {
        public IWebDriver driver;
        public WebDriverWait ?wait;
        public CompanyPortalPage companyPortalPage;
        public ContactThankYouPage contactThankYouPage;

        public Actions actions;

        [SetUp]
        public void OneTimeSetUp()
        {
            var options = new ChromeOptions();
            options.AddUserProfilePreference("profile.password_manager_enabled", false);
            options.AddArguments("--disable-search-engine-choice-screen");

            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            actions = new Actions(driver);

            companyPortalPage = new CompanyPortalPage(driver);
            contactThankYouPage = new ContactThankYouPage(driver);
            companyPortalPage.OpenPage();
            companyPortalPage.ClickAcceptCookiesButton(); 
            
        }  

        //[TearDown]
        //public void OneTimeTearDown()
        //{
        //    driver.Quit();
        //    driver.Dispose();
        //}
    }
}
