using System.ComponentModel.DataAnnotations;

namespace Yumyki.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public string? CreatorName { get; set; }
        public string? RecipeName { get; set; }
        public string? RecipeType { get; set; }
        public string? RecipeImageURL { get; set; }
        public int CookTime { get; set; }
        public int Servings { get; set; }
        public DateTime? DateAdded { get; set; }
        public List<RecipeIngredient>? RecipeIngredientList { get; set; }
        public List<InstructionStep>? InstructionStepList { get; set; }
    }
}
