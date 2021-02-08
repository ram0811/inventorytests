using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace inventory.tests.Pages
{
    public class AddSalesOrderPage
    {
        private IWebDriver driver;

        public AddSalesOrderPage(IWebDriver wd)
        {
            driver = wd;
        }

        public IWebElement CustomerCodeTextbox
        {
            get { return driver.FindElement(By.Id("SelectedCustomerCode")); }
        }

        public IWebElement ProductTextbox
        {
            get { return driver.FindElement(By.Id("ProductAddLine")); }
        }

        public IWebElement GetProductCodeInDropdown(string prodcode)
        {
            return driver.FindElement(By.XPath($"//ul[@id='ui-id-2']/li[.//strong[text()='{prodcode}']]"));
        }

        public IWebElement QuantityTextbox
        {
            get { return driver.FindElement(By.Id("QtyAddLine")); }
        }

        public IWebElement AddOrderLineButton
        {
            get { return driver.FindElement(By.Id("btnAddOrderLine")); }
        }

        public SelectElement WarehouseSelectbox
        {
            get { return new SelectElement(driver.FindElement(By.Id("WarehouseList"))); }
        }

        public IWebElement AvailableTextbox
        {
            get { return driver.FindElement(By.Id("AvailableAddLine")); }
        }

        public IWebElement GetCustomerCodeInDropdown(string customercode)
        {
            return driver.FindElement(By.XPath($"//ul[@id='ui-id-4']//*[text()='{customercode}']"));
        }

        public IWebElement OrderNumberLabel
        {
            get { return driver.FindElement(By.Id("OrderNumberDisplay")); }
        }

        public IWebElement OrderStatusLabel
        {
            get { return driver.FindElement(By.Id("OrderStatusDisplay"));  }
        }

        public IWebElement CompleteOrderButton
        {
            get { return driver.FindElement(By.Id("btnComplete")); }
        }

        public IWebElement SaveMenuDrop
        {
            get { return driver.FindElement(By.CssSelector("#ddbSave .menudrop")); }
        }

        public IWebElement PlacedButton
        {
            get { return driver.FindElement(By.CssSelector("#ddbSave #btnPlace")); }
        }

        public IWebElement CompleteOrderModalYes
        {
            get { return driver.FindElement(By.CssSelector("#simplemodal-container #generic-confirm-modal-yes")); }
        }

        public IWebElement OrderLinesTable
        {
            get { return driver.FindElement(By.Id("SalesOrderLinesList_DXMainTable")); }
        }
    }
}
