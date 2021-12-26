using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab6Core
{
    //[TestFixture]
    class Demo
    {
        private IWebDriver driver;
        public IDictionary<string, object> vars { get; private set; }
        private IJavaScriptExecutor js;
        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            //ChromeOptions chromeOptions = new ChromeOptions();
            //driver = new RemoteWebDriver(new Uri("https://www.dns-shop.ru/"), chromeOptions.ToCapabilities());
            js = (IJavaScriptExecutor)driver;
            vars = new Dictionary<string, object>();
            //driver.Url = "https://www.dns-shop.ru/";
        }
        [TearDown]
        protected void TearDown()
        {
            driver.Quit();
        }
        [Test]
        public void opensitetest()
        {
            driver.Navigate().GoToUrl("https://www.dns-shop.ru/");
            driver.Manage().Window.Size = new System.Drawing.Size(1820, 900);
            driver.FindElement(By.CssSelector(".cart-link__lbl")).Click();
        }
        [Test]
        public void addingtofavorite()
        {
            driver.Navigate().GoToUrl("https://www.dns-shop.ru/");
            driver.Manage().Window.Size = new System.Drawing.Size(1820, 900);
            {
                var element = driver.FindElement(By.CssSelector(".banner-header .loaded"));
                Actions builder = new Actions(driver);
                builder.MoveToElement(element).Perform();
            }
            driver.FindElement(By.CssSelector(".btn-additional")).Click();
            driver.FindElement(By.CssSelector(".header-menu-wrapper > form > .ui-input-search > .ui-input-search__input")).Click();
            driver.FindElement(By.CssSelector(".header-menu-wrapper > form > .ui-input-search > .ui-input-search__input")).SendKeys("iphone");
            driver.FindElement(By.CssSelector(".header-menu-wrapper > form > .ui-input-search > .ui-input-search__input")).SendKeys(Keys.Enter);
            js.ExecuteScript("window.scrollTo(0,0)");
            js.ExecuteScript("window.scrollTo(0,122)");
            js.ExecuteScript("window.scrollTo(0,307)");
            js.ExecuteScript("window.scrollTo(0,300)");
            driver.FindElement(By.CssSelector(".catalog-product:nth-child(2) > .catalog-product__name > span")).Click();
            js.ExecuteScript("window.scrollTo(0,3)");
            driver.FindElement(By.CssSelector(".button-ui_icon")).Click();
            driver.FindElement(By.CssSelector(".wishlist-login-modal__buttons > .ui-link")).Click();
            var elements = driver.FindElements(By.CssSelector(".wishlist-link__badge"));
            Assert.True(elements.Count > 0);
            Assert.That(driver.FindElement(By.CssSelector(".wishlist-link__badge")).Text, Is.EqualTo("1"));
        }
        [Test]
        public void nothingmatches()
        {
            driver.Navigate().GoToUrl("https://www.dns-shop.ru/");
            driver.Manage().Window.Size = new System.Drawing.Size(1820, 900);
            driver.FindElement(By.CssSelector(".btn-additional")).Click();
            driver.FindElement(By.CssSelector(".header-menu-wrapper > form > .ui-input-search > .ui-input-search__input")).Click();
            driver.FindElement(By.CssSelector(".header-menu-wrapper > form > .ui-input-search > .ui-input-search__input")).SendKeys("Футболка PUMA");
            driver.FindElement(By.CssSelector(".header-menu-wrapper > form > .ui-input-search > .ui-input-search__input")).SendKeys(Keys.Enter);
            var elements = driver.FindElements(By.CssSelector(".empty-search-results__container-header"));
            Assert.True(elements.Count > 0);
        }
        [Test]
        public void openauthorization()
        {
            driver.Navigate().GoToUrl("https://www.dns-shop.ru/");
            driver.Manage().Window.Size = new System.Drawing.Size(1820, 900);
            driver.FindElement(By.CssSelector(".btn-additional")).Click();
            driver.FindElement(By.CssSelector(".header__login_button")).Click();
            {
                WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(30));
                wait.Until(driver => driver.FindElement(By.CssSelector(".form-entry-or-registry")).Displayed);
            }
        }
    }
}
