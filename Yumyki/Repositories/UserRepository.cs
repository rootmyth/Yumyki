using System.Data.SqlClient;
using Yumyki.Interfaces;
using Yumyki.Models;

namespace Yumyki.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration) { }
        
        public User GetUser(string firebaseId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, FirebaseId, Username FROM [User] WHERE FirebaseId = @FirebaseId
                    ";
                    cmd.Parameters.AddWithValue("@FirebaseId", firebaseId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        User user = null;
                        while (reader.Read())
                        {
                            user = new()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FirebaseId = reader.GetString(reader.GetOrdinal("FirebaseId")),
                                Username = reader.GetString(reader.GetOrdinal("Username"))
                            };
                        }
                        return user;
                    }
                }
            }
        }

        public bool CheckIfUserExists(string firebaseId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT 1 FROM [User] WHERE FirebaseId = @FirebaseId
                    ";
                    cmd.Parameters.AddWithValue("@FirebaseId", firebaseId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        bool userExists = false;
                        if (reader.Read())
                        {
                            userExists = true;
                        }
                        return userExists;
                    }
                }
            }
        }

        public void PostUser(User user)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                INSERT INTO [User] (FirebaseId, Username)
                                VALUES(@FirebaseId, @Username);
                            ";

                    cmd.Parameters.AddWithValue("@FirebaseId", user.FirebaseId);
                    cmd.Parameters.AddWithValue("@Username", user.Username);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
