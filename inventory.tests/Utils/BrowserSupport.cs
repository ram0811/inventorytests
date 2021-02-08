using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;

namespace inventory.tests.Utils
{
    public class BrowserSupport
    {
        public static IWebDriver StartBrowser(string browsername)
        {
            IWebDriver driver;

            switch (browsername.ToUpper())
            {
                case "SAFARI": driver = StartSafari();
                    break;
                case "FF":
                case "FIREFOX": driver = StartFirefox();
                    break;
                default: driver = StartChrome();
                    break;
            }

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(2);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            return driver;
        }

        public static void Delay(int ms)
        {
            Thread.Sleep(ms);
        }

        private static IWebDriver StartChrome()
        {
            var opts = new ChromeOptions();
            opts.AddArguments("--disable-extensions");
            opts.AddArguments("--no-sandbox");

            return new ChromeDriver(opts); ;
        }

        private static IWebDriver StartFirefox()
        {
            var opts = new FirefoxOptions();
            return new FirefoxDriver(opts); ;
        }

        private static IWebDriver StartSafari()
        {
            var opts = new SafariOptions();
            return new SafariDriver(opts);
        }
    }
}
