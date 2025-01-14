﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UISelenium.Pages
{
	///<summary>
	/// Common Header elements
	///</summary>
    public class Header
    {
        public static By SignOut = By.Id("logout-link");
        public static By LinkSignOut = By.Id("linkSignOut");
        public static By BookingsList = By.XPath("//ul[@id='navigation']//a[text()[contains(.,'Booking')]]");
        public static By SignoutCompletely = By.CssSelector("div.table-cell.text-left.content");
    }
}
