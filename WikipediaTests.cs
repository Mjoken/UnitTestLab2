using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace WikipediaTests
{
    public class WikipediaTests : BaseTest
    {
        // Тест 1: Проверка заголовка
        [Test]
        public void TitleContainsWikipedia()
        {
            Assert.That(driver.Title, Does.Contain("Wikipedia"));
        }

        // Тест 2: Видимость логотипа
        [Test]
        public void LogoIsDisplayed()
        {
            IWebElement logo = driver.FindElement(By.CssSelector(".central-featured-logo"));
            Assert.That(logo.Displayed, Is.True);
        }

        // Тест 3: Видимость поисковой строки
        [Test]
        public void SearchInputIsVisible()
        {
            IWebElement searchInput = driver.FindElement(By.Id("searchInput"));
            Assert.That(searchInput.Displayed, Is.True);
        }

        // Тест 4: Активность кнопки поиска
        [Test]
        public void SearchButtonIsClickable()
        {
            IWebElement searchButton = driver.FindElement(By.CssSelector("button[type='submit']"));
            Assert.That(searchButton.Enabled, Is.True);
        }

        // Тест 5: Переход на английскую версию
        [Test]
        public void SwitchToEnglishVersion()
        {
            IWebElement englishLink = driver.FindElement(By.CssSelector("a[href='//en.wikipedia.org/']"));
            englishLink.Click();
            Assert.That(driver.Url, Does.Contain("en.wikipedia.org"));
        }

        // Тест 6: Поиск статьи
        [Test]
        public void SearchForValidTerm()
        {
            IWebElement searchInput = driver.FindElement(By.Id("searchInput"));
            searchInput.SendKeys("Selenium (software)");
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement title = wait.Until(d => d.FindElement(By.Id("firstHeading")));
            Assert.That(title.Text, Is.EqualTo("Selenium (software)"));
        }

        // Тест 7: Проверка футера
        [Test]
        public void FooterContainsLinks()
        {
            IWebElement footer = driver.FindElement(By.CssSelector("footer"));
            Assert.That(footer.FindElements(By.TagName("a")).Count, Is.GreaterThan(0));
        }

        // Тест 8: Проверка меню навигации
        [Test]
        public void NavigationMenuInteraction()
        {
            driver.FindElement(By.Id("js-link-box-en")).Click();
            driver.FindElement(By.CssSelector("#vector-main-menu-dropdown")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            IWebElement menu = wait.Until(d => d.FindElement(By.CssSelector("#p-navigation")));
            Assert.That(menu.Displayed, Is.True);
        }

        // Тест 9: Поиск неверного запроса
        [Test]
        public void SearchForInvalidTerm()
        {
            IWebElement searchInput = driver.FindElement(By.Id("searchInput"));
            searchInput.SendKeys("InvalidTerm12345");
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            IWebElement errorMessage = wait.Until(d => d.FindElement(By.CssSelector(".mw-search-none-results")));
            Assert.That(errorMessage.Text, Does.Contain("There were no results matching the query"));
        }

        // Тест 10: Случайная статья
        [Test]
        public void RandomArticleLink()
        {
            IWebElement randomLink = driver.FindElement(By.CssSelector("a[href*='Special:Random']"));
            string initialUrl = driver.Url;
            randomLink.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => !d.Url.Equals(initialUrl));
            Assert.That(driver.Url, Does.Contain("/wiki/"));
        }
    }
}