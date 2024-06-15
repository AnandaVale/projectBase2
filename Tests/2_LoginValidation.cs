using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using Base2Mantis.Config;
using Base2Mantis.Pages;

namespace LoginValidation
{
    [TestFixture]
    public class LoginTest
    {
        private IWebDriver? driver = null;
        private HomePage? homePage = null;

        [SetUp]
        public void SetUp()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--incognito");
            driver = new ChromeDriver(chromeOptions);
            homePage = new HomePage(driver);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            homePage.NavigateToHomePage(AppConfig.BaseUrl);
        }

        [Test]
        public void TestLogin()
        {
            if (driver == null)
            {
                Console.WriteLine("O driver não foi inicializado.");
                Assert.Fail("O driver não foi inicializado.");
                return;
            }

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));

            IWebElement usernameElement = wait.Until(d => d.FindElement(By.Name("username")));
            usernameElement.SendKeys("Ananda_Vale");

            IWebElement loginButton = wait.Until(d => d.FindElement(By.XPath("//input[@type='submit' and @value='Entrar']")));
            loginButton.Click();

            IWebElement passwordElement = wait.Until(d => d.FindElement(By.Name("password")));
            passwordElement.SendKeys("judith310123");

            IWebElement loginPasswordButton = wait.Until(d => d.FindElement(By.XPath("//input[@type='submit' and @value='Entrar']")));
            loginPasswordButton.Click();

            IWebElement navbarElement = wait.Until(d => d.FindElement(By.Id("navbar")));

            string expectedElement = "navbar";
            string correctElement = navbarElement.GetAttribute("id");
            Assert.AreEqual(expectedElement, correctElement);
        }

        [TearDown]
        public void TearDown()
        {
            driver?.Quit();
        }
    }
}