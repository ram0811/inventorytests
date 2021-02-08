using System.Collections.Generic;
using inventory.tests.Pages;
using OpenQA.Selenium;

namespace inventory.tests.Actions
{
    public class ViewProductsPageAction : BaseAction
    {
        private ViewProductsPage viewproducts;

        public ViewProductsPageAction(IWebDriver wd) : base(wd)
        {
            viewproducts = new ViewProductsPage(wd);
        }

        public void FilterByProduct(string product)
        {
            var elem = viewproducts.ProductTextbox;
            elem.Clear();
            elem.SendKeys(product + Keys.Return);
            WaitAjaxLoader();
        }

        public void ClickProductCode(string productcode)
        {
            driver.FindElement(By.LinkText(productcode)).Click();
            WaitPageLoad();
        }

        public List<string> GetHeaders()
        {
            var headerstr = new List<string>();
            var headers = viewproducts.TableHeaders;

            foreach (var header in headers)
            {
                headerstr.Add(viewproducts.GetHeaderText(header));
            }

            return headerstr;
        }

        public List<string> GetRowByProductCode(string productcode, int prodcodeidx)
        {
            var cells = new List<string>();
            var trlist = viewproducts.TableRows;
            IReadOnlyList<IWebElement> ntdlist = null;

            foreach (var tr in trlist)
            {
                var tdlist = viewproducts.RowCells(tr);
                if (tdlist.Count > 1)
                {
                    if (tdlist[prodcodeidx].GetAttribute("textContent").Trim() == productcode)
                    {
                        ntdlist = tdlist;
                        break;
                    }
                }
                else
                    break;
            }

            if (ntdlist != null)
            {
                foreach(var ntd in ntdlist)
                {
                    cells.Add(ntd.GetAttribute("textContent").Trim());
                }
            }

            return cells;
        }

        public void ClickInventoryTab()
        {
            viewproducts.InventoryTab.Click();
            Delay(100);
        }

        public List<string> GetInventoryTabHeaders()
        {
            var headerstr = new List<string>();
            var headers = viewproducts.InventoryTabTableHeaders;

            foreach (var header in headers)
            {
                headerstr.Add(header.GetAttribute("textContent"));
            }

            return headerstr;
        }

        public List<string> GetColumnVals(int colidx)
        {
            var colvals = new List<string>();
            var trows = viewproducts.InventoryTableRows;

            foreach (var tr in trows)
            {
                var rowcells = viewproducts.InventoryTableRowCells(tr);
                colvals.Add(rowcells[colidx].GetAttribute("textContent"));
            }

            return colvals;
        }

        public List<string> GetRowVals(int rowidx)
        {
            var rowvals = new List<string>();
            var trows = viewproducts.InventoryTableRows;
            var rowcells = viewproducts.InventoryTableRowCells(trows[rowidx]);

            foreach (var cell in rowcells)
            {
                rowvals.Add(cell.GetAttribute("textContent"));
            }

            return rowvals;
        }
    }
}
