using Lab3.Data;
using Lab3.Data.Entities;
using System.Web.Mvc;

namespace Lab3.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            InMemoryDatabase.Users.Add(user);
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            return View(InMemoryDatabase.Users);
        }
    }
}