using System.Data.SqlClient;
using Yumyki.Interfaces;
using Yumyki.Models;

namespace Yumyki.Repositories
{
    public class IngredientRepository : BaseRepository, IIngredientRepository
    {
        public IngredientRepository(IConfiguration configuration) : base(configuration) { }

        public List<Ingredient> GetAllIngredients()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                SELECT * FROM Ingredient
                            ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Ingredient> ingredients = new();
                        while (reader.Read())
                        {
                            Ingredient ingredient = new()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                IngredientName = reader.GetString(reader.GetOrdinal("IngredientName")),
                                IngredientType = reader.GetString(reader.GetOrdinal("IngredientType")),
                                
                            };
                            ingredients.Add(ingredient);
                        }
                        return ingredients;
                    }
                }
            }
        }
    }
}
