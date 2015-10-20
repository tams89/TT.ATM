using System;
using System.Linq;
using System.Web.Mvc;
using TT.ATM.Domain.Service;

namespace TT.ATM.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IAccountService _accountService;

        public HomeController()
        {
            _authenticationService = new AuthenticationService();
            _accountService = new AccountService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PinVerified()
        {
            return View("MainMenu");
        }

        // TODO remove hardcoded should be supplied from card details. 
        [HttpPost]
        public ActionResult VerifyPin(string inputValue, int sortCode = 123456, int accountNumber = 12345678)
        {
            try
            {
                _authenticationService.VerifyPin(inputValue, sortCode, accountNumber);
                return View("MainMenu");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Pin", ex.Message);
            }

            return View("Index");
        }


        // TODO remove hardcoded should be supplied from card details. 
        public ActionResult CurrentBalance(int sortCode = 123456, int accountNumber = 12345678)
        {
            var balance = _accountService.GetBalance(sortCode, accountNumber);
            return View(balance);

        }

        // TODO remove hardcoded should be supplied from card details. 
        public ActionResult PreviousTransactions(int sortCode = 123456, int accountNumber = 12345678)
        {
            var transactions = _accountService.GetTransactions(sortCode, accountNumber)
                .Select(x=> new { Date = x.TransactionTime, Amount = x.Amount });

            return View(transactions);
        }

        [HttpGet]
        public ActionResult WithdrawCash()
        {
            return View();
        }

        // TODO remove hardcoded should be supplied from card details. 
        [HttpPost]
        public ActionResult WithdrawCash(int amount, int sortCode = 123456, int accountNumber = 12345678)
        {
            try
            {
                _accountService.WithdrawCash(amount, sortCode, accountNumber);
            }
            // If any issue then back out to pin entry screen.
            catch (OverflowException ex)
            {
                ModelState.AddModelError("Overflow", ex.Message);
                return View("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("PinVerified");
        }
    }
}