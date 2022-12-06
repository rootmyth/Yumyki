namespace Yumyki.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? RecipeName { get; set; }
        public string? RecipeType { get; set; }
        public DateTime? DateAdded { get; set; }
        public List<RecipeIngredient>? RecipeIngredientList { get; set; }
        public List<InstructionStep>? InstructionStepList { get; set; }
    }
}
