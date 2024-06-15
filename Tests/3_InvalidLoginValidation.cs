using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using Base2Mantis.Config;
using Base2Mantis.Pages;

namespace InvalidLoginValidation
{
    [TestFixture]
    public class LoginInvalidTest
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
        public void TestLoginInvalido()
        {
            if (driver == null)
            {
                Console.WriteLine("O driver não foi inicializado.");
                Assert.Fail("O driver não foi inicializado.");
                return;
            }

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            IWebElement usernameField = wait.Until(d => d.FindElement(By.Name("username")));
            usernameField.SendKeys("Usuário_teste");

            IWebElement loginButton = wait.Until(d => d.FindElement(By.XPath("//input[@type='submit' and @value='Entrar']")));
            loginButton.Click();

            IWebElement errorMessage = wait.Until(d => d.FindElement(By.XPath("//div[@class='alert alert-danger']")));
            string expectedErrorMessage = "Sua conta pode estar desativada ou bloqueada ou o nome de usuário e a senha que você digitou não estão corretos.";
            Assert.AreEqual(expectedErrorMessage, errorMessage.Text);

        }

        [TearDown]
        public void TearDown()
        {
            driver?.Quit();
        }
    }
}
