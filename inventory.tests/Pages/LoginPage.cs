using System;
using OpenQA.Selenium;

namespace inventory.tests.Pages
{
    public class LoginPage
    {
        private IWebDriver driver;

        public LoginPage(IWebDriver wd)
        {
            driver = wd;
        }

        public IWebElement UsernameTextbox
        {
            get { return driver.FindElement(By.Id("username")); }
        }

        public IWebElement PasswordTextbox
        {
            get { return driver.FindElement(By.Id("password")); }
        }

        public IWebElement LoginButton
        {
            get { return driver.FindElement(By.Id("btnLogOn")); }
        }

        public IWebElement SkipMFALink
        {
            get { return driver.FindElement(By.LinkText("Skip for now")); }
        }
    }
}
