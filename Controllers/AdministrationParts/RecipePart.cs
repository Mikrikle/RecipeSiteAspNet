using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using RecipeSiteAspNet.Areas.Identity.Data;
using RecipeSiteAspNet.Data;
using RecipeSiteAspNet.Models;
using Microsoft.AspNetCore.Authorization;


namespace RecipeSiteAspNet.Controllers
{
    public partial class AdministrationController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> CreateRecipe()
        {
            ViewBag.Categories = await _db.Categories.ToListAsync();
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> RecipeRedact(int? id)
        {
            if (id == null)
                return NotFound();
            Recipe? recipe = _db.Recipes.Include(r => r.Img).Include(r => r.Author)
                .Include(r => r.reciepeSteps)
                .Single(r => r.RecipeID == id);
            if (recipe == null)
                return NotFound();
            if (recipe.RecipeSiteUserID != _userManager.GetUserId(HttpContext.User))
                return Forbid();

            ViewBag.Categories = await _db.Categories.ToListAsync();
            return View(recipe);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult UpdateRecipe(int id, Recipe recipe, IFormFile? file)
        {
            Recipe? r = _db.Recipes.Include(r => r.Img).Single(r => r.RecipeID == id);
            if (r == null)
                return NotFound();
            if (r.RecipeSiteUserID != _userManager.GetUserId(HttpContext.User))
                return Forbid();
            r.Name = recipe.Name;
            r.Description = recipe.Description;
            r.CategoryID = recipe.CategoryID;
            r.CookingTimeMinutes = recipe.CookingTimeMinutes;
            r.Ingredients = recipe.Ingredients;
            r.nServings = recipe.nServings;
            if (file != null)
                UpdateRecipeImage(file, r);
            _db.SaveChanges();
            return RedirectToAction("RecipeDetail", "Administration", new { id = id }, null);
        }

        private void UpdateRecipeImage(IFormFile file, Recipe recipe)
        {
            // сохранение нового изображения
            string path = GetFilePath(file, recipe.RecipeID, true);
            SaveFile(file, path);
            // удаление прошлого изобрадения
            Img? img = recipe.Img;
            if (img != null)
            {
                System.IO.File.Delete(_appEnvironment.WebRootPath + img.Path);
                // обновление баазы данных
                img.Path = path;
                recipe.Img = img;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult CreateRecipe(Recipe recipe, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                // создание рецепта
                recipe.RecipeSiteUserID = _userManager.GetUserId(HttpContext.User);
                recipe.CreatedDate = DateTime.Now;
                _db.Recipes.Add(recipe);
                _db.SaveChanges();
                // сохранение изображения в wwwroot
                string path = GetFilePath(file, recipe.RecipeID, false);
                SaveFile(file, path);
                Img img = new Img { Path = path, Name = file.Name };
                _db.Images.Add(img);
                // установка изображения
                recipe.Img = img;
                //
                _db.SaveChanges();
                return RedirectToAction("RecipeDetail", "Administration", new { id = recipe.RecipeID }, null);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult DeleteRecipe(int id)
        {
            Recipe? recipe = _db.Recipes.Find(id);
            if (recipe == null)
                return NotFound();
            if (recipe.RecipeSiteUserID != _userManager.GetUserId(HttpContext.User))
                return Forbid();
            // Удаление изображений из wwwroot
            DeleteRecipeFiles(recipe.RecipeID);
            // удалений шагов
            List<ReciepeStep> steps = _db.ReciepeSteps.Include(s => s.Recipe).Where(s => s.RecipeID == id).ToList();
            _db.RemoveRange(steps);

            // Удаление изображения из базы данных
            Img? img = _db.Images.Find(recipe.Img);
            if (img != null && recipe.ImgID != 1)
                _db.Images.Remove(img);
            // Удаление самого рецепта
            _db.Recipes.Remove(recipe);
            //
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
