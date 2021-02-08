using OpenQA.Selenium;

namespace inventory.tests.Pages
{
    public class LeftMenuPanel
    {
        private IWebDriver driver;

        public LeftMenuPanel(IWebDriver wd)
        {
            driver = wd;
        }

        public IWebElement GetNavLink(string navtext)
        {
            return driver.FindElement(By.XPath($"//div[@class='menu-group']/ul/li[./a/span[text()='{navtext}']]"));
        }

        public IWebElement GetSubNavLink(ISearchContext sc, string navtext)
        {
            return sc.FindElement(By.XPath($"./ul/li[./a/span[text()='{navtext}']]"));
        }

        public IWebElement GetDispliayedNavLink(string navtext)
        {
            return driver.FindElement(By.XPath($"//div[@class='menu-group']//li[./a/span[text()='{navtext}']]"));
        }
    }
}
