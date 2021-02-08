using System;
using Newtonsoft.Json;

namespace inventory.tests.DataModel
{
    public class SalesOrderList : BaseResponse
    {
		public PaginationInfo Pagination { get; set; }
		/*
sales order
Guid
OrderStatus
Customer
	CustomerCode
	Guid
Tax
	TaxCode
	TaxRate
	Guid
TaxRate
Total = TotalTax + SubTotal
SubTotal
TaxTotal
Warehouse
	WarehouseCode
	Guid

Lines
{
	"DiscountRate": 0,
	"Guid": guidxxx,
	"LineNumber": 1,
	"LineType": null,
	"LineTax": 27.75,
	"LineTotal": 185,
	"OrderQuantity": 1,
	"TaxRate": 0.15,
	"UnitPrice": 185,
	"Product": {
		"ProductCode":,
		"Guid":,
	}
}
        */
	}

	public class SalesOrder
    {
		[JsonProperty("Guid")]
		public string ID { get; set; }
		public string OrderStatus { get; set; }
		[JsonProperty("Customer")]
		public Customer CustomerObj { get; set; }
		[JsonProperty("Tax")]
		public Tax TaxObj { get; set; }
		public decimal TaxRate { get; set; }
		public decimal Total { get; set; }
		public decimal SubTotal { get; set; }
		public decimal TaxTotal { get; set; }
	}

	public class Customer
    {
		[JsonProperty("Guid")]
		public string ID { get; set; }
		public string CustomerCode { get; set; }
	}

	public class Tax
    {
		[JsonProperty("Guid")]
		public string ID { get; set; }
		public string TaxCode { get; set; }
		public decimal TaxRate { get; set; }
	}

	public class Warehouse
    {
		[JsonProperty("Guid")]
		public string ID { get; set; }
		public string WarehouseCode { get; set; }
		public string WarehouseName { get; set; }
	}

	public class SalesOrderLine
    {
		[JsonProperty("Guid")]
		public string ID { get; set; }
		public int LineNumber { get; set; }
		public string LineType { get; set; }
		public decimal LineTax { get; set; }
		public decimal LineTotal { get; set; }
		public decimal OrderQuantity { get; set; }
		public decimal DiscountRate { get; set; }
		public decimal TaxRate { get; set; }
		public decimal UnitPrice { get; set; }
	}
}
