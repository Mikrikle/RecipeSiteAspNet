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
        public IActionResult GetStepPartialView()
        {
            return PartialView("_RecipeStepsForms", _db.ReciepeSteps.Include(s => s.Recipe).Include(s=>s.Img).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult UpdateRecipeStep(int ReciepeStepId, string Description)
        {
            ReciepeStep? rs = _db.ReciepeSteps.Find(ReciepeStepId);
            if (rs == null || Description.Length < 1)
                return NotFound();
            rs.Description = Description;
            //
            _db.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult DeleteRecipeStep(int ReciepeStepId)
        {
            ReciepeStep? rs = _db.ReciepeSteps.Include(s=>s.Recipe).Include(s=>s.Img).Single(s=>s.ReciepeStepID == ReciepeStepId);
            if (rs == null)
                return NotFound();
            // удаление изображения, если есть
            if(rs.Img != null)
                DeleteRecipeStepFile(rs.RecipeID, rs.Img.Path);
            _db.Remove(rs);
            //
            _db.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult CreateRecipeStep(int recipeId, IFormFile file, ReciepeStep rstep)
        {
            // получение записи о рецете
            Recipe? recipe = _db.Recipes.Find(recipeId);
            if (recipe == null || file==null || rstep.Description.Length < 1)
                return NotFound();
            // запись шага рецепта в базу данных
            _db.ReciepeSteps.Add(rstep);
            // добавление шага к рецепту
            recipe.reciepeSteps.Add(rstep);
            // сохранение изображения в wwwroot
            string path = GetFilePath(file, recipeId, true);
            SaveFile(file, path);
            // сохранение изображения в базу данных
            Img img = new Img { Path=path, Name=file.FileName };
            _db.Images.Add(img);
            // установка изображения рецепту
            rstep.Img = img;
            //
            _db.SaveChanges();
            return Ok();
            //return RedirectToAction("RecipeDetail", "Home", new { id = recipeId }, null);
        }
    }
}
