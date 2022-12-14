namespace Yumyki.Models
{
    public class MealPlanRecipe
    {
        public int Id { get; set; }
        public int MealPlanId { get; set; }
        public int RecipeId { get; set; }
        public bool isComplete { get; set; }
    }
}
