using System;

namespace inventory.tests.Utils
{
    public class ScenarioVarKey
    {
        public static readonly string CREATED_PRODUCT = "CREATED_PRODUCT";
        public static readonly string PRODUCT_STOCK = "PRODUCT_STOCK";
        public static readonly string ACTUAL_STOCK = "ACTUAL_STOCK";
        public static readonly string SALES_ORDER = "SALES_ORDER";
        public static readonly string CREATED_PRODUCT_API = "CREATED_PRODUCT_API";
    }

    public class HeaderKey
    {
        public static readonly string CONTENT = "Content-Type";
        public static readonly string ACCEPT = "Accept";
        public static readonly string AUTHID = "api-auth-id";
        public static readonly string SIGN = "api-auth-signature";
        public static readonly string CLIENT = "client-type";
    }

    public class HeaderValue
    {
        public static readonly string APPJSON = "application/json";
        public static readonly string APPXML = "application/xml";
        public static readonly string XYZCOMPANY = "xyzcompany";
    }

    public class TaskKey
    {
        public static readonly string PRODGROUPS = "TASK_PRODGROUPS";
        public static readonly string UNITSOFMEASURE = "TASK_UNITSOFMEASURE";
        public static readonly string PRODLIST = "TASK_PRODLIST";
        public static readonly string CREATEPROD = "TASK_CREATEPROD";
    }
}
