using Yumyki.Models;

namespace Yumyki.Interfaces
{
    public interface IMealPlanRecipeRepository
    {
        void PostRecipeToMealPlan(int mealPlanId, int recipeId);
        void DeleteMealPlanRecipe(int id);
        void CompleteMealPlanRecipe(int id);
    }
}
