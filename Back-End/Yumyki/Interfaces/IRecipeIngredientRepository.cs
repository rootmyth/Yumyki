using Yumyki.Models;

namespace Yumyki.Interfaces
{
    public interface IRecipeIngredientRepository
    {
        List<RecipeIngredient> GetRecipeIngredients(int recipeId);
        List<RecipeIngredient> GetMealPlanIngredients(int mealPlanId);
        void InsertIngredientTableValues(List<RecipeIngredient> recipeIngredientList);
        void UpdateRecipeIngredients(List<RecipeIngredient> recipeIngredientList);
    }
}
