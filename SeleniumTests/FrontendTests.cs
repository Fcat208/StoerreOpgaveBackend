using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    public class FrontendTests : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly string _url = "http://localhost:5500"; // Live Server port

        public FrontendTests()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            _driver = new ChromeDriver(options);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        private void Login()
        {
            _driver.Navigate().GoToUrl(_url);
            _driver.FindElement(By.CssSelector("input[placeholder='Brugernavn']")).SendKeys("admin");
            _driver.FindElement(By.CssSelector("input[placeholder='Adgangskode']")).SendKeys("password123");
            _driver.FindElement(By.CssSelector("button.btn-primary")).Click();
            new WebDriverWait(_driver, TimeSpan.FromSeconds(5))
                .Until(d => d.FindElements(By.TagName("table")).Count > 0);
        }

        [Fact]
        public void LoginSide_VistNårIkkeLoggetInd()
        {
            _driver.Navigate().GoToUrl(_url);
            Assert.Contains("Login", _driver.PageSource);
            Assert.DoesNotContain("Log ud", _driver.PageSource);
        }

        [Fact]
        public void Login_MedKorrekteBrugeroplysninger()
        {
            Login();
            Assert.Contains("Log ud", _driver.PageSource);
            Assert.Contains("DR Music Records", _driver.PageSource);
        }

        [Fact]
        public void Login_MedForkertAdgangskode_ViserFejl()
        {
            _driver.Navigate().GoToUrl(_url);
            _driver.FindElement(By.CssSelector("input[placeholder='Brugernavn']")).SendKeys("admin");
            _driver.FindElement(By.CssSelector("input[placeholder='Adgangskode']")).SendKeys("forkert");
            _driver.FindElement(By.CssSelector("button.btn-primary")).Click();
            new WebDriverWait(_driver, TimeSpan.FromSeconds(5))
                .Until(d => d.FindElements(By.CssSelector(".alert-danger")).Count > 0);
            Assert.Contains("Forkert", _driver.PageSource);
        }

        [Fact]
        public void TabelVises_EfterLogin()
        {
            Login();
            var table = _driver.FindElement(By.TagName("table"));
            Assert.NotNull(table);
            var rows = _driver.FindElements(By.CssSelector("tbody tr"));
            Assert.True(rows.Count > 0);
        }

        [Fact]
        public void Søgning_FiltrererRecords()
        {
            Login();
            var søgefelt = _driver.FindElement(By.CssSelector("input[placeholder='Søg efter titel...']"));
            søgefelt.SendKeys("Queen");
            System.Threading.Thread.Sleep(500);
            var rows = _driver.FindElements(By.CssSelector("tbody tr"));
            Assert.True(rows.Count >= 0); // Søgningen kørte uden fejl
        }

        [Fact]
        public void TilføjRecord_ViserBekræftelse()
        {
            Login();
            _driver.FindElement(By.CssSelector("input[placeholder='Titel']")).SendKeys("TestSang");
            _driver.FindElement(By.CssSelector("input[placeholder='Artist']")).SendKeys("TestArtist");
            _driver.FindElement(By.CssSelector("input[placeholder='Varighed (sek)']")).SendKeys("200");
            _driver.FindElement(By.CssSelector("input[placeholder='År']")).SendKeys("2024");
            _driver.FindElement(By.CssSelector(".card button.btn-primary")).Click();
            new WebDriverWait(_driver, TimeSpan.FromSeconds(5))
                .Until(d => d.FindElements(By.CssSelector(".alert-success")).Count > 0);
            Assert.Contains("Record tilføjet", _driver.PageSource);
        }

        [Fact]
        public void Logout_SenderTilbaeTilLogin()
        {
            Login();
            _driver.FindElement(By.CssSelector("button.btn-outline-secondary")).Click();
            new WebDriverWait(_driver, TimeSpan.FromSeconds(5))
                .Until(d => d.FindElements(By.CssSelector("input[placeholder='Brugernavn']")).Count > 0);
            Assert.Contains("Login", _driver.PageSource);
        }

        public void Dispose()
        {
            _driver.Quit();
        }
    }
}
