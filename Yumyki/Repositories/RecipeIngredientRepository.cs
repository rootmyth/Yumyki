using System.Data.SqlClient;
using Yumyki.Interfaces;
using Yumyki.Models;

namespace Yumyki.Repositories
{
    public class RecipeIngredientRepository : BaseRepository, IRecipeIngredientRepository
    {
        public RecipeIngredientRepository(IConfiguration configuration) : base(configuration) { }

        public List<RecipeIngredient> GetRecipeIngredients(int recipeId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                SELECT ri.Id AS Id, RecipeId, IngredientId, IngredientName, IngredientType, Quantity, QuantityUnit, Note
                                FROM RecipeIngredient ri
                                JOIN Ingredient AS i ON i.Id = ri.IngredientId
                                WHERE ri.RecipeId = @RecipeId
                            ";
                    cmd.Parameters.AddWithValue("@RecipeId", recipeId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<RecipeIngredient> recipeIngredients = new();
                        while (reader.Read())
                        {
                            RecipeIngredient recipeIngredient = new RecipeIngredient()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                RecipeId = reader.GetInt32(reader.GetOrdinal("RecipeId")),
                                IngredientId = reader.GetInt32(reader.GetOrdinal("IngredientId")),
                                Ingredient = new Ingredient()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("IngredientId")),
                                    IngredientName = reader.GetString(reader.GetOrdinal("IngredientName")),
                                    IngredientType = reader.GetString(reader.GetOrdinal("IngredientType"))
                                },
                                Quantity = reader.GetDecimal(reader.GetOrdinal("Quantity")),
                                QuantityUnit = reader.IsDBNull(reader.GetOrdinal("QuantityUnit")) ? null : reader.GetString(reader.GetOrdinal("QuantityUnit")),
                                Note = reader.IsDBNull(reader.GetOrdinal("Note")) ? "No Note" : reader.GetString(reader.GetOrdinal("Note"))

                            };
                            recipeIngredients.Add(recipeIngredient);
                        }
                        return recipeIngredients;
                    }
                }
            }
        }

        public void InsertIngredientTableValues(List<RecipeIngredient> recipeIngredientList)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    foreach (RecipeIngredient recipeIngredient in recipeIngredientList)
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

        public void UpdateRecipeIngredients(List<RecipeIngredient> recipeIngredientList)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    foreach (RecipeIngredient recipeIngredient in recipeIngredientList)
                    {
                        cmd.CommandText = @"
                            UPDATE RecipeIngredient
                            SET IngredientId = (SELECT Id FROM Ingredient WHERE IngredientName = @IngredientName), Quantity = @Quantity, QuantityUnit = @QuantityUnit, Note = @Note
                            WHERE Id = @Id
                         ";
                        cmd.Parameters.AddWithValue("@IngredientName", recipeIngredient.Ingredient.IngredientName);
                        cmd.Parameters.AddWithValue("@Quantity", recipeIngredient.Quantity);
                        cmd.Parameters.AddWithValue("@QuantityUnit", recipeIngredient.QuantityUnit);
                        cmd.Parameters.AddWithValue("@Note", recipeIngredient.Note);
                        cmd.Parameters.AddWithValue("@Id", recipeIngredient.Id);

                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }
    }
}
