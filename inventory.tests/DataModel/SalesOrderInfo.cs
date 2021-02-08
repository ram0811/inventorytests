using System;
using System.Collections.Generic;

namespace inventory.tests.DataModel
{
    public class SalesOrderInfo
    {
        public string OrderNumber { get; set; }
        public string Warehouse { get; set; }
        public string OrderStatus { get; set; }
        public string CustomerCode { get; set; }
        public List<OrderLine> Orderlines { get; set; }

        public SalesOrderInfo(string ordernum)
        {
            Orderlines = new List<OrderLine>();
            OrderNumber = ordernum;
        }
    }

    public class OrderLine
    {
        public string ProductCode { get; set; }
        public decimal Qty { get; set; }

        public OrderLine(string prodcode)
        {
            ProductCode = prodcode;
        }
    }
}
