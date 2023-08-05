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

    [TestMethod]
    public void ReadMoreLink_ShouldNavigateToBlogDetailsPage()
    {
        // Arrange
        _webDriver?.Navigate().GoToUrl(BaseUrl);

        // Act
        var readMoreLink = _webDriver?.FindElement(By.ClassName("card-link"));
        readMoreLink?.Click();

        // Assert
        Assert.IsTrue(_webDriver?.Url.Contains("/Home/BlogDetails/"));
    }

    [TestMethod]
    public void HomePage_ShouldContainBlogCards()
    {
        // Arrange
        _webDriver?.Navigate().GoToUrl(BaseUrl);

        // Act
        var blogCards = _webDriver?.FindElements(By.ClassName("blog-card"));

        // Assert
        Assert.IsTrue(blogCards is { Count: > 0 });
    }

    [TestCleanup]
    public void TearDown()
    {
        //tear down
        _webDriver?.Quit();
    }
}
