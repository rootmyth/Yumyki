using Yumyki.Models;

namespace Yumyki.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(string firebaseId);
        bool CheckIfUserExists(string firebaseId);
        void PostUser(User user);
    }
}
