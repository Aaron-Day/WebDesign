using System.Web.Mvc;

namespace Lab1.Controllers
{
    public class PersonController : Controller
    {
        // GET: Person
        public ActionResult Index()
        {
            ViewBag.person = "person";

            return View();
        }
    }
}