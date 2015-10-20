using System.Collections.Generic;
using System.Linq;

namespace TT.ATM.Domain.Model
{
    public class Account : EntityBase
    {
        public string AccountHolder { get; set; }

        public string Pin { get; set; }

        public IList<Transaction> Transactions { get; set; }

        public decimal CurrentBalance
        {
            get { return Transactions.Sum(x => x.Amount); }
        }

        public int SortCode { get; set; }

        public int AccountNumber { get; set; }
    }
}