using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeSiteAspNet.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}
