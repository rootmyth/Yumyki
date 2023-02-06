using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yumyki.Interfaces;
using Yumyki.Models;

namespace Yumyki.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MealPlanRecipeController : ControllerBase
    {
        private readonly IMealPlanRecipeRepository _mealPlanRecipeRepo;
        public MealPlanRecipeController(IMealPlanRecipeRepository mealPlanRecipeRepository)
        {
            _mealPlanRecipeRepo = mealPlanRecipeRepository;
        }

        //Post
        [HttpPost("Add/{mealPlanId}/{recipeId}")]
        public void PostMealPlanRecipe(int mealPlanId, int recipeId)
        {
            _mealPlanRecipeRepo.PostRecipeToMealPlan(mealPlanId, recipeId);
        }

        [HttpDelete("Remove/{id}")]
        //Delete
        public void DeleteMealPlanRecipe(int id)
        {
            _mealPlanRecipeRepo.DeleteMealPlanRecipe(id);
        }

        [HttpPut("{id}")]
        //Complete
        public void CompleteMealPlanRecipe(int id)
        {
            _mealPlanRecipeRepo.CompleteMealPlanRecipe(id);
        }
    }
}
