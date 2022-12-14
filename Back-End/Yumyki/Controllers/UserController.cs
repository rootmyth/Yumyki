using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yumyki.Interfaces;
using Yumyki.Models;

namespace Yumyki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        public UserController(IUserRepository usereRepository)
        {
            _userRepo = usereRepository;
        }

        [HttpGet]
        public IActionResult GetUser(string firebaseId)
        {
            User user = _userRepo.GetUser(firebaseId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("DoesUserExist")]
        public IActionResult CheckIfUserExists(string firebaseId)
        {
            User user = _userRepo.GetUser(firebaseId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPost]
        public void PostUser(User user)
        {
            _userRepo.PostUser(user);
        }
    }
}
