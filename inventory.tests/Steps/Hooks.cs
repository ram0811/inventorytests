using BoDi;
using System;
using System.Configuration;
using System.Collections.Generic;
using NUnit.Framework;
using TechTalk.SpecFlow;
using inventory.tests.DataModel;
using inventory.tests.Utils;
using inventory.tests.RestClient;

namespace inventory.tests.Steps
{
    [Binding]
    public class Hooks : BaseSpecStep
    {
        public Hooks(ContextObject context, IObjectContainer ocontainer) : base(context, ocontainer)
        {
        }

        [BeforeTestRun]
        public static void RunTest()
        {
            BaseClient.SetApiIDKey("b21f6288-3a59-4f30-8c32-3661a26ce92a",
                "ac8xXW76L7F3Xd8mUSYB1Z2aDTppr4rjxahplvM71QNwDz1RGLWoYCo1xgLL8tiS6YbSJMpcAVKSkdrG5rQ==",
                "https://api.unleashedsoftware.com");
            ContextObject.Browser = "chrome";
            ContextObject.AppURL = "https://au.unleashedsoftware.com/v2/Account/LogOn";
            ContextObject.ApiURL = "https://google.com";
        }

        [BeforeScenario]
        public void SetUp()
        {
            Context.Products = new ProductsAPI();
            foreach (var tag in ScenContext.ScenarioInfo.Tags)
                if (tag == "ui")
                {
                    Context.Driver = BrowserSupport.StartBrowser(ContextObject.Browser);
                    break;
                }
        }

        [AfterScenario]
        public void CleanUp()
        {
            Context.Driver?.Close();
        }
    }
}
