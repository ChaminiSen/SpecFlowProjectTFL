using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProjectTFL.Drivers
{
    public class SelenuimDriver
    {


        private IWebDriver driver;

        private readonly ScenarioContext _scenarioContext;

        public SelenuimDriver(ScenarioContext scenarioContext) => _scenarioContext = scenarioContext;


        public IWebDriver Setup() {

            IWebDriver driver = new ChromeDriver();

            _scenarioContext.Set(driver, "WebDriver");
            driver.Navigate().GoToUrl("https://tfl.gov.uk");
            driver.Manage().Window.Maximize();
            return driver;

        }


    }
}
