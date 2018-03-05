﻿using Lab8.Data.Entities;
using Lab8.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab8.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILog _log = log4net.LogManager.GetLogger(typeof(UserRepository));

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User GetUser(int id)
        {
            _log.Info($"GetUser, id: {id}");

            User user;
            try
            {
                user = _context.MyUsers.Find(id);
            }
            catch (Exception e)
            {
                _log.Error($"Failure to get user: {id}");
                throw;
            }
            return user;
        }

        public IEnumerable<User> GetAllUsers()
        {
            _log.Info("GetAllUsers");

            IEnumerable<User> users;
            try
            {
                users = _context.MyUsers.ToList();
            }
            catch (Exception e)
            {
                _log.Error("Failure to get all users", e);
                throw;
            }

            return users;
        }

        public void CreateUser(User user)
        {
            _log.Info($"CreateUser");
            try
            {
                _context.MyUsers.Add(user);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _log.Error($"Failure to create user", e);
                throw;
            }
        }

        public void UpdateUser(User user)
        {
            _log.Info($"UpdateUser, user: {user.Id}");
            try
            {
                _context.SaveChanges();
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

            User user;
            try
            {
                user = _context.MyUsers.Find(id);
            }
            catch (Exception e)
            {
                _log.Error($"Failure to find user: {id}", e);
                throw;
            }

            if (user == null)
            {
                _log.Debug($"Unable to find user: {id}");
                return;
            }

            try
            {
                _context.MyUsers.Remove(user);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _log.Error($"Failure to delete user: {id}", e);
                throw;
            }
        }
    }
}