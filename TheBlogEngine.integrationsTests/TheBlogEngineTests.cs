using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace TheBlogEngine.integrationsTests;

[TestClass]
public class TheBlogEngineTests
{
    private IWebDriver? _webDriver;
    private const string BaseUrl = "https://localhost:7231";

    [TestInitialize]
    public void SetUp()
    {
        //setup
        new DriverManager().SetUpDriver(new ChromeConfig());
        _webDriver = new ChromeDriver();
    }


    [TestMethod]
    public void TitleShouldContainHomeInIt()
    {
        _webDriver?.Navigate().GoToUrl(BaseUrl);
        Assert.IsTrue(_webDriver?.Title.Contains("Home"));
    }

    


    [TestCleanup]
    public void TearDown()
    {
        //tear down
        _webDriver?.Quit();
    }
}
