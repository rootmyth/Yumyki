using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yumyki.Interfaces;
using Yumyki.Models;

namespace Yumyki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructionStepController : ControllerBase
    {
        private readonly IInstructionStepRepository _InstructionStepRepo;
        public InstructionStepController(IInstructionStepRepository instructionStepRepository)
        {
            _InstructionStepRepo = instructionStepRepository;
        }

        [HttpGet("Instructions/{recipeId}")]
        public List<InstructionStep> GetRecipeInstructions(int recipeId)
        {
            return _InstructionStepRepo.GetRecipeInstructions(recipeId);
        }
    }
}
