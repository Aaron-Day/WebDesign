using Lab8.Models.View;
using Lab8.Service;
using log4net;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;

namespace Lab8.Controllers
{
    [Authorize]
    public class PetController : Controller
    {
        private readonly IPetService _service;
        private readonly ILog _log = LogManager.GetLogger(typeof(PetController));

        public PetController(IPetService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            _log.Info($"Index, userId: {userId}");
            if (userId == null)
            {
                _log.Debug("Invalid userId");
                return HttpNotFound();
            }

            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var userId = User.Identity.GetUserId();
            _log.Info($"List, userId: {userId}");
            if (string.IsNullOrWhiteSpace(userId))
            {
                _log.Debug("Invalid userId");
                return HttpNotFound();
            }

            try
            {
                var pets = _service.GetUsersPets(userId);
                return View(pets);
            }
            catch (Exception e)
            {
                _log.Error($"Failed to retrieve pets for user: {userId}", e);
                throw;
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            _log.Info($"Create[HttpGet], userId: {userId}");
            if (string.IsNullOrWhiteSpace(userId))
            {
                _log.Debug("Invalid userId passed");
                return HttpNotFound();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                pet.UserId = User.Identity.GetUserId();
                _service.CreatePet(pet);
                return RedirectToAction("List");
            }
            catch (Exception e)
            {
                _log.Error($"Failed to save pet: {pet.Id} for user: {pet.UserId}", e);
                throw;
            }
        }

        public ActionResult Details(int id)
        {
            _log.Info($"Details, id: {id}");
            if (id <= 0)
            {
                _log.Debug("Invalid id passed");
                return HttpNotFound();
            }

            try
            {
                return View(_service.GetPet(id));
            }
            catch (Exception e)
            {
                _log.Error($"Failed to get pet: {id}", e);
                throw;
            }
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

            try
            {
                return View(_service.GetPet(id));
            }
            catch (Exception e)
            {
                _log.Error($"Failed to get pet: {id}", e);
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                return RedirectToAction("List");
            }
            catch (Exception e)
            {
                _log.Error($"Failed to update pet: {pet.Id} for user: {pet.UserId}", e);
                throw;
            }
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

            try
            {
                return View(_service.GetPet(id));
            }
            catch (Exception e)
            {
                _log.Error($"Failed to get pet: {id}", e);
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(PetViewModel pet)
        {
            var userId = User.Identity.GetUserId();
            _log.Info($"Delete[HttpPost], pet: {pet.Id}, user: {userId}");
            if (pet.Id <= 0)
            {
                _log.Debug("Invalid PetViewModel passed");
                return HttpNotFound();
            }

            try
            {
                _service.DeletePet(pet.Id);
                return RedirectToAction("List");
            }
            catch (Exception e)
            {
                _log.Error($"Failed to delete pet: {pet.Id} for user: {userId}", e);
                throw;
            }
        }
    }
}