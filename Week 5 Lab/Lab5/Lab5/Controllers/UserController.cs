using Lab5.Models.View;
using Lab5.Service;
using log4net;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Lab5.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _service;
        private readonly ILog _log = log4net.LogManager.GetLogger(typeof(UserController));

        public UserController(IUserService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            _log.Info("Index");
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            _log.Info("List");

            IEnumerable<UserViewModel> users;
            try
            {
                users = _service.GetAllUsers();
            }
            catch (Exception e)
            {
                _log.Error("Failed to retrieve all users", e);
                throw;
            }

            return View(users);
        }

        [HttpGet]
        public ActionResult Create()
        {
            _log.Info("Create[HttpGet]");
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserViewModel user)
        {
            _log.Info($"Create[HttpPost], creating user: {user.Id}");
            if (!ModelState.IsValid)
            {
                _log.Debug("Invalid UserViewModel passed");
                return View(user);
            }

            try
            {
                _service.CreateUser(user);
            }
            catch (Exception e)
            {
                _log.Error($"Failed to create user: {user.Id}", e);
                throw;
            }

            return RedirectToAction("List");
        }

        public ActionResult Details(int id)
        {
            _log.Info($"Details, id: {id}");
            if (id <= 0)
            {
                _log.Debug("Invalid id passed");
                return HttpNotFound();
            }

            UserViewModel user;
            try
            {
                user = _service.GetUser(id);
            }
            catch (Exception e)
            {
                _log.Error($"Failed to get user: {id}", e);
                throw;
            }

            return View(user);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            _log.Info($"Edit[HttpGet], id: {id}");
            if (id <= 0)
            {
                _log.Debug("Invalid id passed");
                return HttpNotFound();
            }

            UserViewModel user;
            try
            {
                user = _service.GetUser(id);
            }
            catch (Exception e)
            {
                _log.Error($"Failed to get user: {id}", e);
                throw;
            }

            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel user)
        {
            _log.Info($"Edit[HttpPost], user: {user.Id}");
            if (!ModelState.IsValid)
            {
                _log.Debug("Invalid UserViewModel passed");
                return View(user);
            }

            try
            {
                _service.UpdateUser(user);
            }
            catch (Exception e)
            {
                _log.Error($"Failure to update user: {user.Id}");
                throw;
            }

            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            _log.Info($"Delete[HttpGet], id: {id}");
            if (id <= 0)
            {
                _log.Debug("Invalid id passed");
                return HttpNotFound();
            }

            UserViewModel user;
            try
            {
                user = _service.GetUser(id);
            }
            catch (Exception e)
            {
                _log.Error($"Failure to get user: {id}");
                throw;
            }

            return View(user);
        }

        [HttpPost]
        public ActionResult Delete(UserViewModel user)
        {
            _log.Info($"Delete[HttpPost], user: {user.Id}");
            try
            {
                _service.DeleteUser(user.Id);
            }
            catch (Exception e)
            {
                _log.Error($"Failure to delete user: {user.Id}");
                throw;
            }

            return RedirectToAction("List");
        }
    }
}