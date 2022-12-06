namespace Yumyki.Models
{
    public class MealPlan
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool isConfirmed { get; set; }
        public bool isComplete { get; set; }
        public List<MealPlanRecipe>? MealPlanRecipeList { get; set; }
        
    }
}
