using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yumyki.Interfaces;
using Yumyki.Models;

namespace Yumyki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryRepository _libraryRepo;
        public LibraryController(ILibraryRepository libraryRepository)
        {
            _libraryRepo = libraryRepository;
        }

        public List<Recipe> GetLibraryRecipes(int userId)
        {
            return _libraryRepo.GetLibraryRecipes(userId);
        }
        public void PostRecipeToLibrary(int userId, int recipeId)
        {
            _libraryRepo.PostRecipeToLibrary(userId, recipeId);
        }
        public void DeleteRecipeFromLibrary(int Id)
        {
            _libraryRepo.DeleteRecipeFromLibrary(Id);
        }
    }
}
