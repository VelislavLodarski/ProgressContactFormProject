using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ProgressContactFormProject.Pages
{
    public class CompanyPortalPage
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;
        protected static string CompanyUrl = "https://www.progress.com/company/contact";

        //Button
        public IWebElement AcceptCookiesButton => driver.FindElement(By.Id("onetrust-accept-btn-handler"));

        private IWebElement ContactSalseButton => driver.FindElement(By.XPath("//button[normalize-space()='Contact sales']"));


        //Drop Downs
        private SelectElement ProductDropDown => new SelectElement(driver.FindElement(By.Id("Dropdown-1")));
        private SelectElement IndustryDropDown => new SelectElement(driver.FindElement(By.Id("TaxonomiesListField-1")));
        private SelectElement JobFunctionDropDown => new SelectElement(driver.FindElement(By.Id("Dropdown-3")));
        private SelectElement CountryDropdown => new SelectElement(driver.FindElement(By.Id("Country-1")));

        //Language DropDown

        private IWebElement LanguageDropDown => driver.FindElement(By.XPath("//a[@aria-label='Language dropdown']"));

        //WebElements
        private IWebElement BusinessEmailField => driver.FindElement(By.Id("Email-1"));
        private IWebElement CompanyField => driver.FindElement(By.Id("Textbox-3"));
        public IWebElement FirstNameField => driver.FindElement(By.Name("FirstName"));
        private IWebElement LastNameField => driver.FindElement(By.Name("LastName"));
        private IWebElement PhoneNumberField => driver.FindElement(By.Id("Textbox-5"));
        private IWebElement MessageField => driver.FindElement(By.Id("Textarea-1"));

        private IWebElement PrivacyPolicyHyperlink => driver.FindElement(By.XPath("//p[contains(text(),'By submitting this form, I understand and acknowle')]//a[normalize-space()='Privacy Policy']"));

        // Constructor
        public CompanyPortalPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        // Open the page
        public void OpenPage()
        {
            driver.Navigate().GoToUrl(CompanyUrl);
        }

        public void ClickPrivacyPolicyHyperlink()
        {
            // Wait for the element to be clickable, if necessary
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement privacyPolicyLink = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//p[contains(text(),'By submitting this form, I understand and acknowle')]//a[normalize-space()='Privacy Policy']")));

            // Click on the Privacy Policy link
            privacyPolicyLink.Click();
        }

        public void ClickContactSalesBtn()
        {
            ContactSalseButton.Click();
        }


        public void ChangeLanguage(string language)
        {
            // Click the Language Dropdown to open the options
            LanguageDropDown.Click();

            // Construct the XPath for the desired language dynamically
            string languageOptionXPath = $"//a[normalize-space()='{language}']";

            // Find the language option element
            IWebElement languageOption = driver.FindElement(By.XPath(languageOptionXPath));

            // Click the desired language option
            languageOption.Click();
        }
        public bool IsLanguageChanged(string languageIndicator)
        {
            // Get the current URL
            string currentUrl = driver.Url;

            // Check if the URL contains the language indicator
            return currentUrl.Contains($"/{languageIndicator}/");
        }
        public bool IsHeaderVisible(string expectedHeaderXPath)
        {
            try
            {
                IWebElement header = driver.FindElement(By.XPath("//h1[normalize-space()='¿Cómo podemos ayudar?']"));
                return header.Displayed; // Ensures the element is visible on the screen
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void ClickElementById(string elementId)
        {
            IWebElement element = driver.FindElement(By.Id(elementId));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
        }

        //Actions
        public void ClickAcceptCookiesButton()
        {
            // Scroll the element into view and click it using JavaScript
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", AcceptCookiesButton);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", AcceptCookiesButton);
        }

        public void EnterFirstName(string firstName)
        {
            FirstNameField.Clear(); // Optional: Clears the field before entering text
            FirstNameField.SendKeys(firstName);
        }
        public void EnterLastName(string lastName)
        {
            LastNameField.Clear();
            LastNameField.SendKeys(lastName);
        }

        public void EnterBusinessEmail(string email)
        {
            BusinessEmailField.Clear();
            BusinessEmailField.SendKeys(email);
        }
        public void EnterPhoneNumber(string email)
        {
            PhoneNumberField.Clear();
            PhoneNumberField.SendKeys(email);
        }
        public void SelectProduct(string product)
        {
            ProductDropDown.SelectByText(product);
        }


        public void SelectProductDropDownByText1(string productName)
        {
            // Wait for the dropdown to be visible
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("Dropdown-1")));

            if (string.IsNullOrEmpty(productName))
            {
                // Select the default option by its visible text
                ProductDropDown.SelectByText("Select product");
            }
            else
            {
                ProductDropDown.SelectByText(productName);
            }
        }

        public void SelectProductDropDownByText(string productName)
        {
            // Locate and click the dropdown
            var dropdownTrigger = driver.FindElement(By.Id("Dropdown-1"));
            dropdownTrigger.Click();

            // Wait for the dropdown options to be visible
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("Dropdown-1")));

            if (string.IsNullOrEmpty(productName))
            {
                // Select the default option by clicking it
                var defaultOption = driver.FindElement(By.Id("DropdownListFieldCustomValueController"));
                defaultOption.Click();

            }
            else
            {
                // Select the desired product
                var productOption = driver.FindElement(By.XPath($"//option[text()='{productName}']"));
                productOption.Click();
            }
        }

        public void EnterCompanyField(string company)
        {
            // CompanyField.Clear();
            CompanyField.SendKeys(company);
        }

        public void SelectJobFunction(string jobFunction)
        {
            JobFunctionDropDown.SelectByText(jobFunction);
        }

        public void SelectCountry(string country)
        {
            CountryDropdown.SelectByText(country);
        }

        public void SelectIndustry(string industry)
        {
            IndustryDropDown.SelectByText(industry);
        }

        public void EnterLongMessage()
        {
            // Generate the long message
            string longMessage = StringGenerator.LongMessage;
            // Clear the field (optional) and send the long message
            MessageField.Clear();
            MessageField.SendKeys(longMessage);
        }
        public void EnterCustomMessage(int length)
        {
            string customMessage = StringGenerator.GenerateRandomString(length); // Generate the message
            MessageField.SendKeys(customMessage); // Send the custom message
        }
        public void EnterMessage(string message)
        {
            // Send the provided message to the Message field
            MessageField.SendKeys(message);
        }

        public bool IsNameFieldChanged(string expectedHeaderXPath)
        {
            try
            {
                IWebElement header = driver.FindElement(By.XPath("//label[normalize-space()='Nombre']"));
                return header.Displayed; // Ensures the element is visible on the screen
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public List<string> ValidateRequiredFields()
        {
            // Locate all elements containing the text "is required"
            IList<IWebElement> requiredFieldElements = driver.FindElements(By.XPath("//*[contains(text(),'is required')]"));

            // Store the displayed validation messages
            List<string> displayedMessages = new List<string>();

            foreach (var element in requiredFieldElements)
            {
                if (element.Displayed)
                {
                    Console.WriteLine("Validation Message Displayed: " + element.Text);
                    displayedMessages.Add(element.Text);
                }
                else
                {
                    Console.WriteLine("Validation message is not displayed.");
                }
            }

            return displayedMessages;
        }


        public void ValidateMandatoryFields()
        {
            // Locate all elements containing the asterisk symbol
            IList<IWebElement> elementsWithAsterisk = driver.FindElements(By.XPath("//*[text()[contains(., '*')]]"));

            Console.WriteLine("Found " + elementsWithAsterisk.Count + " elements with asterisk (*).");

            foreach (var element in elementsWithAsterisk)
            {
                try
                {
                    // Print the text and tag name of each element
                    Console.WriteLine("Element found with asterisk: Tag: " + element.TagName + " - Text: " + element.Text);
                }
                catch (Exception ex)
                {
                    // Handle any exceptions and print the error
                    Console.WriteLine("Error processing element: " + ex.Message);
                }
            }
        }
    }
}