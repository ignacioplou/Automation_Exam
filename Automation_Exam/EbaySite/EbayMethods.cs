using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Automation_Exam.EbaySite.Models;
using OpenQA.Selenium;

namespace Automation_Exam.EbaySite
{
    class EbayMethods
    {
        IWebDriver _webDriver;

        public EbayMethods(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public List<ProductsData> GetListOfProductsAndPrices()
        {
            List<ProductsData> result = new List<ProductsData>();
            IReadOnlyCollection<IWebElement> productContainers = _webDriver.FindElements(By.CssSelector("li.sresult"));

            foreach (IWebElement productContainer in productContainers)
            {
                IWebElement titleElement = productContainer.FindElement(By.CssSelector(".gvtitle"));
                IWebElement priceElement;
                try
                {
                    priceElement = productContainer.FindElement(By.CssSelector(".gvprices"));
                }
                catch
                {
                    priceElement = productContainer.FindElement(By.CssSelector(".gvprices1"));
                }
                IWebElement shipmentElement = productContainer.FindElement(By.CssSelector(".gvshipping"));

                ProductsData productsData = new ProductsData();
                productsData.ProductName = titleElement.Text;
                var prices = FindNumbers(priceElement.Text);
                productsData.Price = prices[0];
                var shipment = FindNumbers(shipmentElement.Text);
                productsData.Shippment = shipment[0];

                result.Add(productsData);
            }

            return result;
        }

        IReadOnlyList<decimal> FindNumbers(string str)
        {
            List<decimal> result = new List<decimal>();

            MatchCollection matches = Regex.Matches(str, @"USD(\s)?(?<num>[0-9\.,]+)");

            for (int i = 0; i < matches.Count; i++)
            {
                decimal num;
                if (decimal.TryParse(matches[i].Groups["num"].Value, out num))
                    result.Add(num);
            }

            if (result.Count == 0)
            {
                result.Add(0);
            }

            return result;
        }

        public bool TestOrderAscendant(int quantity, List<ProductsData> listOfProducts)
        {
            bool result = false;

            if ((listOfProducts.Count) >= quantity)
            {
                decimal comparedTo = 0;
                for (int i = 0; i < quantity; i++)
                {
                    if (listOfProducts[i].TotalPrice > comparedTo)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                        return result;
                    }
                    comparedTo = listOfProducts[i].TotalPrice;
                }
            }
            else
            {
                Console.WriteLine("The quantity of products is less than what you asked");
                return result;
            }

            return result;
        }

        public void PrintFirstProductsWithPrices(List<ProductsData> firstProductsList)
        {
            Console.WriteLine("- Print First 5 products:");
            foreach (ProductsData firstProducts in firstProductsList)
            {
                Console.WriteLine(firstProducts.ProductName + " - Price: " + firstProducts.Price.ToString() + " Shipment: " + firstProducts.Shippment.ToString());
            }
        }

        public void PrintProductsByNameAsc(List<ProductsData> list)
        {
            Console.WriteLine("- Print Products ordered by Name ascendant:");
            List<ProductsData> orderedNameAsc = list.OrderBy(p => p.ProductName).ToList();
            foreach (ProductsData byName in orderedNameAsc.OrderBy(p => p.ProductName))
            {
                Console.WriteLine(byName.ProductName + " - " + byName.TotalPrice.ToString());
            }
        }

        public void PrintProductsByPriceDesc(List<ProductsData> list)
        {
            Console.WriteLine("- Print Products ordered by Total Price (Price + Shipment) descendant:");
            List<ProductsData> orderedPriceDesc = list.OrderByDescending(p => p.TotalPrice).ToList();
            foreach (ProductsData byTotalPrice in orderedPriceDesc.OrderByDescending(p => p.TotalPrice))
            {
                Console.WriteLine(byTotalPrice.TotalPrice.ToString() + " - " + byTotalPrice.ProductName);
            }
        }

    }
}
