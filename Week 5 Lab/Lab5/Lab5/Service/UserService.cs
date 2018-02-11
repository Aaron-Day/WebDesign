using Lab5.Data.Entities;
using Lab5.Models.View;
using Lab5.Repository;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab5.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly ILog _log = log4net.LogManager.GetLogger(typeof(UserService));

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public UserViewModel GetUser(int id)
        {
            _log.Info($"GetUser, id: {id}");

            UserViewModel user;
            try
            {
                user = MapToUserViewModel(_repository.GetUser(id));
            }
            catch (Exception e)
            {
                _log.Error($"Failure to get user: {id}");
                throw;
            }

            return user;
        }

        public IEnumerable<UserViewModel> GetAllUsers()
        {
            _log.Info("GetAllUsers");

            IEnumerable<UserViewModel> views;
            try
            {
                var users = _repository.GetAllUsers();
                views = users.Select(MapToUserViewModel).ToList();
            }
            catch (Exception e)
            {
                _log.Error($"Failure to get all users", e);
                throw;
            }

            return views;
        }

        public void CreateUser(UserViewModel user)
        {
            _log.Info($"CreateUser");

            try
            {
                _repository.CreateUser(MapToUser(user));
            }
            catch (Exception e)
            {
                _log.Error($"Failure to create user", e);
                throw;
            }
        }

        public void UpdateUser(UserViewModel user)
        {
            _log.Info($"UpdateUser, user: {user.Id}");

            try
            {
                var u = _repository.GetUser(user.Id);
                CopyToUser(user, u);
                _repository.UpdateUser(u);
            }
            catch (Exception e)
            {
                _log.Error($"Failure to update user: {user.Id}", e);
                throw;
            }
        }

        public void DeleteUser(int id)
        {
            _log.Info($"DeleteUser, id: {id}");

            try
            {
                _repository.DeleteUser(id);
            }
            catch (Exception e)
            {
                _log.Error($"Failure to delete user: {id}", e);
                throw;
            }
        }

        //--------------------------------------------------//
        // METHODS

        private User MapToUser(UserViewModel user)
        {
            _log.Info($"MapToUser, id: {user.Id}");

            try
            {
                return new User
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName,
                    EmailAddress = user.EmailAddress,
                    DateOfBirth = user.DOB,
                    YearsInSchool = user.YearsInSchool
                };
            }
            catch (Exception e)
            {
                _log.Error($"Failure to map to user: {user.Id}", e);
                throw;
            }
        }

        private UserViewModel MapToUserViewModel(User user)
        {
            _log.Info($"MapToUserViewModel, user: {user.Id}");

            try
            {
                return new UserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName,
                    EmailAddress = user.EmailAddress,
                    DOB = user.DateOfBirth,
                    YearsInSchool = user.YearsInSchool
                };
            }
            catch (Exception e)
            {
                _log.Error($"Failure to map to UserViewModel: {user.Id}", e);
                throw;
            }
        }

        private void CopyToUser(UserViewModel view, User user)
        {
            _log.Info($"CopyToUser, user: {view.Id}");
            try
            {
                user.Id = view.Id;
                user.FirstName = view.FirstName;
                user.MiddleName = view.MiddleName;
                user.LastName = view.LastName;
                user.EmailAddress = view.EmailAddress;
                user.DateOfBirth = view.DOB;
                user.YearsInSchool = view.YearsInSchool;
            }
            catch (Exception e)
            {
                _log.Error($"Failure to copy to user: {view.Id}", e);
                throw;
            }
        }
    }
}