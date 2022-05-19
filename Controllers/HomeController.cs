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

        [HttpGet]
        public async Task<IActionResult> RecipeDetail(int? id)
        {
            if (id == null)
                return NotFound();
            // Получение рецепта и всях связных моделей
            Recipe? recipe = _db.Recipes.Include(r => r.Img).Include(r => r.Author)
                .Single(r => r.RecipeID == id);
            if (recipe == null)
                return NotFound();
            // получение шагов рцепты
            List<ReciepeStep> steps = await _db.ReciepeSteps.Include(s => s.Recipe)
                .Include(s => s.Img)
                .Where(s => s.RecipeID == id)
                .ToListAsync();
            // создание модели представления
            RecipeDetailModelView viewmodel = new RecipeDetailModelView
            {
                reciepeSteps = steps,
                recipe = recipe
            };
            ViewBag.Categories = await _db.Categories.ToListAsync();

            return View(viewmodel);
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