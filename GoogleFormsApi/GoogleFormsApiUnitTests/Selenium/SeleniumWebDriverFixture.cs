using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace GoogleFormsApiUnitTests.Selenium
{
    public class SeleniumWebDriverFixture : IDisposable
    {
        public ChromeDriver ChromeDriver { get; set; }

        public SeleniumWebDriverFixture()
        {
            var driver = new DriverManager().SetUpDriver(new ChromeConfig());
            ChromeDriver = new ChromeDriver(driver);
        }

        public void Dispose()
        {
            ChromeDriver.Quit();
            ChromeDriver.Dispose();
        }
    }
}
