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
        public async Task<IActionResult> ManageCategories()
        {
            ViewBag.Categories = await _db.Categories.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
            }
            return RedirectToAction("ManageCategories", "Administration");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            Category? c = await _db.Categories.FindAsync(id);
            if (c == null)
                return NotFound();
            _db.Categories.Remove(c);
            await _db.SaveChangesAsync();
            return RedirectToAction("ManageCategories", "Administration");
        }
    }
}
