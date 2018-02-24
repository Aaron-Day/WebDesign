using Lab7.Data;
using Lab7.Data.Entities;
using Lab7.Models.Views;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Lab7.Controllers
{
    public class PetController : Controller
    {
        public ActionResult Index(int userId)
        {
            ViewBag.UserId = userId;

            return RedirectToAction("List", new { userId });
        }

        public ActionResult List(int userId)
        {
            ViewBag.UserId = userId;

            return View(GetUsersPets(userId));
        }

        [HttpGet]
        public ActionResult Create(int userId)
        {
            ViewBag.UserId = userId;

            return View();
        }

        [HttpPost]
        public ActionResult Create(PetViewModel pet)
        {
            if (!ModelState.IsValid)
            {
                return View(pet);
            }

            SavePet(pet);

            return RedirectToAction("List", new { userId = pet.UserId });
        }

        public ActionResult Details(int id)
        {
            var pet = GetPet(id);

            return View(MapToPetViewModel(pet));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var pet = GetPet(id);
            return View(MapToPetViewModel(pet));
        }

        [HttpPost]
        public ActionResult Edit(PetViewModel pet)
        {
            if (!ModelState.IsValid)
            {
                return View(pet);
            }

            UpdatePet(pet);

            return RedirectToAction("List", new { userId = pet.UserId });
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (id == 0) return HttpNotFound();
            var pet = GetPet(id);
            return View(MapToPetViewModel(pet));
        }

        [HttpPost]
        public ActionResult Delete(PetViewModel pet)
        {
            if (pet.Id == 0) return View(pet);
            var mypet = GetPet(pet.Id);
            DeletePet(mypet.Id);
            return RedirectToAction("List", new { userId = mypet.UserId });
        }

        private Pet GetPet(int id)
        {
            var dbContext = new AppDbContext();
            return dbContext.Pets.Find(id);
        }

        private IEnumerable<PetViewModel> GetUsersPets(int userId)
        {
            var petViewModels = new List<PetViewModel>();
            var dbContext = new AppDbContext();
            var pets = dbContext.Pets.Where(pet => pet.UserId == userId).ToList();

            foreach (var pet in pets)
            {
                var petViewModel = MapToPetViewModel(pet);
                petViewModels.Add(petViewModel);
            }

            return petViewModels;
        }

        private void SavePet(PetViewModel pet)
        {
            var dbContext = new AppDbContext();
            var mypet = MapToPet(pet);

            dbContext.Pets.Add(mypet);
            dbContext.SaveChanges();
        }

        private void DeletePet(int id)
        {
            var dbContext = new AppDbContext();
            var pet = dbContext.Pets.Find(id);

            if (pet != null)
            {
                dbContext.Pets.Remove(pet);
                dbContext.SaveChanges();
            }
        }

        private void UpdatePet(PetViewModel pet)
        {
            var dbContext = new AppDbContext();
            var mypet = dbContext.Pets.Find(pet.Id);
            CopyToPet(pet, mypet);
            dbContext.SaveChanges();
        }

        private Pet MapToPet(PetViewModel pet)
        {
            return new Pet
            {
                Id = pet.Id,
                Name = pet.Name,
                Age = pet.Age,
                NextCheckup = pet.NextCheckup,
                VetName = pet.VetName,
                UserId = pet.UserId
            };
        }

        private PetViewModel MapToPetViewModel(Pet pet)
        {
            return new PetViewModel
            {
                Id = pet.Id,
                Name = pet.Name,
                Age = pet.Age,
                NextCheckup = pet.NextCheckup,
                VetName = pet.VetName,
                UserId = pet.UserId
            };
        }

        private void CopyToPet(PetViewModel pet, Pet mypet)
        {
            mypet.Id = pet.Id;
            mypet.Name = pet.Name;
            mypet.Age = pet.Age;
            mypet.NextCheckup = pet.NextCheckup;
            mypet.VetName = pet.VetName;
            mypet.UserId = pet.UserId;
        }
    }
}