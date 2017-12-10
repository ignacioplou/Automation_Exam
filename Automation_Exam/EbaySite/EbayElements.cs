using OpenQA.Selenium;

namespace Automation_Exam.EbaySite
{
    public class EbayElements
    {
        IWebDriver _webDriver;

        public EbayElements(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public IWebElement MainSearchField()
        {
            return _webDriver.FindElement(By.Id("gh-ac"));
        }

        public IWebElement MainSearchButton()
        {
            return _webDriver.FindElement(By.Id("gh-btn"));
        }

        public IWebElement BrandSelection()
        {
            return _webDriver.FindElement(By.CssSelector(".brnd input[aria-label='PUMA']"));
        }

        public IWebElement SizeSelection()
        {
            return _webDriver.FindElement(By.CssSelector(".cbx input[aria-label='10']"));
        }

        public IWebElement NumberOfResults()
        {
            return _webDriver.FindElement(By.CssSelector(".clt span"));
        }

        public IWebElement OrderByDropDown()
        {
            return _webDriver.FindElement(By.CssSelector(".sel a"));
        }

        public IWebElement OrderByPriceAscendant()
        {
            return _webDriver.FindElement(By.CssSelector("#SortMenu li:nth-child(3)"));
        }

        public IWebElement BuyItNowButton()
        {
            return _webDriver.FindElement(By.CssSelector(".pnl-b a:nth-child(3)"));
        }
    }
}