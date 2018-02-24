using Lab7.Data;
using Lab7.Data.Entities;
using Lab7.Models.Views;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Lab7.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            return View(GetAllUsers());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userViewModel);
            }

            SaveUser(MapToUser(userViewModel));

            return RedirectToAction("List");
        }

        public ActionResult Details(int id)
        {
            return View(GetUser(id));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(GetUser(id));
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userViewModel);
            }

            UpdateUser(userViewModel);

            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (id == 0) return HttpNotFound();
            var user = GetUser(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Delete(UserViewModel user)
        {
            DeleteUser(user.Id);
            return RedirectToAction("List");
        }

        private UserViewModel GetUser(int id)
        {
            var dbContext = new AppDbContext();
            var user = dbContext.Users.Find(id);

            return MapToUserViewModel(user);
        }

        private IEnumerable<UserViewModel> GetAllUsers()
        {
            var userViewModels = new List<UserViewModel>();

            var dbContext = new AppDbContext();

            foreach (var user in dbContext.Users)
            {
                var userViewModel = MapToUserViewModel(user);
                userViewModels.Add(userViewModel);
            }

            return userViewModels;
        }

        private void SaveUser(User user)
        {
            var dbContext = new AppDbContext();
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }

        private void UpdateUser(UserViewModel userViewModel)
        {
            var dbContext = new AppDbContext();
            var user = dbContext.Users.Find(userViewModel.Id);
            CopyToUser(userViewModel, user);
            dbContext.SaveChanges();
        }

        private void DeleteUser(int id)
        {
            var dbContext = new AppDbContext();

            var user = dbContext.Users.Find(id);

            if (user != null)
            {
                dbContext.Users.Remove(user);
                dbContext.SaveChanges();
            }
        }

        private User MapToUser(UserViewModel userViewModel)
        {
            return new User
            {
                Id = userViewModel.Id,
                FirstName = userViewModel.FirstName,
                MiddleName = userViewModel.MiddleName,
                LastName = userViewModel.LastName,
                EmailAddress = userViewModel.EmailAddress,
                DateOfBirth = userViewModel.DOB,
                YearsInSchool = userViewModel.YearsInSchool
            };
        }

        private UserViewModel MapToUserViewModel(User user)
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

        private void CopyToUser(UserViewModel userViewModel, User user)
        {
            user.FirstName = userViewModel.FirstName;
            user.MiddleName = userViewModel.MiddleName;
            user.LastName = userViewModel.LastName;
            user.EmailAddress = userViewModel.EmailAddress;
            user.DateOfBirth = userViewModel.DOB;
            user.YearsInSchool = userViewModel.YearsInSchool;
        }
    }
}