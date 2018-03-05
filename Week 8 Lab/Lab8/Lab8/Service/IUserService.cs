using Lab8.Models.View;
using System.Collections.Generic;

namespace Lab8.Service
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