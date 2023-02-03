using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yumyki.Interfaces;
using Yumyki.Models;

namespace Yumyki.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryRepository _libraryRepo;
        public LibraryController(ILibraryRepository libraryRepository)
        {
            _libraryRepo = libraryRepository;
        }

        [HttpGet("{userId}")]
        public List<Recipe> GetLibraryRecipes(int userId)
        {
            return _libraryRepo.GetLibraryRecipes(userId);
        }

        [HttpPost("Add/{userId}/{recipeId}")]
        public void PostRecipeToLibrary(int userId, int recipeId)
        {
            _libraryRepo.PostRecipeToLibrary(userId, recipeId);
        }

        [HttpDelete("Remove/{recipeId}")]
        public void DeleteRecipeFromLibrary(int recipeId)
        {
            _libraryRepo.DeleteRecipeFromLibrary(recipeId);
        }
    }
}
