using BoDi;
using TechTalk.SpecFlow;
using System;
using NUnit.Framework;
using inventory.tests.DataModel;
using inventory.tests.Utils;
using inventory.tests.Actions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace inventory.tests.Steps
{
    [Binding]
    public class StepDefs : BaseSpecStep
    {
        public StepDefs(ContextObject context, IObjectContainer ocontainer) : base(context, ocontainer)
        {
        }

        [Given(@"user logs into Unleashed")]
        public void UserLogsIntoUnleashed()
        {
            Context.Driver.Navigate().GoToUrl(ContextObject.AppURL);
            var login = new LoginPageAction(Context.Driver);
            login.Login("qa+tanghal@unl.sh", "PrintPaySilver01");
            login.ClickSkipMFA();
        }

        [When(@"navigates to (.*)")]
        [Given(@"navigates to (.*)")]
        public void UserNavigatesTo(string navpattern)
        {
            var leftmenu = new LeftMenuAction(Context.Driver);
            leftmenu.NavigateTo(navpattern);
        }

        [When(@"add product page required fields are populated with random values")]
        public void WhenAddProductRequiredFieldsFilled()
        {
            var proddict = GetScenarioVar<Dictionary<string, string>>(ScenarioVarKey.CREATED_PRODUCT)
                ?? new Dictionary<string, string>();
            var addprod = new AddProductPageAction(Context.Driver);
            var randstr = GenerateRandomChars(7);
            var prodcode = "TST" + randstr;
            var proddesc = "Test" + randstr;
            proddict.Add("Product Code", prodcode);
            proddict.Add("Product Description", proddesc);
            addprod.FillRequiredFields(prodcode, proddesc);
            SetScenarioVar(ScenarioVarKey.CREATED_PRODUCT, proddict);
        }

        [When(@"unit of measure (EA|KG|ML) is selected")]
        public void WhenUnitOfMeasureSelected(string unit)
        {
            var proddict = GetScenarioVar<Dictionary<string, string>>(ScenarioVarKey.CREATED_PRODUCT)
                ?? new Dictionary<string, string>();
            var addprod = new AddProductPageAction(Context.Driver);
            addprod.SelectUnitMeasure(unit);
            proddict.Add("Units", unit);
        }

        [When(@"product group (Consumable|Furniture|Material) is selected")]
        public void WhenProdGrpSelected(string grp)
        {
            var proddict = GetScenarioVar<Dictionary<string, string>>(ScenarioVarKey.CREATED_PRODUCT)
                ?? new Dictionary<string, string>();
            var addprod = new AddProductPageAction(Context.Driver);
            addprod.SelectProductGrp(grp);
            proddict.Add("Product Group", grp);
        }

        [Then(@"save button is clicked")]
        public void ThenSaveButtonClicked()
        {
            var addprod = new AddProductPageAction(Context.Driver);
            addprod.ClickSave();

            var msgcontent = addprod.GetMessagebox();
            Assert.AreEqual("Updating a Product", msgcontent.Title, "Messagebox title incorrect");
            Assert.AreEqual("You have updated the product successfully.", msgcontent.Text, "Messagebox content incorrect");
            Assert.IsTrue(addprod.IsMessageboxInvisible, "Success message was dismissed");
        }

        [When(@"displayed nav link (.*) is clicked")]
        public void WhenNavLinkClicked(string navlinktext)
        {
            var leftmenu = new LeftMenuAction(Context.Driver);
            leftmenu.ClickDisplayedNav(navlinktext);
        }

        [When(@"created product is searched")]
        public void WhenCreatedProductSearched()
        {
            var proddict = GetScenarioVar<Dictionary<string, string>>(ScenarioVarKey.CREATED_PRODUCT);
            var viewprod = new ViewProductsPageAction(Context.Driver);
            viewprod.FilterByProduct(proddict["Product Description"]);
        }

        [Then(@"created product is found and verified")]
        public void ThenCreatedProductFoundVerified()
        {
            var proddict = GetScenarioVar<Dictionary<string, string>>(ScenarioVarKey.CREATED_PRODUCT);
            var viewprod = new ViewProductsPageAction(Context.Driver);
            var headers = viewprod.GetHeaders();

            var cells = viewprod.GetRowByProductCode(proddict["Product Code"], headers.IndexOf("Product Code"));
            Assert.Greater(cells.Count, 0, "The created product " + proddict["Product Code"] + " was not found.");

            foreach (var kvp in proddict)
                Assert.AreEqual(kvp.Value, cells[headers.IndexOf(kvp.Key)], "Product info is incorrect");
        }

        [When(@"sales order for customer code (.*) created")]
        public void WhenSalesOrderForCustomer(string customercode)
        {
            var salesorder = new AddSalesOrderPageAction(Context.Driver);
            salesorder.EnterSelectCustomerCode(customercode);

            var newsale = new SalesOrderInfo(salesorder.GetOrderNumber());
            newsale.CustomerCode = customercode;
            newsale.OrderStatus = salesorder.GetOrderStatus();
            SetScenarioVar(ScenarioVarKey.SALES_ORDER, newsale);
        }

        [Given(@"(.*) has available stocks in different warehouses")]
        [When(@"(.*) was rechecked in different warehouses")]
        public void GivenHasAvailableStocks(string prodcode)
        {
            var prodcodes = prodcode.Split(',');
            var viewprod = new ViewProductsPageAction(Context.Driver);
            var leftmenu = new LeftMenuAction(Context.Driver);
            var prodinfolist = new List<ProductInfo>();

            if (GetScenarioVar<List<ProductInfo>>(ScenarioVarKey.PRODUCT_STOCK) == null)
                SetScenarioVar(ScenarioVarKey.PRODUCT_STOCK, prodinfolist);
            else
                SetScenarioVar(ScenarioVarKey.ACTUAL_STOCK, prodinfolist);

            foreach (var code in prodcodes)
            {
                var prodinfo = new ProductInfo();
                viewprod.FilterByProduct(code);
                viewprod.ClickProductCode(code);
                viewprod.ClickInventoryTab();
                var headers = viewprod.GetInventoryTabHeaders();
                var warehouses = viewprod.GetColumnVals(headers.IndexOf("Warehouse"));
                prodinfo.ProductCode = code;

                for (int i = 0; i < warehouses.Count; i++)
                {
                    var warehouse = new ProductWarehouse(warehouses[i]);
                    var cells = viewprod.GetRowVals(i);
                    warehouse.AvailableQty = decimal.Parse(cells[headers.IndexOf("Available Qty")]);
                    warehouse.OnPurchase = decimal.Parse(cells[headers.IndexOf("On Purchase")]);
                    warehouse.StockOnHand = decimal.Parse(cells[headers.IndexOf("Stock On Hand")]);
                    prodinfo.Warehouses.Add(warehouse);
                }

                prodinfolist.Add(prodinfo);
                leftmenu.ClickDisplayedNav("View Products");
            }
        }

        [When(@"(.*) selected as product sale source")]
        public void WhenWarehouseSelectedSource(string warehouse)
        {
            var newsale = GetScenarioVar<SalesOrderInfo>(ScenarioVarKey.SALES_ORDER);
            var salesorder = new AddSalesOrderPageAction(Context.Driver);
            salesorder.SelectWarehouse(warehouse);
            newsale.Warehouse = warehouse;
        }

        [When(@"(.*) qty (\d+) product added into sales order lines")]
        public void WhenWarehouseSelectedSource(string prodcode, string qty)
        {
            var newsale = GetScenarioVar<SalesOrderInfo>(ScenarioVarKey.SALES_ORDER);
            var salesorder = new AddSalesOrderPageAction(Context.Driver);
            salesorder.EnterSelectProductCode(prodcode);
            salesorder.EnterQty(qty);
            salesorder.ClickAddOrderLine();

            Assert.IsTrue(salesorder.IsLinkTextPresentInOrderLines(prodcode), prodcode + " link was not present");

            var nqty = decimal.Parse(qty);
            var orderline = new OrderLine(prodcode);
            orderline.Qty = nqty;
            newsale.Orderlines.Add(orderline);
        }

        [Then(@"sales order (Placed|Completed)")]
        public void ThenOrderPlacedCompleted(string stat)
        {
            var newsale = GetScenarioVar<SalesOrderInfo>(ScenarioVarKey.SALES_ORDER);
            var salesorder = new AddSalesOrderPageAction(Context.Driver);
            var addprod = new AddProductPageAction(Context.Driver);
            var status = salesorder.GetOrderStatus().ToLower();

            if (status == "parked" || status == "placed")
            {
                if (stat == "Completed")
                {
                    salesorder.ClickComplete();
                    salesorder.ClickYesModal();
                    newsale.OrderStatus = salesorder.GetOrderStatus();
                    UpdateStock();
                }
                else
                {
                    salesorder.ClickPlaced();
                    newsale.OrderStatus = salesorder.GetOrderStatus();
                    Assert.AreEqual("Placed", newsale.OrderStatus, "Incorrect sales order status");
                }

                var msgcontent = addprod.GetMessagebox();
                Assert.AreEqual("Save Order", msgcontent.Title, "Messagebox title incorrect");
                Assert.AreEqual($"You have successfully saved Sales Order {newsale.OrderNumber}. Enter a new Sales Order?", msgcontent.Text, "Messagebox content incorrect");
                Assert.IsTrue(addprod.IsMessageboxInvisible, "Success message was dismissed");
                Assert.AreEqual(stat, newsale.OrderStatus, "Incorrect sales order status");
            }
        }

        [Then(@"stocks are updated")]
        public void ThenStocksUpdated()
        {
            var expected = GetScenarioVar<List<ProductInfo>>(ScenarioVarKey.PRODUCT_STOCK);
            var actual = GetScenarioVar<List<ProductInfo>>(ScenarioVarKey.ACTUAL_STOCK);

            for (int i = 0; i < expected.Count; i++)
            {
                for (int j = 0; j < expected[i].Warehouses.Count; j++)
                {
                    var warehousename = expected[i].Warehouses[j].WarehouseName;
                    Assert.AreEqual(expected[i].ProductCode, actual[i].ProductCode, "Product code mismatch");
                    Assert.AreEqual(warehousename, actual[i].Warehouses[j].WarehouseName, "Warehouse mismatch" + warehousename);
                    Assert.AreEqual(expected[i].Warehouses[j].AvailableQty, actual[i].Warehouses[j].AvailableQty, "Available Qty mismatch in " + warehousename);
                    Assert.AreEqual(expected[i].Warehouses[j].StockOnHand, actual[i].Warehouses[j].StockOnHand, "Stock On Hand mismatch in " + warehousename);
                }
            }
        }

        private void UpdateStock()
        {
            var newsale = GetScenarioVar<SalesOrderInfo>(ScenarioVarKey.SALES_ORDER);
            var prodlist = GetScenarioVar<List<ProductInfo>>(ScenarioVarKey.PRODUCT_STOCK);

            foreach (var orderline in newsale.Orderlines)
            {
                var prod = prodlist.Find(p => p.ProductCode == orderline.ProductCode);
                var warehouse = prod.Warehouses.Find(w => w.WarehouseName == newsale.Warehouse);
                warehouse.AvailableQty -= orderline.Qty;
                warehouse.StockOnHand -= orderline.Qty;
                warehouse = prod.Warehouses.Find(w => w.WarehouseName == "Global");
                warehouse.AvailableQty -= orderline.Qty;
                warehouse.StockOnHand -= orderline.Qty;
            }
        }

        [Given(@"user can access products API")]
        public void GivenUserAccessProductsAPI()
        {
            SetScenarioVar(TaskKey.PRODGROUPS, Context.Products.GetProductGroups());
            SetScenarioVar(TaskKey.UNITSOFMEASURE, Context.Products.GetUnitMeasures());
            SetScenarioVar(TaskKey.PRODLIST, Context.Products.GetProducts());
        }

        [Then(@"user is able to get 200 OK response code on product group, unit of measures and product list")]
        public async Task ThenUserAccessProductsAPI()
        {
            var prodgrps = await GetScenarioVar<Task<ProductGroupList>>(TaskKey.PRODGROUPS);
            var unitmeasures = await GetScenarioVar<Task<UnitMeasureList>>(TaskKey.UNITSOFMEASURE);
            var prodlist = await GetScenarioVar<Task<ProductList>>(TaskKey.PRODLIST);

            AssertHTTPResponse("200", "OK", prodgrps);
            AssertHTTPResponse("200", "OK", unitmeasures);
            AssertHTTPResponse("200", "OK", prodlist);
        }

        [Then(@"existing product code (.*) is found in the product list")]
        public async Task ThenUserAccessProductsAPI(string prodcode)
        {
            var prodlist = await GetScenarioVar<Task<ProductList>>(TaskKey.PRODLIST);
            var item = prodlist.Items.Find(i => i.ProductCode == prodcode);
            Assert.Greater(prodlist.Pagination.NumberOfItems, 0, "No items found on product list");
            Assert.AreEqual(prodlist.Pagination.NumberOfItems, prodlist.Items.Count, "Item count mismatch");
            Assert.NotNull(item, prodcode + " not found in the product list API");
        }

        [When(@"a new product is created with (.*) product group, (.*) unit of measure")]
        public async Task WhenNewProductCreated(string prodgroup, string unit)
        {
            var prodgrps = await GetScenarioVar<Task<ProductGroupList>>(TaskKey.PRODGROUPS);
            var unitmeasures = await GetScenarioVar<Task<UnitMeasureList>>(TaskKey.UNITSOFMEASURE);

            var payload = new ProductPayload();
            payload.UnitOfMeasure = unitmeasures.Items.Find(u => u.Name.ToLower() == unit.ToLower());

            var grp = prodgrps.Items.Find(g => g.GroupName.ToLower() == prodgroup.ToLower());
            payload.Group.GroupName = grp.GroupName;
            payload.Group.ID = grp.ID;

            var filler = GenerateRandomChars(7);
            payload.ProductCode = "API" + filler;
            payload.ProductDescription = "POST " + payload.ProductCode;

            SetScenarioVar(TaskKey.CREATEPROD, Context.Products.PostProduct(payload));
            SetScenarioVar(ScenarioVarKey.CREATED_PRODUCT_API, payload);
        }

        [Then(@"a new product is created")]
        [Then(@"product is updated")]
        public async Task ThenNewProductCreatedAPI()
        {
            var newitem = await GetScenarioVar<Task<ProductItem>>(TaskKey.CREATEPROD);
            var payload = GetScenarioVar<ProductPayload>(ScenarioVarKey.CREATED_PRODUCT_API);
            SetScenarioVar(TaskKey.PRODLIST, Context.Products.GetProducts());
            var prodlist = await GetScenarioVar<Task<ProductList>>(TaskKey.PRODLIST);

            AssertHTTPResponse("201", "Created", newitem);
            AssertHTTPResponse("200", "OK", prodlist);
            Assert.AreEqual(payload.ID, newitem.ID, "Guid is incorrect");
            Assert.AreEqual(payload.ProductCode, newitem.ProductCode, "Product code is incorrect");
            Assert.AreEqual(payload.ProductDescription, newitem.ProductDescription, "Product desc is incorrect");
            Assert.AreEqual(payload.UnitOfMeasure.Name, newitem.UnitOfMeasure.Name, "Unit of measure is incorrect");
            Assert.AreEqual(payload.Group.GroupName, newitem.Group.GroupName, "Product group is incorrect");

            var item = prodlist.Items.Find(p => p.ID == payload.ID);
            Assert.NotNull(item, payload.ProductCode + " is not found on the list");
        }

        
        [When(@"a product is updated with description '(.*)'")]
        public void WhenUpdateProductAPI(string desc)
        {
            var payload = GetScenarioVar<ProductPayload>(ScenarioVarKey.CREATED_PRODUCT_API);
            payload.ProductDescription += " " + desc;
            SetScenarioVar(TaskKey.CREATEPROD, Context.Products.PostProduct(payload));
        }
    }
}