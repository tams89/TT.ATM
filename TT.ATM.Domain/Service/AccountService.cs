using System;
using System.Collections.Generic;
using TT.ATM.Domain.Model;

namespace TT.ATM.Domain.Service
{
    public class AccountService
    {
        public IEnumerable<Transaction> GetBalance(int numberOfTransactions = 5)
        {
            throw new NotImplementedException();
        }

        public void WithdrawCash(int amount)
        {
            throw new NotImplementedException();
        }
    }
}