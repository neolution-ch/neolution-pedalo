namespace PedaloWebApp.FunctionalTests.SeleniumTests
{
    using Neolution.Automation.Selenium;
    using PageObjectModels;
    using Shouldly;
    using Xunit;

    public class CustomersTests
    {
        [Fact]
        public void CanEditFirstnameOfCustomer()
        {
            using var driver = WebDriverFactory.CreateChrome();
            driver.Navigate().GoToUrl("https://localhost:5001");

            var mainPage = new MainPage(driver);
            var customersPage = mainPage.Navigation.GoToCustomersPage();
            var customerEditPage = customersPage.EditCustomer("Skywalker");

            customerEditPage.FirstNameTextBox.Clear();
            customerEditPage.FirstNameTextBox.SendKeys("Anakin");
            var resultPage = customerEditPage.SubmitEdit();

            resultPage.ShouldBeOfType<CustomersPage>();
        }
    }
}
