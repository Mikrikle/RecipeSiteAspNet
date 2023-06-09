﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RecipeSiteAspNet.Areas.Identity.Data;

namespace RecipeSiteAspNet.Models
{
    public class Recipe
    {
        public int RecipeID { get; set; }

        [BindNever]
        public DateTime CreatedDate { get; set; }

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Ingredients { get; set; } = null!;
        public int CookingTimeMinutes { get; set; }
        public int nServings { get; set; }

        public List<ReciepeStep> reciepeSteps { get; set; } = new();


        public string RecipeSiteUserID { get; set; } = null!;
        [BindNever]
        [ForeignKey("RecipeSiteUserID")]
        public RecipeSiteUser? Author { get; set; }

        public int CategoryID { get; set; }
        [BindNever]
        public Category? Category { get; set; }

        [BindNever]
        public int? ImgID { get; set; }
        [BindNever]
        public virtual Img? Img { get; set; }
    }
}
