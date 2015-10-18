using System.Security.Authentication;

namespace TT.ATM.Domain.Service
{
    public interface IAuthenticationService
    {
        void VerifyPin(string inputValue);
    }

    public class AuthenticationService : IAuthenticationService
    {
        public void VerifyPin(string inputValue)
        {
            int pin;
            if (!int.TryParse(inputValue, out pin))
                throw new AuthenticationException("Invalid Pin");
        }
    }
}