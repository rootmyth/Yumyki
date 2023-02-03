using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yumyki.Interfaces;
using Yumyki.Models;

namespace Yumyki.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientRepository _ingredientRepo;
        public IngredientController(IIngredientRepository ingredientRepository)
        {
            _ingredientRepo = ingredientRepository;
        }

        [HttpGet("All")]
        public List<Ingredient> GetAllIngredients()
        {
            return _ingredientRepo.GetAllIngredients();
        }
    }
}
