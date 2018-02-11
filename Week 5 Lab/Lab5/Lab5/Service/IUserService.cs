using Lab5.Models.View;
using System.Collections.Generic;

namespace Lab5.Service
{
    public interface IUserService
    {
        UserViewModel GetUser(int id);
        IEnumerable<UserViewModel> GetAllUsers();
        void CreateUser(UserViewModel user);
        void UpdateUser(UserViewModel user);
        void DeleteUser(int id);
    }
}