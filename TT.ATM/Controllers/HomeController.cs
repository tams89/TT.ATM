using System;
using System.Web.Mvc;
using TT.ATM.Domain.Service;

namespace TT.ATM.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public HomeController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PinVerified()
        {
            return View("MainMenu");
        }

        public ActionResult VerifyPin(string inputValue)
        {
            try
            {
                _authenticationService.VerifyPin(inputValue);
                return View("MainMenu");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Pin", ex.Message);
            }

            return View("Index");
        }

       

        public ActionResult CurrentBalance()
        {
            // TODO get balance info from DB.
            return View();

        }

        public ActionResult PreviousTransactions()
        {
            // TODO get balance info from DB.
            return View();
        }

        public ActionResult WithdrawCash()
        {
            // TODO get balance info from DB.
            return View();
        }
    }
}