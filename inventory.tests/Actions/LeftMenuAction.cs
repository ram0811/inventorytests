using inventory.tests.Pages;
using OpenQA.Selenium;

namespace inventory.tests.Actions
{
    public class LeftMenuAction : BaseAction
    {
        private LeftMenuPanel leftmenu;

        public LeftMenuAction(IWebDriver wd) : base(wd)
        {
            leftmenu = new LeftMenuPanel(wd);
        }

        public void NavigateTo(string navpattern)
        {
            var navparts = navpattern.Split('.');
            var elem = leftmenu.GetNavLink(navparts[0]);
            elem.Click();

            for (int i = 1; i < navparts.Length; i++)
            {
                elem = leftmenu.GetSubNavLink(elem, navparts[i]);
                elem.Click();
            }

            WaitPageLoad();
        }

        public void ClickDisplayedNav(string linktext)
        {
            var elem = leftmenu.GetDispliayedNavLink(linktext);
            if (elem.Displayed && elem.Enabled)
                elem.Click();

            WaitPageLoad();
        }
    }
}
