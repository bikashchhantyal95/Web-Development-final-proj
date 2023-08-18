using AutoFixture;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using TheBlogEngine.API.Controllers;
using TheBlogEngine.API.Data;
using TheBlogEngine.Models;
using TheBlogEngine.Shared;

namespace TheBlogEngine.UnitTests;



    [TestClass]
    public class APIUnitTests
    {
        private Mock<IBlogRepository> _blogRepo;
        private Fixture _fixture;
        private BlogPostController _controller;

        public APIUnitTests()
        {
            _fixture = new Fixture();
            _blogRepo = new Mock<IBlogRepository>();
        }

        
        [TestMethod]
        public async Task GetBlogList_Returns_NotFoundWhenNoBlogsExist()
        {
            //Arrange
            _blogRepo.Setup(repo => repo.GetBlogs()).ReturnsAsync(new List<Blog>());
            
            _controller = new BlogPostController(_blogRepo.Object);

            //Action
            var actionResult = await _controller.GetBlogList();

            //Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetBlogList_Throws_Exception()
        {
            //Arrange
            _blogRepo.Setup((repo) => repo.GetBlogs()).Throws(new Exception());

            _controller = new BlogPostController(_blogRepo.Object);

            var result = await _controller.GetBlogList();

            var obj = result.Result as OkObjectResult;
            
            Assert.AreEqual(400, obj?.StatusCode);
            
        }

        [TestMethod]
        public async Task GetBlogList_Returns_OkWithCorrectNumberOfBlogs()
        {
            //Arrange
            var blogList = _fixture.CreateMany<Blog>(3).ToList();
            _blogRepo.Setup(repo => repo.GetBlogs()).ReturnsAsync(blogList);
            
            _controller = new BlogPostController(_blogRepo.Object);

            //Action
            var actionResult = await _controller.GetBlogList();

            var result = actionResult.Result as OkObjectResult;
            var returnBlogs = result?.Value as IEnumerable<Blog>;
            Assert.IsNotNull(returnBlogs);
            Assert.AreEqual(blogList.Count, returnBlogs.Count());
        }
        
        //GetBlog Test cases
        [TestMethod]
        public async Task GetBlog_Returns_NotFound_WhenBlogDoesNotExist()
        {
            //Arrange
            var nonExisttentId = 99;
            _blogRepo.Setup(repo => repo.GetBlogById(nonExisttentId)).ReturnsAsync((Blog)null);

            _controller = new BlogPostController(_blogRepo.Object);
            
            //Act
            var actionResult = await _controller.GetBlog(nonExisttentId);
            
            //Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetBlog_Returns_OkWithCorrectBlog()
        {
            //Arrange
            var blogId = 1;
            var blog = new Blog { Id = blogId, Title = "Test Blog", Content = "Test Content" };
            _blogRepo.Setup(repo => repo.GetBlogById(blogId)).ReturnsAsync(blog);

            _controller = new BlogPostController(_blogRepo.Object);
            
            //Act
            var actionResult = await _controller.GetBlog(blogId);
            
            //Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));
            var result = actionResult.Result as OkObjectResult;
            var returnedBlog = result?.Value as Blog;
            Assert.IsNotNull(returnedBlog);
            Assert.AreEqual(blog.Id, returnedBlog.Id);
            Assert.AreEqual(blog.Title, returnedBlog.Title);
            Assert.AreEqual(blog.Content, returnedBlog.Content);
        }
        
        
    }
