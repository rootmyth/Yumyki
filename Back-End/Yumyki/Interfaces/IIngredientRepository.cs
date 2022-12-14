using Yumyki.Models;

namespace Yumyki.Interfaces
{
    public interface IIngredientRepository
    {
        List<Ingredient> GetAllIngredients();
    }
}
