using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TheBlogEngine.Models;

namespace TheBlogEngine.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var post = new BlogPost()
        {
            Author = "Joh Doe",
            Content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ad eum dolorum architecto obcaecati enim dicta praesentium, quam nobis! Neque ad aliquam facilis numquam. Veritatis, sit.",
            PublishedDate = DateTime.Now

        };
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult CreateBlog()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

