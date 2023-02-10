using System.Data.SqlClient;
using Yumyki.Interfaces;
using Yumyki.Models;

namespace Yumyki.Repositories
{
    public class LibraryRepository : BaseRepository, ILibraryRepository
    {
        public LibraryRepository(IConfiguration configuration) : base(configuration) { }

        public List<Recipe> GetLibraryRecipes(int userId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT r.Id, r.UserId, u.UserName, RecipeName, RecipeType, RecipeImageURL, CookTime, Servings, DateAdded
                        From Recipe r
                        JOIN LibraryRecipe lr ON lr.RecipeId = r.Id
                        JOIN [User] u ON r.UserId = u.Id
                        WHERE lr.UserId = @UserId
                    ";
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Recipe> libraryRecipes = new();
                        while (reader.Read())
                        {
                            Recipe recipe = new()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                                CreatorName = reader.GetString(reader.GetOrdinal("Username")),
                                RecipeName = reader.GetString(reader.GetOrdinal("RecipeName")),
                                RecipeType = reader.GetString(reader.GetOrdinal("RecipeType")),
                                RecipeImageURL = reader.IsDBNull(reader.GetOrdinal("RecipeImageURL")) ? "No Image URL" : reader.GetString(reader.GetOrdinal("RecipeImageURL")),
                                CookTime = reader.GetInt32(reader.GetOrdinal("CookTime")),
                                Servings = reader.GetInt32(reader.GetOrdinal("Servings")),
                                DateAdded = reader.GetDateTime(reader.GetOrdinal("DateAdded"))

                            };
                            libraryRecipes.Add(recipe);
                        }
                        return libraryRecipes;
                    }
                }
            }
        }

        public void PostRecipeToLibrary(int userId, int recipeId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO LibraryRecipe (UserId, RecipeId) 
                        VALUES (@UserId, @RecipeId)
                    ";
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@RecipeId", recipeId);

                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void DeleteRecipeFromLibrary(int recipeId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        DELETE FROM LibraryRecipe
                        WHERE RecipeId = @RecipeId
                    ";
                    cmd.Parameters.AddWithValue("@RecipeId", recipeId);

                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
    }
}
