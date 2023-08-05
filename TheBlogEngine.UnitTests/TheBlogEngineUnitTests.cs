using TheBlogEngine.Shared;

namespace TheBlogEngine.UnitTests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
    }

    [TestMethod]
    public void Blog_ShouldCreate_With_Valid_properties()
    {
        
        var blog = new Blog {
            Id = 1,
            Title = "Sample Blog",
            Author = "John Doe",
            Content = "This is the sample blog content.",
            PublishedDate = new DateTime(2023, 8, 4)
        };

        Assert.AreEqual(1, blog.Id);
        Assert.AreEqual("Sample Blog", blog.Title);
        Assert.AreEqual("John Doe", blog.Author);
        Assert.AreEqual(new DateTime(2023, 8, 4), blog.PublishedDate);
        Assert.AreEqual("This is the sample blog content.", blog.Content);
    }
}
