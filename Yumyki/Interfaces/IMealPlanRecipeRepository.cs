using Yumyki.Models;

namespace Yumyki.Interfaces
{
    public interface IMealPlanRecipeRepository
    {
        List<MealPlanRecipe> GetMealPlanRecipes(int mealPlanId);

        void PostRecipeToMealPlan(int recipeId, int mealPlanId);

        void DeleteMealPlanRecipe(int id);

        void CompleteMealPlanRecipe(int id);
    }
}
