﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeSiteAspNet.Models
{
    public class ReciepeStep
    {
        public int ReciepeStepID { get; set; }

        public string Description { get; set; }

        [BindNever]
        public int RecipeID { get; set; }
        [BindNever]
        public virtual Recipe Recipe { get; set; }

        [BindNever]
        public int? ImgID { get; set; }
        [BindNever]
        public virtual Img? Img { get; set; }
    }
}