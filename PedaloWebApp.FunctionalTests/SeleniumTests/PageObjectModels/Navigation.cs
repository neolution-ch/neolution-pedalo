namespace PedaloWebApp.FunctionalTests.SeleniumTests.PageObjectModels
{
    using System;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    public class Navigation
    {
        private RemoteWebDriver driver;
        private IWebElement navBar;

        public Navigation(RemoteWebDriver driver)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));

            this.navBar = driver.FindElementByCssSelector("ul.navbar-nav");
        }

        public CustomersPage GoToCustomersPage()
        {
            this.navBar.FindElement(By.LinkText("Customers")).Click();
            return new CustomersPage(this.driver);
        }
    }
}