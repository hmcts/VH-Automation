﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TestFramework
{
    public class ExtensionMethods
    {
  
        public static IWebElement FindElementWithWait(IWebDriver webdriver, By findBy, TimeSpan? waitPeriod=null)
        {
            waitPeriod = waitPeriod == null ? TimeSpan.FromSeconds(60) : waitPeriod;
            IWebElement webelement = null;
            try
            {
                //If there is no page specific timeout specified, use default timeout
                var wait = new WebDriverWait(webdriver, waitPeriod.Value);
                wait.Until(ExpectedConditions.ElementIsVisible(findBy));

                webelement = wait.Until(d =>
                {
                    try
                    {
                        var result = d.FindElements((findBy)).FirstOrDefault(elem => elem.Displayed);
                        return result;
                    }
                    catch (StaleElementReferenceException)
                    {

                        return null;
                    }

                });
            }
            catch 
            {
                //log the exception
            }
            return webelement;
        }
    }
}
