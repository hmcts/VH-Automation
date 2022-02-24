﻿using SeleniumSpecFlow.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using UI.Model;
using UISelenium.Pages;
using FluentAssertions;
using TestFramework;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace UI.Steps
{
    [Binding]
    public class WaitingRoomPageSteps : ObjectFactory
    {
        ScenarioContext _scenarioContext;
        public WaitingRoomPageSteps(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Then(@"the judge starts the hearing")]
        public void ThenTheJudgeStartsTheHearing()
        {
            Driver = GetDriver("Judge", _scenarioContext);
            _scenarioContext["driver"] = Driver;
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(int.Parse(Config.DefaultElementWait)));
            wait.Until(ExpectedConditions.ElementToBeClickable(ParticipantWaitingRoomPage.StartVideoHearingButton));
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(int.Parse(Config.DefaultElementWait));
            ExtensionMethods.FindElementWithWait(Driver, ParticipantWaitingRoomPage.StartVideoHearingButton).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(ParticipantWaitingRoomPage.ConfirmStartButton));
            ExtensionMethods.FindElementWithWait(Driver, ParticipantWaitingRoomPage.ConfirmStartButton).Click();
        }
    }
}
