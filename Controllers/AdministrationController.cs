using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using RecipeSiteAspNet.Areas.Identity.Data;
using RecipeSiteAspNet.Data;
using System.IO;


namespace RecipeSiteAspNet.Controllers
{
    public partial class AdministrationController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RecipeAppDbContext _db;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly UserManager<RecipeSiteUser> _userManager;

        public AdministrationController(ILogger<HomeController> logger, RecipeAppDbContext contex,
            IWebHostEnvironment appEnvironment, UserManager<RecipeSiteUser> userManager)
        {
            _logger = logger;
            _db = contex;
            _appEnvironment = appEnvironment;
            _userManager = userManager;
        }

        private string GetFilePath(IFormFile file, int recipeID, bool isStepImg = false)
        {
            string path = $"{_appEnvironment.WebRootPath}/Files/Recipe{recipeID}/";
            if (isStepImg)
                path += $"Steps/";
            path += file.FileName;
            return path;
        }
        
        private void DeleteRecipeStepFile(int recipeID, string filename)
        {
            string path = $"{_appEnvironment.WebRootPath}/Files/Recipe{recipeID}/Steps/{filename}";
            if(System.IO.File.Exists(path))
                System.IO.File.Delete(path);
        }

        private void DeleteRecipeFiles(int recipeID)
        {
            string path = $"{_appEnvironment.WebRootPath}/Files/Recipe{recipeID}/";
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }

        private void SaveFile(IFormFile file, string path)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            using (var output = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(output);
            }
        }
    }
}
