using System;
using System.Linq;
using System.Security.Authentication;

namespace TT.ATM.Domain.Service
{
    public interface IAuthenticationService
    {
        void VerifyPin(string inputValue, int sortCode, int accountNumber);
    }

    public class AuthenticationService : ServiceBase, IAuthenticationService
    {
        public void VerifyPin(string inputValue, int sortCode, int accountNumber)
        {
            int pin;
            if (!int.TryParse(inputValue, out pin))
                throw new AuthenticationException("Invalid Pin");

            var account = Context.Accounts.Single(x =>
                x.AccountNumber == accountNumber &&
                x.SortCode == sortCode);

            if (!string.Equals(account.Pin, inputValue, StringComparison.InvariantCulture))
                throw new AuthenticationException("Invalid Pin");
        }
    }
}