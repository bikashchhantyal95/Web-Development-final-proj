using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TheBlogEngine.API.Controllers;
using TheBlogEngine.API.Data;
using TheBlogEngine.Models;
using TheBlogEngine.Shared;

namespace TheBlogEngine.UnitTests;

[TestClass]
public class APIUnitTests
{
    private SqliteConnection _connection;
    private DbContextOptions<BlogDbContext> _options;
    
    [TestInitialize]
    public void TestInitialize()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        _options = new DbContextOptionsBuilder<BlogDbContext>()
            .UseSqlite(_connection)
            .Options;

        using (var dbContext = new BlogDbContext(_options))
        {
            dbContext.Database.EnsureCreated();
        }
    }
    
    [TestCleanup]
    public void TestCleanup()
    {
        _connection.Close();
    }

    [TestMethod]
    public async Task GetAllBlogPosts_ShouldReturnAllPosts()
    {
        using (var dbContext = new BlogDbContext(_options))
        {
            var testPosts = GetTestBlogPosts();
            dbContext.BlogList.AddRange(testPosts);
            dbContext.SaveChanges();

            var controller = new BlogPostController(dbContext);

            var result = await controller.GetBlogList();
            var okResult = result.Result as OkObjectResult;
            var blogPosts = okResult.Value as List<Blog>;

            Assert.IsNotNull(okResult);
            Assert.IsNotNull(blogPosts);
            Assert.AreEqual(testPosts.Count, blogPosts.Count);
        }
    }
    
    private List<Blog> GetTestBlogPosts()
    {
        return new List<Blog>
        {
            new Blog { Id = 1, Title = "Post 1", Content = "Content 1" },
            new Blog { Id = 2, Title = "Post 2", Content = "Content 2" },
            new Blog { Id = 3, Title = "Post 3", Content = "Content 3" }
        };
    }
}