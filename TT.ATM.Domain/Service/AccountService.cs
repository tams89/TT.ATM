using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TT.ATM.Domain.Model;

namespace TT.ATM.Domain.Service
{
    public interface IAccountService
    {
        decimal GetBalance(int sortCode, int accountNumber);

        IList<Transaction> GetTransactions(int sortCode, int accountNumber, int numberOfTransactions = 5);

        void WithdrawCash(int amount, int sortCode, int accountNumber);
    }

    public class AccountService : ServiceBase, IAccountService
    {
        public decimal GetBalance(int sortCode, int accountNumber)
        {
            return Context.Accounts
                .Include(x => x.Transactions)
                .Single(
                    x => x.SortCode == sortCode &&
                         x.AccountNumber == accountNumber)
                .CurrentBalance
                ;
        }

        public IList<Transaction> GetTransactions(int sortCode, int accountNumber, int numberOfTransactions = 5)
        {
            return Context.Accounts
                .Include(x => x.Transactions)
                .Single(
                    x => x.SortCode == sortCode &&
                         x.AccountNumber == accountNumber)
                .Transactions
                .OrderByDescending(x => x.TransactionTime)
                .Take(numberOfTransactions)
                .ToList()
                ;
        }

        public void WithdrawCash(int amount, int sortCode, int accountNumber)
        {
            var account = Context.Accounts
                .Include(x => x.Transactions)
                .Single(
                    x => x.SortCode == sortCode &&
                         x.AccountNumber == accountNumber)
                ;

            // Check balance
            var hasFunds = account.CurrentBalance >= amount;

            if (!hasFunds)
            {
                throw new OverflowException("Insufficient funds.");
            }

            var transactionsToday = account.Transactions
                .Where(x => x.TransactionTime.Date == DateTime.UtcNow.Date)
                .ToList();

            // Check number of transactions
            var transactionCount = transactionsToday.Count;
            if (transactionCount >= 10)
            {
                throw new OverflowException("Only 10 transactions allowed per day.");
            }

            // Check amount of transactions

            var amountWithdrawn = transactionsToday.Where(x => x.Amount < 0M).Sum(x => x.Amount) - Math.Abs(amount);
            if (Math.Abs(amount) > 1000 || amountWithdrawn <= -1000)
            {
                throw new OverflowException("£1000 daily limit reached.");
            }

            Context.Transactions.Add(new Transaction(account.Id, DateTime.UtcNow, - Math.Abs(amount)));
            Context.SaveChanges();
        }
    }
}