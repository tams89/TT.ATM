using System;

namespace TT.ATM.Domain.Model
{
    public class Transaction : EntityBase
    {
        public DateTime TransactionTime { get; set; }

        public int Amount { get; set; }
    }
}