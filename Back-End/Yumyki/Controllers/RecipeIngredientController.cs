using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yumyki.Interfaces;
using Yumyki.Models;

namespace Yumyki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeIngredientController : ControllerBase
    {
        private readonly IRecipeIngredientRepository _recipeIngredientRepo;
        public RecipeIngredientController(IRecipeIngredientRepository recipeRepository)
        {
            _recipeIngredientRepo = recipeRepository;
        }

        [HttpGet]
        public List<RecipeIngredient> GetRecipeIngredients(int recipeId)
        {
            return _recipeIngredientRepo.GetRecipeIngredients(recipeId);
        }

        [HttpPut]
        public void UpdateRecipeIngredients(List<RecipeIngredient> recipeIngredientList)
        {
            _recipeIngredientRepo.InsertIngredientTableValues(recipeIngredientList);
            _recipeIngredientRepo.UpdateRecipeIngredients(recipeIngredientList);
        }
    }
}
