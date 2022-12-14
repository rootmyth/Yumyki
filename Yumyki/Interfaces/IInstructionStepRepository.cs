using Yumyki.Models;

namespace Yumyki.Interfaces
{
    public interface IInstructionStepRepository
    {
        List<InstructionStep> GetRecipeInstructions(int recipeId);
        void UpdateRecipeInstructions(List<InstructionStep> instructionStepList);
    }
}
