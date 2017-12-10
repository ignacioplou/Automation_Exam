using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Automation_Exam.Providers;
using Automation_Exam.EbaySite;
using System.Threading;
using System.Globalization;
using Automation_Exam.EbaySite.Models;
using System.Collections.Generic;

namespace Automation_Exam
{
    [TestClass]
    public class EbayFlow
    {
        IWebDriver _webDriver;
        EbayElements _ebayElements;
        EbayMethods _ebayMethods;
        List<ProductsData> _productsData;

        [TestInitialize]
        public void Initialize()
        {
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            _webDriver = new WebDriverFactory().Create();
            _webDriver.Navigate().GoToUrl("https://www.ebay.com/");

            _ebayElements = new EbayElements(_webDriver);
            _ebayMethods = new EbayMethods(_webDriver);
            _productsData = new List<ProductsData>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _webDriver.Quit();
        }
                
        [TestMethod]
        public void AutomationExam()
        {
            _ebayElements.MainSearchField().SendKeys("shoes");
            _ebayElements.MainSearchButton().Click();
            _ebayElements.BrandSelection().Click();
            _ebayElements.SizeSelection().Click();
            Console.WriteLine("- Print the number of results: " + _ebayElements.NumberOfResults().Text);
            _ebayElements.OrderByDropDown().Click();
            Thread.Sleep(1000);
            _ebayElements.OrderByPriceAscendant().Click();
            Thread.Sleep(1000);
            //_ebayElements.BuyItNowButton().Click();
            _productsData = new List<ProductsData>(_ebayMethods.GetListOfProductsAndPrices());
            Assert.IsTrue(_ebayMethods.TestOrderAscendant(5, _productsData), "The order of the products is not Acendant");
            _productsData.RemoveRange(5, ((_productsData.Count) - 5));
            _ebayMethods.PrintFirstProductsWithPrices(_productsData);            
            _ebayMethods.PrintProductsByNameAsc(_productsData);            
            _ebayMethods.PrintProductsByPriceDesc(_productsData);
        }
    }
}