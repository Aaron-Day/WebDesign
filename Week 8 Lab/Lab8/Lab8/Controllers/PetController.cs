using Lab8.Models.View;
using Lab8.Service;
using log4net;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Lab8.Controllers
{
    public class PetController : Controller
    {
        private readonly IPetService _service;
        private readonly ILog _log = log4net.LogManager.GetLogger(typeof(PetController));

        public PetController(IPetService service)
        {
            _service = service;
        }

        public ActionResult Index(int userId)
        {
            _log.Info($"Index, userId: {userId}");
            if (userId <= 0)
            {
                _log.Debug("Invalid userId passed");
                return HttpNotFound();
            }

            return RedirectToAction("List", new { userId });
        }

        public ActionResult List(int userId)
        {
            _log.Info($"List, userId: {userId}");
            if (userId <= 0)
            {
                _log.Debug("Invalid userId passed");
                return HttpNotFound();
            }

            IEnumerable<PetViewModel> pets;
            try
            {
                pets = _service.GetUsersPets(userId);
            }
            catch (Exception e)
            {
                _log.Error($"Failed to retrieve pets for user: {userId}", e);
                throw;
            }

            ViewBag.UserId = userId;
            return View(pets);
        }

        [HttpGet]
        public ActionResult Create(int userId)
        {
            _log.Info($"Create[HttpGet], userId: {userId}");
            if (userId <= 0)
            {
                _log.Debug("Invalid userId passed");
                return HttpNotFound();
            }

            ViewBag.UserId = userId;
            return View();
        }

        [HttpPost]
        public ActionResult Create(PetViewModel pet)
        {
            _log.Info($"Create[HttpPost], pet: {pet.Id}, user: {pet.UserId}");
            if (!ModelState.IsValid)
            {
                _log.Debug("Invalid PetViewModel passed");
                return View(pet);
            }

            try
            {
                _service.CreatePet(pet);
            }
            catch (Exception e)
            {
                _log.Error($"Failed to save pet: {pet.Id} for user: {pet.UserId}", e);
                throw;
            }

            return RedirectToAction("List", new { userId = pet.UserId });
        }

        public ActionResult Details(int id)
        {
            _log.Info($"Details, id: {id}");
            if (id <= 0)
            {
                _log.Debug("Invalid id passed");
                return HttpNotFound();
            }

            PetViewModel pet;
            try
            {
                pet = _service.GetPet(id);
            }
            catch (Exception e)
            {
                _log.Error($"Failed to get pet: {id}", e);
                throw;
            }

            return View(pet);
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

            PetViewModel pet;
            try
            {
                pet = _service.GetPet(id);
            }
            catch (Exception e)
            {
                _log.Error($"Failed to get pet: {id}", e);
                throw;
            }

            return View(pet);
        }

        [HttpPost]
        public ActionResult Edit(PetViewModel pet)
        {
            _log.Info($"Edit[HttpPost], pet: {pet.Id}, user: {pet.UserId}");
            if (!ModelState.IsValid)
            {
                _log.Debug("Invalid PetViewModel passed");
                return View(pet);
            }

            try
            {
                _service.UpdatePet(pet);
            }
            catch (Exception e)
            {
                _log.Error($"Failed to update pet: {pet.Id} for user: {pet.UserId}", e);
                throw;
            }

            return RedirectToAction("List", new { userId = pet.UserId });
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

            PetViewModel pet;
            try
            {
                pet = _service.GetPet(id);
            }
            catch (Exception e)
            {
                _log.Error($"Failed to get pet: {id}", e);
                throw;
            }

            TempData["UserId"] = pet.UserId;
            return View(pet);
        }

        [HttpPost]
        public ActionResult Delete(PetViewModel pet)
        {
            _log.Info($"Delete[HttpPost], pet: {pet.Id}, user: {TempData["UserId"]}");
            if (pet.Id <= 0)
            {
                _log.Debug("Invalid PetViewModel passed");
                return HttpNotFound();
            }

            try
            {
                _service.DeletePet(pet.Id);
            }
            catch (Exception e)
            {
                _log.Error($"Failed to delete pet: {pet.Id} for user: {TempData["UserId"]}", e);
                throw;
            }

            return RedirectToAction("List", new { userId = TempData["UserId"] });
        }
    }
}