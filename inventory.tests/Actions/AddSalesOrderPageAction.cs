using System.Collections.Generic;
using inventory.tests.DataModel;
using inventory.tests.Pages;
using inventory.tests.Utils;
using OpenQA.Selenium;

namespace inventory.tests.Actions
{
    public class AddSalesOrderPageAction : BaseAction
    {
        private AddSalesOrderPage salesorderpage;

        public AddSalesOrderPageAction(IWebDriver wd) : base(wd)
        {
            salesorderpage = new AddSalesOrderPage(wd);
        }

        public void EnterSelectCustomerCode(string custcode)
        {
            var customertextbox = salesorderpage.CustomerCodeTextbox;
            customertextbox.Clear();
            customertextbox.SendKeys(custcode);
            WaitAjaxLoader();
            salesorderpage.GetCustomerCodeInDropdown(custcode).Click();
            WaitPageLoad();
        }

        public void SelectWarehouse(string warehouse)
        {
            salesorderpage.WarehouseSelectbox.SelectByText(warehouse);
            WaitAjaxLoader();
        }

        public void EnterSelectProductCode(string prodcode)
        {
            var prodtextbox = salesorderpage.ProductTextbox;
            prodtextbox.Clear();
            prodtextbox.SendKeys(prodcode);
            WaitAjaxLoader();
            salesorderpage.GetProductCodeInDropdown(prodcode).Click();
            WaitAjaxLoader();
        }

        public void EnterQty(string qty)
        {
            var qtytextbox = salesorderpage.QuantityTextbox;
            qtytextbox.Clear();
            qtytextbox.SendKeys(qty);
        }

        public void ClickAddOrderLine()
        {
            salesorderpage.AddOrderLineButton.Click();
            WaitAjaxLoader();
        }

        public void ClickPlaced()
        {
            salesorderpage.SaveMenuDrop.Click();
            Delay(100);
            salesorderpage.PlacedButton.Click();
            WaitAjaxLoader();
        }

        public string GetOrderStatus()
        {
            return salesorderpage.OrderStatusLabel.GetAttribute("textContent").Trim();
        }

        public string GetOrderNumber()
        {
            return salesorderpage.OrderNumberLabel.GetAttribute("textContent").Trim();
        }

        public void ClickComplete()
        {
            salesorderpage.CompleteOrderButton.Click();
            Delay(100);
        }

        public void ClickYesModal()
        {
            salesorderpage.CompleteOrderModalYes.Click();
            WaitAjaxLoader();
        }

        public bool IsLinkTextPresentInOrderLines(string linktext)
        {
            try
            {
                var elem = salesorderpage.OrderLinesTable.FindElement(By.LinkText(linktext));
                return elem.Displayed && elem.Enabled;
            } catch { return false; }
        }
    }
}
