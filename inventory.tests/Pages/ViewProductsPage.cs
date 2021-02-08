using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace inventory.tests.Pages
{
    public class ViewProductsPage
    {
        private IWebDriver driver;

        public ViewProductsPage(IWebDriver wd)
        {
            driver = wd;
        }

        public IWebElement ProductTextbox
        {
            get { return driver.FindElement(By.Id("ProductFilter")); }
        }

        public IReadOnlyList<IWebElement> TableHeaders
        {
            get { return driver.FindElements(By.CssSelector("#ProductList_DXHeadersRow0 td[class*='Header']")); }
        }

        public IReadOnlyList<IWebElement> TableRows
        {
            get { return driver.FindElements(By.CssSelector("tr[class*='DataRow']")); }
        }

        public IReadOnlyList<IWebElement> RowCells(ISearchContext sc)
        {
            return sc.FindElements(By.CssSelector("td"));
        }

        public string GetHeaderText(ISearchContext sc)
        {
            try
            {
                return sc.FindElement(By.XPath(".//td[1]")).GetAttribute("textContent");
            } catch { return ""; }
        }

        public decimal StockOnHand
        {
            get { return decimal.Parse(driver.FindElement(By.Id("StockOnHand")).GetAttribute("textContent")); }
        }

        public decimal AllocatedQty
        {
            get { return decimal.Parse(driver.FindElement(By.Id("AllocatedQty")).GetAttribute("textContent")); }
        }

        public decimal AvailableQty
        {
            get { return decimal.Parse(driver.FindElement(By.Id("AvailableQty")).GetAttribute("textContent")); }
        }

        public IWebElement InventoryTab
        {
            get { return driver.FindElement(By.Id("tabsInventoryLink")); }
        }

        public IReadOnlyList<IWebElement> InventoryTabTableHeaders
        {
            get
            {
                var elems = driver.FindElements(By.CssSelector("div.ngHeaderText"));
                return elems.Where(header => header.Displayed).ToList();
            }
        }

        public IReadOnlyList<IWebElement> InventoryTableRows
        {
            get { return driver.FindElements(By.CssSelector("div.ngRow")); }
        }

        public IReadOnlyList<IWebElement> InventoryTableRowCells(ISearchContext tablerow)
        {
            return tablerow.FindElements(By.CssSelector("div.ngCellText"));
        }
    }
}
