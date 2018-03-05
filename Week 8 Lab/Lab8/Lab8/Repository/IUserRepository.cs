using Lab8.Data.Entities;
using System.Collections.Generic;

namespace Lab8.Repository
{
    public interface IUserRepository
    {
        User GetUser(int id);
        IEnumerable<User> GetAllUsers();
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }
}