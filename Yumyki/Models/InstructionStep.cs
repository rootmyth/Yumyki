namespace Yumyki.Models
{
    public class InstructionStep
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int StepNumber { get; set; }
        public string? StepText { get; set; }
    }
}
