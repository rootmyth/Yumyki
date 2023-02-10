namespace Yumyki.Models
{
    public class MealPlanRecipe
    {
        public int Id { get; set; }
        public int MealPlanId { get; set; }
        public int RecipeId { get; set; }
        public Recipe? Recipe { get; set; }
        public bool IsComplete { get; set; }
    }
}
