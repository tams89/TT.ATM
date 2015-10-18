using System.Collections.Generic;
using System.Linq;

namespace TT.ATM.Domain.Model
{
    public class Account : EntityBase
    {
        public int Pin { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; }

        public int CurrentBalance
        {
            get { return Transactions.Sum(x => x.Amount); }
        }
    }
}