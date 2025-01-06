using ProgressContactFormProject.Pages;
using ProgressContactFormProject.Tests;

namespace CompanyPortalTest.Tests
{
    public class RegistrationTests : BaseTest
    {


        // Data provider method
        public static object[] RegistrationData => new object[]
        {
            new object[] { "MOVEit – Secure File Transfer", "","Lodarski", "test@progress.com", "IT Director/Executive", "088912345", "North Macedonia",  "Progress" , "Software Solutions" },
            new object[] { "MOVEit – Secure File Transfer", "Velislav","", "test@progress.com", "IT Director/Executive", "088912345", "North Macedonia",  "Progress" , "Software Solutions" },
            new object[] { "MOVEit – Secure File Transfer", "Velislav","Lodarski", "", "IT Director/Executive", "088912345", "North Macedonia",  "Progress" , "Software Solutions" },
            new object[] { "MOVEit – Secure File Transfer", "Velislav","Lodarski", "test@progress.com", "IT Director/Executive", "", "North Macedonia",  "Progress" , "Software Solutions" },
            new object[] { "MOVEit – Secure File Transfer", "Velislav","Lodarski", "test@progress.com", "IT Director/Executive", "088912345", "North Macedonia", "" , "Software Solutions" }
        };

        // Data provider method for EU countries
        public static object[] EUCountries => new object[]
        {
            new object[] { "Sitefinity – Content Management and Digital Experience Platform", "Test","Lodarski", "test@progress.com", "088912345", "Bulgaria",  "Progress"  },
            new object[] { "Sitefinity – Content Management and Digital Experience Platform", "Test","Lodarski", "test@progress.com",  "088912345", "Spain",  "Progress"  },
            new object[] { "Sitefinity – Content Management and Digital Experience Platform", "Test","Lodarski", "test@progress.com",  "088912345", "Italy",  "Progress"  },
        };

        [SetUp]
        public void SetUp()
        {
            // Initialize the page object
            companyPortalPage = new CompanyPortalPage(driver); 
            companyPortalPage.OpenPage();
        }

        [TestCaseSource(nameof(RegistrationData))]
        public void RegistrationTest(string productName, string firstName, string lastName, string email, string jobFunction, string phoneNumber, string country, string companyName, string industry)
        {
            string initialUrl = driver.Url;

            // Fill in the registration form
            companyPortalPage.SelectProductDropDownByText(productName);
            companyPortalPage.EnterFirstName(firstName);
            companyPortalPage.EnterLastName(lastName);
            companyPortalPage.EnterBusinessEmail(email);
            companyPortalPage.SelectJobFunction(jobFunction);
            companyPortalPage.EnterCompanyField(companyName);
            companyPortalPage.SelectCountry(country);
            companyPortalPage.EnterPhoneNumber(phoneNumber);
            companyPortalPage.SelectIndustry(industry);
            companyPortalPage.EnterCustomMessage(2000);
            companyPortalPage.ClickContactSalesBtn();
            string currentUrl = driver.Url;

            // Assert that the URL has not changed
            Assert.That(currentUrl, Is.EqualTo(initialUrl), "The URL has changed even though there is mandatory field which is empty");

        }
        [TestCaseSource(nameof(EUCountries))]
        public void TestEUCountries(string productName, string firstName, string lastName, string email,  string phoneNumber, string country, string companyName )
        {

            // Fill in the registration form
            companyPortalPage.SelectProductDropDownByText(productName);
            companyPortalPage.EnterFirstName(firstName);
            companyPortalPage.EnterLastName(lastName);
            companyPortalPage.EnterBusinessEmail(email);
            companyPortalPage.EnterCompanyField(companyName);
            companyPortalPage.SelectCountry(country);
            companyPortalPage.EnterPhoneNumber(phoneNumber);
            companyPortalPage.EnterCustomMessage(1);
            companyPortalPage.ClickPrivacyPolicyHyperlink();
            // Wait for the new tab to open (adjust the wait as needed)
            

            // Get all window handles and ensure there is more than one
            var windowHandles = driver.WindowHandles;
            string currentWindowHandle = driver.CurrentWindowHandle;
            string? newWindowHandle = windowHandles.FirstOrDefault(handle => handle != currentWindowHandle);

            // Assert that a new window or tab has opened (newWindowHandle should not be null)
            Assert.NotNull(newWindowHandle, "No new window or tab opened.");

            // Switch to the new tab
            driver.SwitchTo().Window(newWindowHandle!);

            // Assert the URL of the new tab
            Assert.That(driver.Url, Is.EqualTo("https://www.progress.com/legal/privacy-policy"), "The Privacy Policy link did not open the expected URL.");
        }

    
    }
}