using ProgressContactFormProject.Tests;

namespace CompanyPortalTest.Tests
{
    public class CompanyPortalTest : BaseTest
    {
        string randomMessage = StringGenerator.GenerateRandomString(1999);
       
        List<string> expectedMessagesForEmptyFields = new List<string>
    {
        "Product is required",
        "Email is required",
        "First name is required",
        "Last name is required",
        "Company is required",
        "Company type is required", 
        "Country/territory is required",
        "Phone is required"
    };

        

        [Test, Order(1)]
        public void ChangeTheLanguageToSpanish()
        {
            
            companyPortalPage.ChangeLanguage("Español");
            Assert.IsTrue(companyPortalPage.IsLanguageChanged("es"), "The language was not successfully changed to Spanish.");
            Assert.IsTrue(companyPortalPage.IsHeaderVisible("¿Cómo podemos ayudar?"),"The expected Spanish header is not visible on the page.");
            Assert.IsTrue(companyPortalPage.IsNameFieldChanged("Nombre"), "The Name field is still in English");
            
        }

        [Test, Order(2)]
        public void SuccessfullySubmitForm()
        {
            companyPortalPage.SelectProduct("MOVEit – Secure File Transfer");
            companyPortalPage.EnterFirstName("test");
            companyPortalPage.EnterLastName("test");
            companyPortalPage.EnterBusinessEmail(" test@progress.com");
            companyPortalPage.SelectJobFunction("IT Director/Executive");
            companyPortalPage.EnterCompanyField("Progress");
            companyPortalPage.SelectCountry("North Macedonia");
            companyPortalPage.EnterPhoneNumber("000000");
            companyPortalPage.SelectIndustry("Software Solutions");
            companyPortalPage.EnterCustomMessage(2000);
            companyPortalPage.ClickContactSalesBtn();
            Assert.IsTrue(contactThankYouPage.IsThankYouMessageVisible("Thanks! We received your request."), "Test Failled");
            Assert.IsTrue(contactThankYouPage.IsContactThankYouPage(), "The URL does not end with 'contact-thank-you'. Which means your contact form is not sent");

        }

        [Test, Order(3)]
        public void ValidateAllMandatoryFields()
        {

            companyPortalPage.ClickContactSalesBtn();
            // Get the displayed messages
            List<string> actualMessages = companyPortalPage.ValidateRequiredFields();
            // Assert that the number of messages matches
            Assert.That(actualMessages.Count, Is.EqualTo(expectedMessagesForEmptyFields.Count),"The number of validation messages does not match the expected count.");
            // Assert that the messages match the expected values
            Assert.That(actualMessages, Is.EquivalentTo(expectedMessagesForEmptyFields),"The validation messages do not match the expected values.");
        }

        [Test, Order(4)]
        public void SubmitFormWithInvalidNumberFormat()
        {
            companyPortalPage.SelectProduct("MOVEit – Secure File Transfer");
            companyPortalPage.EnterFirstName("test");
            companyPortalPage.EnterLastName("test");
            companyPortalPage.EnterBusinessEmail(" test@progress.com");
            companyPortalPage.SelectJobFunction("IT Director/Executive");
            companyPortalPage.EnterCompanyField("Progress");
            companyPortalPage.SelectCountry("North Macedonia");
            companyPortalPage.EnterPhoneNumber("There Are No Digits HERE");
            companyPortalPage.SelectIndustry("Software Solutions");
            companyPortalPage.EnterCustomMessage(2000);
            companyPortalPage.ClickContactSalesBtn();
            Assert.IsTrue(contactThankYouPage.IsThankYouMessageVisible("Thanks! We received your request."), "Test Failled");
            //Note Phone Number field is accepting letters
            Assert.IsTrue(contactThankYouPage.IsContactThankYouPage(), "The URL does not end with 'contact-thank-you'.Which means your contact form is not sent");

        }
        [Test, Order(5)]
        public void EnterBoundrySymbols()
        {
            companyPortalPage.SelectProduct("MOVEit – Secure File Transfer");
            companyPortalPage.EnterFirstName("test");
            companyPortalPage.EnterLastName("test");
            companyPortalPage.EnterBusinessEmail(" test@progress.com");
            companyPortalPage.SelectJobFunction("IT Director/Executive");
            companyPortalPage.EnterCompanyField("Progress");
            companyPortalPage.SelectCountry("North Macedonia");
            companyPortalPage.EnterPhoneNumber("123456");
            companyPortalPage.SelectIndustry("Software Solutions");
            companyPortalPage.EnterCustomMessage(1999);
            // Retrieve the boundry value using the method in the page class
            string boundryValue = companyPortalPage.GetCounterValue();
            Assert.That(boundryValue, Is.EqualTo("1"), "The counter value is not 1.");
        }
        [Test, Order(6)]
        public void ValidatePartnersLink()
        {
            companyPortalPage.SelectProduct("MOVEit – Secure File Transfer");
            companyPortalPage.EnterFirstName("test");
            companyPortalPage.EnterLastName("test");
            companyPortalPage.EnterBusinessEmail(" test@progress.com");
            companyPortalPage.SelectJobFunction("IT Director/Executive");
            companyPortalPage.EnterCompanyField("Progress");
            companyPortalPage.SelectCountry("North Macedonia");
            companyPortalPage.EnterPhoneNumber("123456789");
            companyPortalPage.SelectIndustry("Software Solutions");
            companyPortalPage.EnterCustomMessage(1);
            companyPortalPage.ClickParthnersHyperlink();

            // Get all window handles and ensure there is more than one
            var windowHandles = driver.WindowHandles;
            string currentWindowHandle = driver.CurrentWindowHandle;
            string? newWindowHandle = windowHandles.FirstOrDefault(handle => handle != currentWindowHandle);

            // Assert that a new window or tab has opened (newWindowHandle should not be null)
            Assert.NotNull(newWindowHandle, "No new window or tab opened.");
           
            // Switch to the new tab
            driver.SwitchTo().Window(newWindowHandle!);

            // Assert the URL of the new tab
            StringAssert.StartsWith("https://www.progress.com/partners/", driver.Url, "The Privacy Policy link did not open the expected URL.");
            Assert.IsTrue(globalPartnerDirectoryPage.IsGlobalPartnerLocatorHeaderVisible(), "The 'Global Partner Locator' header is not visible on the page.");

        }
        [Test, Order(7)]
        public void ValidateInvalidMailFormat()
        {
            companyPortalPage.SelectProduct("MOVEit – Secure File Transfer");
            companyPortalPage.EnterFirstName("test");
            companyPortalPage.EnterLastName("test");
            companyPortalPage.EnterBusinessEmail("test7271");
            companyPortalPage.SelectJobFunction("IT Director/Executive");
            companyPortalPage.EnterCompanyField("Progress");
            companyPortalPage.SelectCountry("Bulgaria");
            companyPortalPage.EnterPhoneNumber("727150");
            companyPortalPage.SelectIndustry("Software Solutions");
            companyPortalPage.ClickContactSalesBtn();
            Assert.IsTrue(companyPortalPage.IsInvalidEmailFormatMessageVisible(), "The 'Invalid email format' message was not visible.");

        }
        [Test, Order(8)]
        public void ValidateInvalidNameFields()
        {
            companyPortalPage.SelectProduct("MOVEit – Secure File Transfer");
            companyPortalPage.EnterFirstName("!@#!@#!");
            companyPortalPage.EnterLastName("!@#!@#!@#");
            companyPortalPage.EnterBusinessEmail("test7271");
            companyPortalPage.SelectJobFunction("IT Director/Executive");
            companyPortalPage.EnterCompanyField("Progress");
            companyPortalPage.SelectCountry("Bulgaria");
            companyPortalPage.EnterPhoneNumber("727150");
            companyPortalPage.SelectIndustry("Software Solutions");
            companyPortalPage.ClickContactSalesBtn();
            Assert.IsTrue(companyPortalPage.AreBothInvalidFormatErrorsVisible(), "Invalid format errors for first name and last name are not displayed.");


        }

    }
}