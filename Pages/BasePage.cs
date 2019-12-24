﻿using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TestProject.Pages
{
    public class BasePage
    {
        protected static double ParseProductPrice(string priceString)
        {
            return Convert.ToDouble(priceString.Replace(" ", "").Replace("₴", ""));
        }

    }
}