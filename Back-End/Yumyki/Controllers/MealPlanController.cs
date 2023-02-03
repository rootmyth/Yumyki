using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yumyki.Interfaces;
using Yumyki.Models;

namespace Yumyki.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MealPlanController : ControllerBase
    {
        private readonly IMealPlanRepository _mealPlanRepo;
        public MealPlanController(IMealPlanRepository mealPlanRepository)
        {
            _mealPlanRepo = mealPlanRepository;
        }

        //Current Plan
        [HttpGet("CurrentPlan/{userId}")]
        public IActionResult GetCurrentMealPlan(int userId)
        {
            MealPlan mealPlan = _mealPlanRepo.GetCurrentMealPlan(userId);
            if (mealPlan == null)
            {
                return NotFound();
            }
            List<MealPlanRecipe> mealPlanRecipeList = _mealPlanRepo.GetMealPlanRecipes(mealPlan.Id);
            mealPlan.MealPlanRecipeList = new();
            foreach (MealPlanRecipe mealPlanRecipe in mealPlanRecipeList)
            {
                mealPlan.MealPlanRecipeList.Add(mealPlanRecipe);
            }
            return Ok(mealPlan);
        }

        //Plan History
        [HttpGet("History/{userId}")]
        public List<MealPlan> GetMealPlanHistory(int userId)
        {
            return _mealPlanRepo.GetMealPlanHistory(userId);
        }

        //Confirm
        [HttpPut("Confirm/{id}")]
        public void ConfirmMealPlan(int id)
        {
            _mealPlanRepo.ConfirmMealPlan(id);
        }

        //Complete
        [HttpPut("Complete/{id}")]
        public void CompleteMealPlan(int id)
        {
            _mealPlanRepo.CompleteMealPlan(id);
        }

        //Post
        [HttpPost("Create/{userId}")]
        public void PostMealPlan(int userId)
        {
            _mealPlanRepo.PostMealPlan(userId);
        }
    }
}
