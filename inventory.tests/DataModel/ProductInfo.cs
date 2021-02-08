using System;
using System.Collections.Generic;

namespace inventory.tests.DataModel
{
    public class ProductInfo
    {
        public string ProductCode { get; set; }
        public string ProductDesc { get; set; }
        public List<ProductWarehouse> Warehouses { get; set; }

        public ProductInfo()
        {
            Warehouses = new List<ProductWarehouse>();
        }
    }

    public class ProductWarehouse
    {
        public string WarehouseName { get; set; }
        public decimal AvailableQty { get; set; }
        public decimal OnPurchase { get; set; }
        public decimal StockOnHand { get; set; }

        public ProductWarehouse(string warehousename)
        {
            WarehouseName = warehousename;
        }
    }
}
