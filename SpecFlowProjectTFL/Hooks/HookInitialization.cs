using OpenQA.Selenium;
using SpecFlowProjectTFL.Drivers;
using TechTalk.SpecFlow;

namespace SpecFlowProjectTFL.Hooks
{
    [Binding]
    public  class HookInitialization
    {
        private readonly ScenarioContext _scenarioContext;

        public HookInitialization(ScenarioContext scenarioContext) => _scenarioContext = scenarioContext;

        [BeforeScenario]
        public void BeforeScenarioWithTag()
        {
            SelenuimDriver selenuimDriver = new SelenuimDriver(_scenarioContext);
            _scenarioContext.Set(selenuimDriver, "SeleniumDriver");


        }
            
                [AfterScenario]
        public void AfterScenario()
        {
            //_scenarioContext.Get<IWebDriver>("WebDriver").Manage().Cookies.DeleteAllCookies();
            _scenarioContext.Get<IWebDriver>("WebDriver").Dispose();
            _scenarioContext.Get<IWebDriver>("WebDriver").Quit(); ;
        }
    }
}