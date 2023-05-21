
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using TestAutomatation.Logs.DAL.LogsTestes;
using TestAutomatation.SiteChacara.Config;
using TestAutomatation.TestLinkIntegration;
using Xunit;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace TestAutomatation.SiteChacara.Support
{
    public class SeleniumHelper : IDisposable
    {
        public IWebDriver WebDriver;
        public readonly ConfigurationHelper Configuration;
        public WebDriverWait Wait;
        public System.Drawing.Size Resolucao;
        public Stopwatch TempoTeste;

        public SeleniumHelper(Browser browser)
        {
            TempoTeste = new Stopwatch();
            TempoTeste.Start();
            Configuration = new ConfigurationHelper();
            WebDriver = WebDriverFactory.CreateWebDriver(browser, Configuration.Headless);
            WebDriver.Manage().Window.Maximize();
            Resolucao = WebDriver.Manage().Window.Size;
            Wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(20));
        }

        public string GetUrl()
        {
            return WebDriver.Url;
        }

        public void GoToUrl(string url)
        {
            WebDriver.Navigate().GoToUrl(url);
        }

        public void LinkClickByText(string linkText)
        {
            var link = Wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText(linkText)));
            link.Click();
        }

        public void ClickById(string elementoId)
        {
            var botao = Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(elementoId)));
            botao.Click();
        }
        public void ClickByClassName(string className)
        {
            var botao = Wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName(className)));
            botao.Click();
        }

        public IWebElement GetElementById(string id)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id)));
        }

        public IWebElement GetElementByClass(string className)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName(className)));
        }

        public void FillTextBoxById(string idCampo, string valorCampo)
        {
            var campo = Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(idCampo)));
            campo.SendKeys(valorCampo);
        }

        public string SelectRandonIndexDropDown(string idDropDown)
        {
            Random r = new();

            SelectElement dropdown = new(GetElementById(idDropDown));

            dropdown.SelectByIndex(r.Next(1, dropdown.Options.Count));

            string selecionado = dropdown.SelectedOption.Text;

            return selecionado;
        }

        public void DismissChormeAlert()
        {
            var alert = WebDriver.SwitchTo().Alert();
            alert.Dismiss();
        }

        public string SelectIndexDropDown(string idDropDown, int index)
        {

            SelectElement dropdown = new(GetElementById(idDropDown));

            dropdown.SelectByIndex(index);

            string selecionado = dropdown.SelectedOption.Text;

            return selecionado;
        }

        public void FillDropDownById(string idCampo, string valorCampo)
        {
            var campo = Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(idCampo)));
            var selectElement = new SelectElement(campo);
            selectElement.SelectByValue(valorCampo);
        }

        public string GetTextElementById(string id)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id))).Text;
        }

        public string GetTextElementByClass(string className)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName(className))).Text;
        }

        public string GetTextBoxValueById(string id)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id)))
                .GetAttribute("value");
        }

        public string GetDropDownValueById(string idDropDown)
        {
            SelectElement dropdown = new(GetElementById(idDropDown));

            string selecionado = dropdown.SelectedOption.Text;

            return selecionado;
        }

        public IEnumerable<IWebElement> GetListByClass(string className)
        {
            return Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.ClassName(className)));
        }

        public bool ExistValidationById(string id)
        {
            return Exist(By.Id(id));
        }
        public bool ExistValidationByClassName(string className)
        {
            return Exist(By.ClassName(className));
        }

        private bool Exist(By by)
        {
            try
            {
                WebDriver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void GetScreenShot(string nome)
        {
            SaveScreenShot(WebDriver.TakeScreenshot(), string.Format("{0}_" + nome + ".png", DateTime.Now.ToFileTime()));
        }

        private void SaveScreenShot(Screenshot screenshot, string fileName)
        {
            screenshot.SaveAsFile($"{Configuration.FolderPicture}{fileName}", ScreenshotImageFormat.Png);
        }

        public void Dispose()
        {
            WebDriver.Quit();
            WebDriver.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
