using System.Web.Mvc;

namespace BankLedger.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Bank Ledger.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Aaron Day.";

            return View();
        }
    }
}