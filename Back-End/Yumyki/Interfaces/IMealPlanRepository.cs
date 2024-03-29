﻿using Yumyki.Models;

namespace Yumyki.Interfaces
{
    public interface IMealPlanRepository
    {
        MealPlan GetCurrentMealPlan(int userId);
        List<MealPlanRecipe> GetMealPlanRecipes(int mealPlanId);
        List<MealPlan> GetMealPlanHistory(int userId);
        void ConfirmMealPlan(int Id);
        void CompleteMealPlan(int Id);
        void PostMealPlan(int userId);
    }
}
