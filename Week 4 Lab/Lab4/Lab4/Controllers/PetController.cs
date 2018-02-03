using Lab4.Data;
using Lab4.Data.Entities;
using Lab4.Models.View;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Lab4.Controllers
{
    public class PetController : Controller
    {
        public ActionResult Index(int userId)
        {
            ViewBag.UserId = userId;

            return RedirectToAction("List", new { userId = userId });
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
        public ActionResult Create(PetViewModel petViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(petViewModel);
            }

            SavePet(petViewModel);

            return RedirectToAction("List", new { userId = petViewModel.UserId });
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
        public ActionResult Edit(PetViewModel petViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(petViewModel);
            }

            UpdatePet(petViewModel);

            return RedirectToAction("List", new { userId = petViewModel.UserId });
        }

        public ActionResult Delete(int id)
        {
            var pet = GetPet(id);

            DeletePet(id);

            return RedirectToAction("List", new { userId = pet.UserId });
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

        private void SavePet(PetViewModel petViewModel)
        {
            var dbContext = new AppDbContext();
            var pet = MapToPet(petViewModel);

            dbContext.Pets.Add(pet);
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

        private void UpdatePet(PetViewModel petViewModel)
        {
            var dbContext = new AppDbContext();
            var pet = dbContext.Pets.Find(petViewModel.Id);
            CopyToPet(petViewModel, pet);
            dbContext.SaveChanges();
        }

        private Pet MapToPet(PetViewModel petViewModel)
        {
            return new Pet
            {
                Id = petViewModel.Id,
                Name = petViewModel.Name,
                Age = petViewModel.Age,
                NextCheckup = petViewModel.NextCheckup,
                VetName = petViewModel.VetName,
                UserId = petViewModel.UserId
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

        private void CopyToPet(PetViewModel petViewModel, Pet pet)
        {
            pet.Name = petViewModel.Name;
            pet.Age = petViewModel.Age;
            pet.NextCheckup = petViewModel.NextCheckup;
            pet.VetName = petViewModel.VetName;
        }
    }
}