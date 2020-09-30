namespace PedaloWebApp.FunctionalTests.SeleniumTests.PageObjectModels
{
    using System;
    using OpenQA.Selenium.Remote;

    public class PageBase
    {
        public PageBase(RemoteWebDriver driver)
        {
            this.Driver = driver ?? throw new ArgumentNullException(nameof(driver));
            this.Navigation = new Navigation(this.Driver);
        }

        public RemoteWebDriver Driver { get; }

        public Navigation Navigation { get; set; }
    }
}