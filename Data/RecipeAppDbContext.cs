using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecipeSiteAspNet.Areas.Identity.Data;
using RecipeSiteAspNet.Models;

namespace RecipeSiteAspNet.Data;

public class RecipeAppDbContext : IdentityDbContext<RecipeSiteUser>
{
    public DbSet<Recipe> Recipes { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<ReciepeStep> ReciepeSteps { get; set; } = null!;
    public DbSet<Img> Images { get; set; } = null!;

    public RecipeAppDbContext(DbContextOptions<RecipeAppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Recipe>().ToTable("Recipes");
        builder.Entity<Category>().ToTable("Categories");
        builder.Entity<ReciepeStep>().ToTable("ReciepeSteps");
        builder.Entity<Img>().ToTable("Images");
        base.OnModelCreating(builder);
    }
}
