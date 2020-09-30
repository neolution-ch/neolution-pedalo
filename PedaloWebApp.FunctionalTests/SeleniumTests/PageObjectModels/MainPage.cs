using System.Collections.Generic;
using System.Text;

namespace PedaloWebApp.FunctionalTests.SeleniumTests.PageObjectModels
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    class MainPage : PageBase
    {
        public MainPage(RemoteWebDriver driver) : base(driver)
        {
        }
    }
}
