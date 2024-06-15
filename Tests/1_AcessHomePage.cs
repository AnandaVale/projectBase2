using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Base2Mantis.Config;
using Base2Mantis.Pages;

namespace Base2Mantis.Tests
{
    public class HomePageTest
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
        public void TestAcessPage()
        {
            if (driver == null)
            {
                Console.WriteLine("O driver não foi inicializado.");
                Assert.Fail("O driver não foi inicializado.");
                return;
            }

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement userNameElement = wait.Until(d => d.FindElement(By.Name("username")));

            string expectedElement = "Nome de usuário";
            string correctElement = userNameElement.GetAttribute("placeholder");
            Assert.AreEqual(expectedElement, correctElement);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
