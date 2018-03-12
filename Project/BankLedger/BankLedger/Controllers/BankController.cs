using BankLedger.Services;
using log4net;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace BankLedger.Controllers
{
    [Authorize]
    public class BankController : Controller
    {
        private readonly IBankService _service;
        private readonly ILog _log = LogManager.GetLogger(typeof(BankController));

        public BankController(IBankService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var accounts = _service.GetAccounts(User.Identity.GetUserId());
            return View(accounts);
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}