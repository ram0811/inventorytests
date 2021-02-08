using System;
using inventory.tests.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace inventory.tests.Actions
{
    public class BaseAction
    {
        protected IWebDriver driver;

        public BaseAction(IWebDriver wd)
        {
            driver = wd;
        }

        protected void WaitPageLoad()
        {
            WaitPageLoad(TimeSpan.FromMinutes(2));
        }

        protected void WaitPageLoad(TimeSpan ts)
        {
            Delay(200);
            var js = (IJavaScriptExecutor)driver;
            var wait = new WebDriverWait(driver, ts);
            wait.Until(wd => js.ExecuteScript("return document.readyState").ToString() == "complete");
            Delay(200);
        }

        protected void WaitAjaxLoader()
        {
            Delay(20);
            var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("ajaxLoad")));
            Delay(200);
        }

        protected void Delay(int ms)
        {
            BrowserSupport.Delay(ms);
        }
    }
}
