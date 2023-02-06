using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yumyki.Interfaces;
using Yumyki.Models;

namespace Yumyki.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        public UserController(IUserRepository usereRepository)
        {
            _userRepo = usereRepository;
        }

        [HttpGet("{firebaseId}")]
        public IActionResult GetUser(string firebaseId)
        {
            User user = _userRepo.GetUser(firebaseId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("DoesUserExist/{firebaseId}")]
        public bool CheckIfUserExists(string firebaseId)
        {
            bool userExists = _userRepo.CheckIfUserExists(firebaseId);
            if (!userExists)
            {
                return false;
            }
            return true;
        }

        [HttpPost]
        public User PostUser(User user)
        {
            _userRepo.PostUser(user);
            User createdUser = new();
            createdUser = _userRepo.GetUser(user.FirebaseId);
            return createdUser;
        }
    }
}
