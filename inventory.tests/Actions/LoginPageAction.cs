using System;
using inventory.tests.Pages;
using OpenQA.Selenium;

namespace inventory.tests.Actions
{
    public class LoginPageAction : BaseAction
    {
        private LoginPage loginpage;

        public LoginPageAction(IWebDriver wd) : base(wd)
        {
            loginpage = new LoginPage(wd);
        }

        public void Login(string uname, string pword)
        {
            var unametb = loginpage.UsernameTextbox;
            var pwordtb = loginpage.PasswordTextbox;

            unametb.Clear();
            unametb.SendKeys(uname);
            pwordtb.Clear();
            pwordtb.SendKeys(pword);
            loginpage.LoginButton.Click();
            WaitPageLoad();
        }

        public void ClickSkipMFA()
        {
            try
            {
                loginpage.SkipMFALink.Click();
                WaitPageLoad();
            } catch { }
        }
    }
}
