using System;
using System.Security.Authentication;
using NUnit.Framework;
using TT.ATM.Domain.Service;

namespace TT.ATM.Test
{
    /*

        Preparation

        “The point of the test is to see how they construct a solution and their thought processes in doing so.   The tasks should involve the sort of skills we would want to see.”
        
        •	If you have a laptop and do the solution bring it in and you can demonstrate directly with the team, or you can add to GitHub/send to the below email and discuss on the desktop
        •	While the prep will be time consuming it will allow for a most productive and relevant interview, and allows the chance to show off your thinking 
        •	They are aware of your experience with .NET and will take this into account, it is about your conceptual knowledge.
        
        
        B)Programming Test- Looking to discuss your approaches while in the interview, so looking for you to complete before if possible. 
        
        Create an ATM simulation using C# with either WPF or ASP.NET and a SQL Server database.
        Similar to an ATM, the program should have at least the following features:
        •	Assume that the user has inserted their card. The user details are as follows:
        o	Name:                  Fred; 
        o	Balance:               £2,000; 
        o	PIN:                       0123. 
        These details should be stored on a user table. You need not design a UI for maintaining this.
        •	Verify the user by asking for a PIN number. 
        •	In case of negative verification, notify the user.
        •	In case of positive verification, showing at least the following options:
        o	Current balance;
        o	Previous five transactions;
        o	Cash withdrawal.
         
        •	The user should not withdraw over £1000 of cash in one day.
        •	The user should not total more than 10 transactions in one day.
        •	When the application is restarted, the current balance and transactions should persist.


    */

    public class AtmTests
    {
        public void VerifiesPinOnCardEntry_ShouldFail()
        {
            var service = new AuthenticationService();
            Assert.Throws<AuthenticationException>(() => service.VerifyPin("werfvwef"));
        }

        public void VerifiesPinOnCardEntry_ShouldPass()
        {
            var service = new AuthenticationService();
            Assert.DoesNotThrow(() => service.VerifyPin("4567"));
        }

        public void CanSeePastFiveTransactions_OnceAuthorised()
        {
            var service = new AccountService();
            var data = service.GetBalance(numberOfTransactions: 5);
            Assert.IsNotEmpty(data);
        }

        public void CanWithdraw_OnceAuthorised()
        {
            var service = new AccountService();
            Assert.DoesNotThrow(() => service.WithdrawCash(amount: 500));
        }

        public void CannotWithdraw_MoreThan_OneThousandPerDay()
        {
            var service = new AccountService();
            Assert.DoesNotThrow(() => service.WithdrawCash(amount: 500));
            Assert.Throws<OverflowException>(() => service.WithdrawCash(amount: 500));
        }

        public void Cannot_CarryOut_MoreThanTenTransactionsPerDay()
        {
            var service = new AccountService();

            for (int i = 0; i < 10; i++)
            {
                if (i < 9)
                    Assert.DoesNotThrow(() => service.WithdrawCash(amount: 1));
                else
                    Assert.Throws<OverflowException>(() => service.WithdrawCash(amount: 1));
            }
        }
    }
}