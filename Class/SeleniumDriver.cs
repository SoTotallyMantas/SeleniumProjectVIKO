using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumProjectVIKO.Class
{
    internal class SeleniumDriver
    {
        private IWebDriver _webDriver;
        private WebDriverWait _wait;

        public SeleniumDriver(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            _wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(2));
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        }
        

        public void GoToUrl(string url)
        {
            _webDriver.Navigate().GoToUrl(url);
        }

        public IWebElement FindElementById(string id)
        {
            return _webDriver.FindElement(By.Id(id));
        }
        public IWebElement FindElementByName(string name) 
        {
            return _webDriver.FindElement(By.Name(name));
        }

        public IWebElement FindElementByClassName(string className)
        {
            return _webDriver.FindElement(By.CssSelector("." + className.Replace(" ", ".")));
        }

        public IWebElement FindElementByXPath(string xPath)
        {
            return _webDriver.FindElement(By.XPath(xPath));
        }

        public IWebElement FindElementByCssSelector(string cssSelector)
        {
            return  _webDriver.FindElement(By.CssSelector(cssSelector));
        }

        public void Click(object element)
        {
            ((IWebElement)element).Click();
        }

        public void EnterText(object element, string text)
        {
            ((IWebElement)element).SendKeys(text);
        }




        public void Close()
        {
            _webDriver.Close();
        }


    }
}
