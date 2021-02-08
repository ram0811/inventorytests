using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace inventory.tests.Pages
{
    public class AddProductPage
    {
        private IWebDriver driver;

        public AddProductPage(IWebDriver wd)
        {
            driver = wd;
        }

        public IWebElement ProductCodeTextbox
        {
            get { return driver.FindElement(By.Id("Product_ProductCode")); }
        }

        public IWebElement ProductDescTextarea
        {
            get { return driver.FindElement(By.Id("Product_ProductDescription")); }
        }

        public IWebElement SaveButton
        {
            get { return driver.FindElement(By.Id("btnSave")); }
        }

        public IWebElement DeleteButton
        {
            get { return driver.FindElement(By.Id("DeleteButton")); }
        }

        public SelectElement UnitMeasureSelectbox
        {
            get
            {
                var elem = driver.FindElement(By.Id("Product_UnitOfMeasureId"));
                return new SelectElement(elem);
            }
        }

        public SelectElement ProductGrpSelectbox
        {
            get
            {
                var elem = driver.FindElement(By.Id("Product_ProductGroupId"));
                return new SelectElement(elem);
            }
        }

        public IWebElement Messagebox
        {
            get
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                var elem = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div.ui-pnotify")));
                return elem;
            }
        }

        public string GetMessageboxTitle(ISearchContext msgbox)
        {
            return msgbox.FindElement(By.CssSelector("h4.ui-pnotify-title")).GetAttribute("textContent");
        }

        public string GetMessageboxText(ISearchContext msgbox)
        {
            return msgbox.FindElement(By.CssSelector("div.ui-pnotify-text")).GetAttribute("textContent");
        }

        public IWebElement GetMessageboxCloseBtn(ISearchContext msgbox)
        {
            return msgbox.FindElement(By.CssSelector("div.ui-pnotify-closer"));
        }

        public bool IsMessageboxInvisible
        {
            get
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                var invi = wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector("div.ui-pnotify")));
                return invi;
            }
        }
    }
}
