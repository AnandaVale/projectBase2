using OpenQA.Selenium;

namespace Base2Mantis.Pages

{
    public class HomePage : BasePage
    {
        private By userNameLocator = By.Name("username");

        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        public void NavigateToHomePage(string baseUrl)
        {
            Driver.Navigate().GoToUrl(baseUrl);
        }

        public string GetUserNamePlaceholder()
        {
            return Driver.FindElement(userNameLocator).GetAttribute("placeholder");
        }
    }
}
