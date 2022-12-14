using System.Data.SqlClient;
using Yumyki.Interfaces;
using Yumyki.Models;

namespace Yumyki.Repositories
{
    public class MealPlanRepository : BaseRepository, IMealPlanRepository
    {
        public MealPlanRepository(IConfiguration configuration) : base(configuration) { }
        public MealPlan GetCurrentMealPlan(int userId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, UserId, IsConfirmed, IsComplete
                        FROM MealPlan
                        WHERE UserId = @UserId AND IsComplete = FALSE
                    ";
                    cmd.Parameters.AddWithValue("@userId", userId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        MealPlan mealPlan = null;
                        while (reader.Read())
                        {
                            mealPlan = new()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                                IsConfirmed = reader.GetBoolean(reader.GetOrdinal("IsConfirmed")),
                                IsComplete = reader.GetBoolean(reader.GetOrdinal("IsComplete"))


                            };
                        }
                        return mealPlan;
                    }
                }
            }
        }

        public List<MealPlan> GetMealPlanHistory(int userId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT 10 Id, UserId, IsConfirmed, IsComplete
                        FROM MealPlan
                        WHERE UserId = @UserId AND IsComplete = TRUE
                    ";
                    cmd.Parameters.AddWithValue("@userId", userId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<MealPlan> mealPlanHistory = new();
                        while (reader.Read())
                        {
                            MealPlan mealPlan = new()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                                IsConfirmed = reader.GetBoolean(reader.GetOrdinal("IsConfirmed")),
                                IsComplete = reader.GetBoolean(reader.GetOrdinal("IsComplete"))


                            };
                            mealPlanHistory.Add(mealPlan);
                        }
                        return mealPlanHistory;
                    }
                }
            }
        }

        public void ConfirmMealPlan(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE MealPlan
                        SET IsConfirmed = TRUE
                        Where Id = @Id
                    ";
                    cmd.Parameters.AddWithValue("@Id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CompleteMealPlan(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    UPDATE MealPlan
                    SET IsCompleted = TRUE
                    Where Id = @Id
                ";
                    cmd.Parameters.AddWithValue("@Id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void PostMealPlan(int userId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO MealPlan (UserId, IsConfirmed, IsComplete)
                        VALUES(@UserId, 'false', 'false');
                    ";
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
