﻿using TechTalk.SpecFlow;
using SeleniumSpecFlow.Utilities;
using UISelenium.Pages;
using FluentAssertions;
using SeleniumExtras.WaitHelpers;
using SeleniumExtras;
using OpenQA.Selenium.Support.UI;
using System;
using UI.Model;
using TestLibrary.Utilities;
using System.Collections.Generic;
using OpenQA.Selenium;
using System.Linq;
using OpenQA.Selenium.Interactions;
using TestFramework;

namespace SeleniumSpecFlow.Steps
{
    [Binding]
    public class LoginPageSteps : ObjectFactory
    {
        private ScenarioContext _scenarioContext;
        private Hearing _hearing;
        public LoginPageSteps(ScenarioContext scenarioContext)
            :base (scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _scenarioContext.Add("drivers", drivers);
        }

        [Given(@"I log in as ""([^""]*)""")]
        public void GivenILogInAs(string userName)
        {
            _scenarioContext.UpdatePageName("Login");
            var result= CommonPageActions.NavigateToPage(Config.AdminUrl, "login.microsoftonline.com");
            Login(userName, Config.UserPassword);
            _scenarioContext.UpdateUserName(userName);
            _scenarioContext.UpdatePageName("Dashboard");
        }
    
        public void Login(string username, string password)
        {
            TestFramework.ExtensionMethods.FindElementWithWait(Driver, LoginPage.UsernameTextfield, _scenarioContext, TimeSpan.FromSeconds(int.Parse(Config.DefaultElementWait))).SendKeys(username);
            Driver.FindElement(LoginPage.Next).Click();
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(int.Parse(Config.DefaultElementWait)));
            wait.Until(ExpectedConditions.ElementIsVisible(LoginPage.PasswordField));
            wait.Until(ExpectedConditions.ElementToBeClickable(LoginPage.SignIn));
            wait.Until(ExpectedConditions.ElementToBeClickable(LoginPage.BackButton));
            Driver.FindElement(LoginPage.PasswordField).SendKeys(password);
            TestFramework.ExtensionMethods.FindElementWithWait(Driver, LoginPage.SignIn, _scenarioContext).Click();
        }

        [Then(@"all participants log in to video web")]
        public void ThenAllParticipantsLogInToVideoWeb()
        {
            _hearing = (Hearing)_scenarioContext["Hearing"];
            Driver?.Dispose();
            foreach (var participant in _hearing.Participant)
            {
                Driver = new DriverFactory().InitializeDriver(TestConfigHelper.browser);
                _scenarioContext["driver"] = Driver;
                Driver.Navigate().GoToUrl(Config.VideoUrl);
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(int.Parse(Config.DefaultElementWait)));
                wait.Until(ExpectedConditions.ElementIsVisible(LoginPage.UsernameTextfield));
                _scenarioContext.UpdatePageName("Video Web Login");
                drivers.Add($"{participant.Id}#{participant.Party.Name}-{participant.Role.Name}", Driver);
                Login(participant.Id, Config.UserPassword);
            }
            _scenarioContext["drivers"] = drivers;
        }
    }
}
