using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TheBlogEngine.Models;
using TheBlogEngine.Shared;

namespace TheBlogEngine.Controllers;

public class HomeController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpClient _client;

    public HomeController(IHttpClientFactory httpClientFactory, ILogger<HomeController> logger)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _client = httpClientFactory.CreateClient("MyApiClient");
    }

    private readonly ILogger<HomeController> _logger;

    // public HomeController(ILogger<HomeController> logger)
    // {
    //     _logger = logger;
    // }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var response = await _client.GetAsync("api/BlogPost/GetBlogList");
        var blogList = await response.Content.ReadFromJsonAsync<List<Blog>>();
        return View(blogList);
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
    [HttpPost]
    public async Task<IActionResult> CreateBlog(Blog newBlog)
    {
        newBlog.PublishedDate = DateTime.Now;

        if (string.IsNullOrEmpty(newBlog.Title) || string.IsNullOrEmpty(newBlog.Content))
        {
            ModelState.AddModelError("", "Title and Content are required.");
            return View(newBlog); // Return to the form with validation errors if necessary.
        }

        var json = JsonConvert.SerializeObject(newBlog);

        var content = new StringContent(json, Encoding.UTF8, "application/json");
    
        var response = await _client.PostAsync("api/BlogPost", content);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            var createdBlog = JsonConvert.DeserializeObject<Blog>(result);

            // Redirect back to the home page (assuming you have an 'Index' action in the 'HomeController').
            return RedirectToAction("Index");
        }
        else
        {
            var result = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", "An error occurred while creating the blog.");
            return View(newBlog);
        }
    }

    [HttpGet]
    public async Task<IActionResult> EditBlog(int id)
    {
        var response = await _client.GetAsync($"api/BlogPost/{id}");

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            var blogToEdit = JsonConvert.DeserializeObject<Blog>(result);
            return View(blogToEdit);
        }
        else
        {
            return NotFound();
        }
    }
    

    [HttpPost]
    public async Task<IActionResult> EditBlog(Blog editedBlog)
    {

        if (string.IsNullOrEmpty(editedBlog.Title) || string.IsNullOrEmpty(editedBlog.Content))
        {
            ModelState.AddModelError("", "Title and Content are required.");
            return View(editedBlog); // Return to the form with validation errors if necessary.
        }

        var json = JsonConvert.SerializeObject(editedBlog);

        var content = new StringContent(json, Encoding.UTF8, "application/json");
    
        var response = await _client.PutAsync($"api/BlogPost/{editedBlog.Id}", content);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        else
        {
            var result = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", "An error occurred while editing the blog.");
            return View(editedBlog);
        }
    }

    
    public async Task<IActionResult> BlogDetails(int id)
    {
        var response = await _client.GetAsync($"api/BlogPost/GetBlog/{id}");
        var blogDetails = await response.Content.ReadFromJsonAsync<Blog>();
       
        if (blogDetails == null)
        {
            return NotFound();
        }
        return View(blogDetails);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

