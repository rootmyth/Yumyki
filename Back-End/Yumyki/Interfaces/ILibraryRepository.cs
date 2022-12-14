using Yumyki.Models;

namespace Yumyki.Interfaces
{
    public interface ILibraryRepository
    {
        List<Recipe> GetLibraryRecipes(int userId);
        void PostRecipeToLibrary(int userId, int recipeId);
        void DeleteRecipeFromLibrary(int Id);
    }
}
