using Microsoft.AspNetCore.Mvc;
using RecipeSiteAspNet.Models;
using System.Diagnostics;
using RecipeSiteAspNet.Data;
using Microsoft.EntityFrameworkCore;

namespace RecipeSiteAspNet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RecipeAppDbContext _db;
        private readonly IWebHostEnvironment _appEnvironment;

        public HomeController(ILogger<HomeController> logger, RecipeAppDbContext contex, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _db = contex;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllRecipes()
        {
            return View(_db.Recipes.Include(r => r.Img).Include(r => r.Category).ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}