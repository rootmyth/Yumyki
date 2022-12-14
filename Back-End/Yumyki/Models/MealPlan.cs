namespace Yumyki.Models
{
    public class MealPlan
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsComplete { get; set; }
        public List<MealPlanRecipe>? MealPlanRecipeList { get; set; }
        
    }
}
