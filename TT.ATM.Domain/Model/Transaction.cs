using System;

namespace TT.ATM.Domain.Model
{
    public class Transaction : EntityBase
    {
        public Transaction(int accountId, DateTime transactionTime, int amount)
        {
            AccountId = accountId;
            TransactionTime = transactionTime;
            Amount = amount;
        }

        public Transaction() { }

        public int AccountId { get; set; }

        public DateTime TransactionTime { get; set; }

        public decimal Amount { get; set; }
    }
}