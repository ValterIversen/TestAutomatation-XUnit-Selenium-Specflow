using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using TestAutomatation.SiteChacara.Config;

namespace TestAutomatation.SiteChacara.Support
{
    public static class WebDriverFactory
    {
        public static IWebDriver CreateWebDriver(Browser browser, bool headless)
        {
            IWebDriver webDriver = null;

            switch (browser)
            {
                case Browser.Firefox:
                    var optionsFirefox = new FirefoxOptions();
                    if (headless)
                        optionsFirefox.AddArgument("--headless");

                    webDriver = new FirefoxDriver(Environment.CurrentDirectory, optionsFirefox);

                    break;
                case Browser.Chrome:
                    var optionsChrome = new ChromeOptions();

                    if (headless)
                        optionsChrome.AddArgument("--headless");

                    webDriver = new ChromeDriver(Environment.CurrentDirectory, optionsChrome);

                    break;
            }

            return webDriver;
        }
    }
}
