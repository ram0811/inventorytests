using System;
using inventory.tests.RestClient;
using OpenQA.Selenium;

namespace inventory.tests.DataModel
{
    public class ContextObject
    {
        public static string Browser { get; set; }
        public static string BrowserVersion { get; set; }
        public static string AppURL { get; set; }
        public static string TestDataPath { get; set; }
        public static string ApiURL { get; set; }
        public IWebDriver Driver { get; set; }
        public ProductsAPI Products { get; set; }
    }
}
