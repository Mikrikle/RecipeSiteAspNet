using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

using RecipeSiteAspNet.Models;

namespace RecipeSiteAspNet.Areas.Identity.Data;

// Add profile data for application users by adding properties to the SiteUser class
public class RecipeSiteUser : IdentityUser
{
    public virtual List<Recipe> Recipes { get; set; } = new List<Recipe>();
}

