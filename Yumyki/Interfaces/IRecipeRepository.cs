using Yumyki.Models;

namespace Yumyki.Interfaces
{
    public interface IRecipeRepository
    {
        List<Recipe> GetAllRecipes();
        List<RecipeIngredient> GetRecipeIngredients(int recipeId);
        List<InstructionStep> GetRecipeInstructions(int recipeId);
        void InsertRecipeTableValues(Recipe recipe);
        void InsertIngredientTableValues(Recipe recipe);
        void InsertRecipeIngredientTableValues(Recipe recipe);
        void InsertInstructionStepTableValues(Recipe recipe);
        void DeleteRecipe(int recipeId);
    }
}
