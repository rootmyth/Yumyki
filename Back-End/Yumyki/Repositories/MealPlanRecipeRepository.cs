using System.Data.SqlClient;
using Yumyki.Interfaces;
using Yumyki.Models;

namespace Yumyki.Repositories
{
    public class MealPlanRecipeRepository : BaseRepository, IMealPlanRecipeRepository
    {
        public MealPlanRecipeRepository(IConfiguration configuration) : base(configuration) { }
        public List<MealPlanRecipe> GetMealPlanRecipes(int mealPlanId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                SELECT Id, MealPlanId, RecipeId, IsComplete
                                From MealPlanRecipe
                                WHERE MealPlanId = @MealPlanId
                            ";
                    cmd.Parameters.AddWithValue("@MealPlanId", mealPlanId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<MealPlanRecipe> mealPlanRecipes = new();
                        while (reader.Read())
                        {
                            MealPlanRecipe mealPlanRecipe = new()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                MealPlanId = reader.GetInt32(reader.GetOrdinal("RecipeId")),
                                RecipeId = reader.GetInt32(reader.GetOrdinal("RecipeId")),
                                isComplete = reader.GetBoolean(reader.GetOrdinal("IsComplete"))

                            };
                            mealPlanRecipes.Add(mealPlanRecipe);
                        }
                        return mealPlanRecipes;
                    }
                }
            }
        }

        public void PostRecipeToMealPlan(int recipeId, int mealPlanId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO MealPlanRecipe (MealPlanId, RecipeId, IsComplete) 
                        VALUES (@MealPlanId, @RecipeId, 'false')
                    ";
                    cmd.Parameters.AddWithValue("@MealPlanId", mealPlanId);
                    cmd.Parameters.AddWithValue("@RecipeId", recipeId);

                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void DeleteMealPlanRecipe(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        DELETE FROM MealPlanRecipe
                        WHERE Id = @Id
                    ";
                    cmd.Parameters.AddWithValue("@Id", id);

                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void CompleteMealPlanRecipe(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE MealPlanRecipe
                        SET IsComplete = 'true'
                        WHERE Id = @Id
                    ";
                    cmd.Parameters.AddWithValue("@Id", id);

                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
    }
}
