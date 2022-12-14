using System.Data.SqlClient;
using Yumyki.Interfaces;
using Yumyki.Models;

namespace Yumyki.Repositories
{
    public class RecipeRepository : BaseRepository, IRecipeRepository
    {
        public RecipeRepository(IConfiguration configuration) : base(configuration) { }
        public List<Recipe> GetAllRecipes()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                SELECT Id, [User].Username, RecipeName, RecipeType, RecipeImageURL, CookTime, Servings, DateAdded
                                FROM Recipe
                                JOIN [User] ON [User].Id = Recipe.UserId
                            ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Recipe> recipes = new();
                        while (reader.Read())
                        {
                            Recipe recipe = new Recipe()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                                CreatorName = reader.GetString(reader.GetOrdinal("[User].Username")),
                                RecipeName = reader.GetString(reader.GetOrdinal("RecipeName")),
                                RecipeType = reader.GetString(reader.GetOrdinal("RecipeType")),
                                RecipeImageURL = reader.IsDBNull(reader.GetOrdinal("RecipeImageURL")) ? "No Image URL" : reader.GetString(reader.GetOrdinal("RecipeImageURL")),
                                CookTime = reader.GetInt32(reader.GetOrdinal("CookTime")),
                                Servings = reader.GetInt32(reader.GetOrdinal("Servings")),
                                DateAdded = reader.GetDateTime(reader.GetOrdinal("DateAdded"))
                            };
                            recipes.Add(recipe);
                        }
                        return recipes;
                    }
                }
            }
        }
        public void InsertRecipeTableValues(Recipe recipe)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                INSERT INTO Recipe (UserId, RecipeName, RecipeType, RecipeImageURL, CookTime, Servings, DateAdded)
                                VALUES (@UserId, @RecipeName, @RecipeType, @RecipeImageURL, @CookTime, @Servings, GETDATE())
                            ";
                    cmd.Parameters.AddWithValue("@UserId", recipe.UserId);
                    cmd.Parameters.AddWithValue("@RecipeName", recipe.RecipeName);
                    cmd.Parameters.AddWithValue("@RecipeType", recipe.RecipeType);
                    cmd.Parameters.AddWithValue("@RecipeImageURL", recipe.RecipeImageURL);
                    cmd.Parameters.AddWithValue("@CookTime", recipe.CookTime);
                    cmd.Parameters.AddWithValue("@Servings", recipe.Servings);

                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void InsertIngredientTableValues(Recipe recipe)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    foreach (RecipeIngredient recipeIngredient in recipe.RecipeIngredientList)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = @"
                            IF NOT EXISTS (SELECT 1 FROM Ingredient WHERE IngredientName = @IngredientName)
                            BEGIN
                            INSERT INTO Ingredient (IngredientName, IngredientType)
                            VALUES (@IngredientName, @IngredientType)
                            END
                        ";
                        cmd.Parameters.AddWithValue("@IngredientName", recipeIngredient.Ingredient.IngredientName);
                        cmd.Parameters.AddWithValue("@IngredientType", recipeIngredient.Ingredient.IngredientType);

                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }

        public void InsertRecipeIngredientTableValues(Recipe recipe)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    foreach (RecipeIngredient recipeIngredient in recipe.RecipeIngredientList)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = @"
                        INSERT INTO RecipeIngredient (RecipeId, IngredientId, Quantity, QuantityUnit, Note)
                        VALUES ((SELECT Id FROM Recipe WHERE RecipeName = @RecipeName), (SELECT Id FROM Ingredient WHERE IngredientName = @IngredientName), @Quantity, @QuantityUnit, @Note)
                        ";
                        cmd.Parameters.AddWithValue("@RecipeName", recipe.RecipeName);
                        cmd.Parameters.AddWithValue("@IngredientName", recipeIngredient.Ingredient.IngredientName);
                        cmd.Parameters.AddWithValue("@IngredientType", recipeIngredient.Ingredient.IngredientType);
                        cmd.Parameters.AddWithValue("@Quantity", recipeIngredient.Quantity);
                        cmd.Parameters.AddWithValue("@QuantityUnit", recipeIngredient.QuantityUnit);
                        cmd.Parameters.AddWithValue("@Note", recipeIngredient.Note);

                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }

        public void InsertInstructionStepTableValues(Recipe recipe)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    foreach (InstructionStep instructionStep in recipe.InstructionStepList)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = @"
                                    INSERT INTO InstructionStep (RecipeId, StepNumber, StepText)
                                    VALUES ((SELECT Id FROM Recipe WHERE RecipeName = @RecipeName), @StepNumber, @StepText)
                                ";
                        cmd.Parameters.AddWithValue("@RecipeName", recipe.RecipeName);
                        cmd.Parameters.AddWithValue("@StepNumber", instructionStep.StepNumber);
                        cmd.Parameters.AddWithValue("@StepText", instructionStep.StepText);

                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }

        public void DeleteRecipe(int recipeId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                DELETE FROM Recipe
                                WHERE Recipe.Id = @RecipeId

                                DELETE FROM InstructionStep
                                WHERE InstructionStep.RecipeId = @RecipeId

                                DELETE FROM RecipeIngredient
                                WHERE RecipeIngredient.RecipeId = @RecipeId
                            ";
                    cmd.Parameters.AddWithValue("@RecipeId", recipeId);

                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void UpdateRecipe(Recipe recipe)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                UPDATE Recipe
                                SET RecipeName = @RecipeName, RecipeImageURL = @RecipeImageURL, CookTime = @CookTime, Servings = @Servings
                                WHERE Recipe.Id = @RecipeId
                            ";
                    cmd.Parameters.AddWithValue("@RecipeName", recipe.RecipeName);
                    cmd.Parameters.AddWithValue("@RecipeImageURL", recipe.RecipeImageURL);
                    cmd.Parameters.AddWithValue("@CookTime", recipe.CookTime);
                    cmd.Parameters.AddWithValue("@Servings", recipe.Servings);
                    cmd.Parameters.AddWithValue("@RecipeId", recipe.Id);

                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
    }
}
