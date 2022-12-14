using Microsoft.AspNetCore.Mvc;
using Yumyki.Interfaces;
using Yumyki.Models;

namespace Yumyki.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeRepository _recipeRepo;
        public RecipeController(IRecipeRepository recipeRepository)
        {
            _recipeRepo = recipeRepository;
        }

        [HttpGet]
        public List<Recipe> GetAllRecipes()
        {
            return _recipeRepo.GetAllRecipes();
        }

        [HttpPost]
        public void PostRecipe(Recipe recipe)
        {
            _recipeRepo.InsertRecipeTableValues(recipe);
            _recipeRepo.InsertIngredientTableValues(recipe);
            _recipeRepo.InsertRecipeIngredientTableValues(recipe);
            _recipeRepo.InsertInstructionStepTableValues(recipe);
        }

        [HttpDelete]
        public void DeleteRecipe(int recipeId)
        {
            _recipeRepo.DeleteRecipe(recipeId);
        }
    }
}