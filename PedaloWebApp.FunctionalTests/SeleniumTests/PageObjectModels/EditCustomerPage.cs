namespace PedaloWebApp.FunctionalTests.SeleniumTests.PageObjectModels
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    public class EditCustomerPage : PageBase
    {
        public EditCustomerPage(RemoteWebDriver driver) : base(driver)
        {
        }

        public IWebElement FirstNameTextBox => this.Driver.FindElement(By.Id("Customer_FirstName"));

        public CustomersPage SubmitEdit()
        {
            this.Driver.FindElement(By.CssSelector("input[type=submit]")).Click();
            return new CustomersPage(this.Driver);
        }
    }
}