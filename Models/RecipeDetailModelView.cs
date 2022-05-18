namespace RecipeSiteAspNet.Models
{
    public class RecipeDetailModelView
    {
        public Recipe recipe { get; set; } = new Recipe();
        public List<ReciepeStep> reciepeSteps { get; set; } = new();
    }
}
