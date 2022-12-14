using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yumyki.Interfaces;
using Yumyki.Models;

namespace Yumyki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealPlanRecipeController : ControllerBase
    {
        private readonly IMealPlanRecipeRepository _mealPlanRecipeRepo;
        public MealPlanRecipeController(IMealPlanRecipeRepository mealPlanRecipeRepository)
        {
            _mealPlanRecipeRepo = mealPlanRecipeRepository;
        }
        //Get
        [HttpGet]
        public List<MealPlanRecipe> GetMealPlanRecipes(int mealPlanId)
        {
            return _mealPlanRecipeRepo.GetMealPlanRecipes(mealPlanId);
        }

        //Post
        public void PostMealPlanRecipe(int recipeId, int mealPlanId)
        {
            _mealPlanRecipeRepo.PostRecipeToMealPlan(recipeId, mealPlanId);
        }

        //Delete
        public void DeleteMealPlanRecipe(int id)
        {
            _mealPlanRecipeRepo.DeleteMealPlanRecipe(id);
        }


        //Complete
        public void CompleteMealPlanRecipe(int id)
        {
            _mealPlanRecipeRepo.CompleteMealPlanRecipe(id);
        }
    }
}
