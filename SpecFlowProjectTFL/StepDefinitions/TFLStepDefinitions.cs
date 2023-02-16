using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SpecFlowProjectTFL.Drivers;
using System.Collections.Generic;
using System.Xml.Linq;
using TechTalk.SpecFlow;
using static System.Collections.Specialized.BitVector32;
using OpenQA.Selenium.Support.UI;

namespace SpecFlowProjectTFL.StepDefinitions
{
    [Binding]
    public sealed class TFLStepDefinitions

    {
        IWebDriver driver;

        private readonly ScenarioContext _scenarioContext;

        public TFLStepDefinitions(ScenarioContext scenarioContext) { 
            
            _scenarioContext= scenarioContext;
        }

        [Given(@"I set valid from location")]
        public void GivenISetValidFromLocation()

        {
            driver = _scenarioContext.Get<SelenuimDriver>("SeleniumDriver").Setup();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(02);
                      

            if (driver.FindElement(By.Id("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll")).Displayed) {
                driver.FindElement(By.Id("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll")).Click();
            
            }
            driver.FindElement(By.Id("InputFrom")).SendKeys("Wood Green Underground Station");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(01);

        }

        [Given(@"I set valid to location")]
        public void GivenISetValidToLocation()
        {
            driver.FindElement(By.Id("InputTo")).SendKeys("Old Street Moorfields Eye Hospital");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(01);
        }

        [When(@"I click plan my journey button")]
        public void WhenIClickPlanMyJourneyButton()
        {
            driver.FindElement(By.Id("plan-journey-button")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(01);
           
        }

        [Then(@"I am taken to journey results screen")]
        public void ThenIAmTakenToJourneyResultsScreen()
        {
            Assert.IsTrue(driver.Title.Contains("Journey results - Transport for London"));
           
            string Expheader = "Journey results";
            var Actheader = driver.FindElement(By.XPath("//span[@class='jp-results-headline']")).Text;
            Assert.AreEqual(Expheader,Actheader,"Test Failed");
        }

        [Given(@"I set an invalid from location")]
        public void GivenISetAnInvalidFromLocation()
        {
            driver = _scenarioContext.Get<SelenuimDriver>("SeleniumDriver").Setup();
         
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(02);
         
            if (driver.FindElement(By.Id("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll")).Displayed)
            {
                driver.FindElement(By.Id("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll")).Click();

            }
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(02);
            driver.FindElement(By.Id("InputFrom")).SendKeys("123");
        }

        [Given(@"I set an invalid to location")]
        public void GivenISetAnInvalidToLocation()
        {
           driver.FindElement(By.Id("InputTo")).SendKeys("124");
        }

        [Then(@"I am shown an error message")]
        public void ThenIAmShownAnErrorMessage()
        {
            var Errorpanel = driver.FindElement(By.ClassName("field-validation-errors")).Displayed;
                        Assert.IsTrue(Errorpanel, "test failed, error panel not displayed");
            var Readerror = driver.FindElement(By.ClassName("field-validation-errors")).Text;
            Assert.IsTrue(Readerror.Contains("Journey planner could not find any results to your search. Please try again"));
                     
        }

        [Given(@"I don't set from location")]
        public void GivenIDontSetFromLocation()
        {
            driver = _scenarioContext.Get<SelenuimDriver>("SeleniumDriver").Setup();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(02);
            
            if (driver.FindElement(By.Id("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll")).Displayed)
            {
                driver.FindElement(By.Id("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll")).Click();

            }
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(02);
        }

        [Given(@"I don't set to location")]
        public void GivenIDontSetToLocation()
        {
           
        }

        [Then(@"I see an error message on screen")]
        public void ThenISeeAnErrorMessageOnScreen()
        {
            var ExpfromMsg = "The From field is required.";
            var ActfromMsg = driver.FindElement(By.Id("InputFrom-error")).Text;
            Assert.AreEqual(ExpfromMsg, ActfromMsg);

            var ExptoMsg = "The To field is required.";
            var ActtoMsg = driver.FindElement(By.Id("InputTo-error")).Text;
            Assert.AreEqual(ExptoMsg, ActtoMsg);

            Assert.IsFalse(driver.Title.Contains("Journey results - Transport for London"));

         }

        [Given(@"I am on journey results screen")]
        public void GivenIAmOnJourneyResultsScreen()
        {

            GivenISetValidFromLocation();
            GivenISetValidToLocation();
            WhenIClickPlanMyJourneyButton();
        }

        [When(@"I click edit journey button")]
        public void WhenIClickEditJourneyButton()
        {
            driver.FindElement(By.LinkText("Edit journey")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(02);
          
        }
              
        [Then(@"I can update my journey location")]

       
        public void ThenICanUpdateMyJourneyLocation()
        {
                   
            driver.FindElement(By.Id("InputFrom")).Click();
            driver.FindElement(By.Id("InputFrom")).SendKeys(Keys.Control+"a");
            driver.FindElement(By.Id("InputFrom")).SendKeys(Keys.Delete);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(02);

            driver.FindElement(By.Id("InputFrom")).SendKeys("Canary Wharf Pier");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(02);

            driver.FindElement(By.Id("plan-journey-button")).Click();

            //driver.FindElement(By.XPath("//span[@class='marker-number'][contains(text(),'1')]")).Click();
            Assert.IsTrue(driver.FindElement(By.ClassName("journey-result-summary")).Displayed);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(02);                 

           Assert.IsTrue(driver.FindElement(By.XPath("//span[contains(text(),'Canary Wharf, Canary Wharf Pier')]")).Text.Contains("Canary Wharf, Canary Wharf Pier"));
        }

        [Given(@"There is a previous planned journey")]
        public void GivenThereIsAPreviousPlannedJourney()
        {
            driver = _scenarioContext.Get<SelenuimDriver>("SeleniumDriver").Setup();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(02);
                              

            if (driver.FindElement(By.Id("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll")).Displayed)
            {
                driver.FindElement(By.Id("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll")).Click();

            }
          
            driver.FindElement(By.Id("InputFrom")).SendKeys("Gants Hill Station / Woodford Avenue");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(02);

            driver.FindElement(By.Id("InputTo")).SendKeys("Canary Wharf Pier");
            Thread.Sleep(2000);

            driver.FindElement(By.Id("InputFrom")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.Id("plan-journey-button")).Click();
            Thread.Sleep(2000);

            //Click from location
            driver.FindElement(By.XPath("//span[@class='marker-number'][contains(text(),'1')]")).Click();
            Thread.Sleep(2000);
                 
            driver.Navigate().GoToUrl("https://tfl.gov.uk/");
          
        }

        [Given(@"I select Recents tab")]
        public void GivenISelectRecentsTab()
        {

            driver.FindElement(By.LinkText("Recents")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(02);

        }

        [Then(@"I can see my recently planned journeys")]
        public void ThenICanSeeMyRecentlyPlannedJourneys()
        {
                 
            var tabDisplay = driver.FindElement(By.LinkText("Recents")).Displayed;
            Thread.Sleep(2000);
            Assert.IsTrue(tabDisplay, "Test failed, Recent tab not displayed");

            //var panel=driver.FindElement(By.Id("jp-recent-content-home-")).Displayed;

            var expContent = "Canary Wharf, Canary Wharf Pier";
            var readContent = driver.FindElement(By.XPath("//a[@class='plain-button journey-item']")).Text;

            Assert.True(readContent.Contains(expContent));
          
        }















    }
}