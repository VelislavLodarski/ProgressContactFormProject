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
            Assert.IsTrue(contactThankYouPage.IsThankYouMessageVisible("Thanks! We received your request."), "dasda");
            Assert.IsTrue(contactThankYouPage.IsContactThankYouPage(), "The URL does not end with 'contact-thank-you'.");

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
        public void ValidateAsterinxFields()
        {
            companyPortalPage.ValidateMandatoryFields();
        }
        //[Test, Order(5)]
        //public void MarkLastMovieAsWatched()
        //{

        //}
        
    }
}