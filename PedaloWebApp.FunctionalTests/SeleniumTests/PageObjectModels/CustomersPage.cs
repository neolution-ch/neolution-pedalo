namespace PedaloWebApp.FunctionalTests.SeleniumTests.PageObjectModels
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    public class CustomersPage : PageBase
    {
        public CustomersPage(RemoteWebDriver driver) : base(driver)
        {
        }

        public EditCustomerPage EditCustomer(string lastName)
        {
            var table = this.Driver.FindElement(By.CssSelector("table.table"));
            var tableRow = table.FindElement(By.XPath($"//tbody/tr/td[contains(.,'{lastName}')]/.."));
            var editLink = tableRow.FindElement(By.LinkText("Edit"));
            editLink.Click();
            return new EditCustomerPage(this.Driver);
        }

    }
}