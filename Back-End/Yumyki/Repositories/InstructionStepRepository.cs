using System.Data.SqlClient;
using Yumyki.Interfaces;
using Yumyki.Models;

namespace Yumyki.Repositories
{
    public class InstructionStepRepository : BaseRepository, IInstructionStepRepository
    {
        public InstructionStepRepository(IConfiguration configuration) : base(configuration) { }

        public List<InstructionStep> GetRecipeInstructions(int recipeId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                SELECT * From InstructionStep
                                WHERE InstructionStep.RecipeId = @RecipeId
                            ";
                    cmd.Parameters.AddWithValue("@RecipeId", recipeId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<InstructionStep> instructionSteps = new();
                        while (reader.Read())
                        {
                            InstructionStep instructionStep = new InstructionStep()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                RecipeId = reader.GetInt32(reader.GetOrdinal("RecipeId")),
                                StepNumber = reader.GetInt32(reader.GetOrdinal("StepNumber")),
                                StepText = reader.GetString(reader.GetOrdinal("StepText"))

                            };
                            instructionSteps.Add(instructionStep);
                        }
                        return instructionSteps;
                    }
                }
            }
        }

        public void UpdateRecipeInstructions(List<InstructionStep> instructionStepList)
        {
            using (SqlConnection conn = Connection)
            {
                foreach (InstructionStep instructionStep in instructionStepList)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                    
                        cmd.CommandText = @"
                            UPDATE InstructionStep
                            SET StepText = @StepText
                            WHERE Id = @Id
                         ";
                        cmd.Parameters.AddWithValue("@StepText", instructionStep.StepText);
                        cmd.Parameters.AddWithValue("@Id", instructionStep.Id);

                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
        }
    }
}
