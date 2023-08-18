using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TheBlogEngine.Models;
using TheBlogEngine.Shared;

namespace TheBlogEngine.Controllers;

public class HomeController : Controller
{

    //list of Blogpost
    List<BlogPost> blogs = new List<BlogPost> {
            new BlogPost{
                Id = 1,
                Author = "John Doe",
                Title = "Getting Started with Artificial Intelligence",
                Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                PublishedDate = DateTime.Now
            },
            new BlogPost
            {
                Id = 2,
                Author = "John Doe",
                Title = "My First Journal",
                Content = "In the vast expanse of the universe, space exploration has captivated the minds of humanity for centuries.",
                PublishedDate = DateTime.Now
            }
        };

    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View(blogs);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }
    
    public IActionResult CreateBlog()
    {
        return View();
    }

    public IActionResult EditBlog()
    {
        return View();
    }
    

    public IActionResult BlogDetails(int id)
    {
        var blogPost = blogs.FirstOrDefault(blog => blog.Id == id);
        if(blogPost == null)
        {
            return NotFound();
        }
        return View(blogPost);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

