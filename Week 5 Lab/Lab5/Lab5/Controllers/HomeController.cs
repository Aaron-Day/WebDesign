using log4net;
using System.Web.Mvc;

namespace Lab5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILog _log = log4net.LogManager.GetLogger(typeof(HomeController));

        public ActionResult Index()
        {
            _log.Info("Index");
            return View();
        }

        public ActionResult About()
        {
            _log.Info("About");
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}