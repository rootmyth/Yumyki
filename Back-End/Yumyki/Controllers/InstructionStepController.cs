using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yumyki.Interfaces;
using Yumyki.Models;

namespace Yumyki.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InstructionStepController : ControllerBase
    {
        private readonly IInstructionStepRepository _InstructionStepRepo;
        public InstructionStepController(IInstructionStepRepository instructionStepRepository)
        {
            _InstructionStepRepo = instructionStepRepository;
        }

        [HttpGet("{recipeId}")]
        public List<InstructionStep> GetRecipeInstructions(int recipeId)
        {
            return _InstructionStepRepo.GetRecipeInstructions(recipeId);
        }

        [HttpPut("Update")]
        public void UpdateRecipeInstructions(List<InstructionStep> steps)
        {
            _InstructionStepRepo.UpdateRecipeInstructions(steps);
        }
    }
}
