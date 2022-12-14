using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yumyki.Interfaces;
using Yumyki.Models;

namespace Yumyki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealPlanController : ControllerBase
    {
        private readonly IMealPlanRepository _mealPlanRepo;
        public MealPlanController(IMealPlanRepository mealPlanRepository)
        {
            _mealPlanRepo = mealPlanRepository;
        }

        //Current Plan
        [HttpGet("CurrentPlan")]
        public MealPlan GetCurrentMealPlan(int userId)
        {
            return _mealPlanRepo.GetCurrentMealPlan(userId);
        }

        //Plan History
        [HttpGet("History")]
        public List<MealPlan> GetMealPlanHistory(int userId)
        {
            return _mealPlanRepo.GetMealPlanHistory(userId);
        }

        //Confirm
        [HttpPut("Confirm")]
        public void ConfirmMealPlan(int id)
        {
            _mealPlanRepo.ConfirmMealPlan(id);
        }

        //Complete
        [HttpPut("Complete")]
        public void CompleteMealPlan(int id)
        {
            _mealPlanRepo.CompleteMealPlan(id);
        }

        //Post
        [HttpPost]
        public void PostMealPlan(int userId)
        {
            _mealPlanRepo.PostMealPlan(userId);
        }
    }
}
