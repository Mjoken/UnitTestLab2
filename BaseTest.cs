using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WikipediaTests
{
    public class BaseTest
    {
        protected IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver(); // Инициализация Chrome
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.wikipedia.org/");
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit(); // Закрытие браузера после теста
        }
    }
}