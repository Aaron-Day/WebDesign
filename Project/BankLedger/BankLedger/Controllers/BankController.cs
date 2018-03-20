using BankLedger.Models;
using BankLedger.Services;
using log4net;
using Microsoft.AspNet.Identity;
using System;
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

        [HttpGet]
        public ActionResult Create()
        {
            _log.Info("Create Get");
            return View();
        }

        [HttpPost]
        public ActionResult Create(BankViewModel account)
        {
            _log.Info("Create Post");
            if (!ModelState.IsValid) return View(account);

            try
            {
                _log.Info("Create account");
                account.UserId = User.Identity.GetUserId();
                _service.Create(account);

                return RedirectToAction("List");
            }
            catch (Exception e)
            {
                _log.Error("Error creating account: ", e);
                throw;
            }
        }

        public ActionResult Details(string id)
        {
            if (id == null) return HttpNotFound();
            try
            {
                return View(_service.GetAccount(id));
            }
            catch (Exception e)
            {
                _log.Error("Error getting account details: ", e);
                throw;
            }
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (id == null) return HttpNotFound();
            try
            {
                return View(_service.GetAccount(id));
            }
            catch (Exception e)
            {
                _log.Error("Error updating account: ", e);
                throw;
            }
        }

        [HttpPost]
        public ActionResult Edit(BankViewModel account)
        {
            if (!ModelState.IsValid) return View(account);
            try
            {
                _service.Update(account);
                return RedirectToAction("List");
            }
            catch (Exception e)
            {
                _log.Error("Error updating account: ", e);
                throw;
            }
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            if (id == null) return HttpNotFound();
            try
            {
                return View(_service.GetAccount(id));
            }
            catch (Exception e)
            {
                _log.Error("Error deleting account: ", e);
                throw;
            }
        }

        [HttpPost]
        public ActionResult Delete(BankViewModel account)
        {
            try
            {
                _service.Delete(account.Id);
                return RedirectToAction("List");
            }
            catch (Exception e)
            {
                _log.Error("Error deleting account: ", e);
                throw;
            }
        }

        public ActionResult History(string id)
        {
            if (id == null) return HttpNotFound();
            ViewBag.Account = _service.GetAccount(id);
            return View(_service.GetTransactions(id));
        }

        [HttpGet]
        public ActionResult Deposit(string id)
        {
            if (id == null) return HttpNotFound();
            try
            {
                ViewBag.Account = _service.GetAccount(id);
                return View(_service.GetAccount(id));
            }
            catch (Exception e)
            {
                _log.Error("Error depositing to account: ", e);
                throw;
            }
        }

        [HttpPost]
        public ActionResult Deposit(BankViewModel account)
        {
            if (!ModelState.IsValid) return View(account);
            try
            {
                _service.Deposit(account.Id, account.Balance);
                return RedirectToAction("History", new { id = account.Id });
            }
            catch (Exception e)
            {
                _log.Error("Error depositing to account: ", e);
                throw;
            }
        }

        [HttpGet]
        public ActionResult Withdrawl(string id)
        {
            if (id == null) return HttpNotFound();
            try
            {
                ViewBag.Account = _service.GetAccount(id);
                return View(_service.GetAccount(id));
            }
            catch (Exception e)
            {
                _log.Error("Error withdrawing from account: ", e);
                throw;
            }
        }

        [HttpPost]
        public ActionResult Withdrawl(BankViewModel account)
        {
            if (!ModelState.IsValid) return View(account);
            var acct = _service.GetAccount(account.Id);
            if (account.Balance > acct.Balance)
            {
                ViewBag.Account = acct;
                ModelState.AddModelError("Amount", "Withdrawl amount greater than account balance.");
                return View(account);
            }
            try
            {
                _service.Withdrawl(account.Id, account.Balance);
                return RedirectToAction("History", new { id = account.Id });
            }
            catch (Exception e)
            {
                _log.Error("Error withdrawing from account: ", e);
                throw;
            }
        }
    }
}