using System;
using System.Data.Entity.Migrations;
using TT.ATM.Domain.Model;
using TT.ATM.Domain.ObjectMapper;

namespace TT.ATM.Domain.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<ATMContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ATMContext context)
        {
            Add(context);
        }

        public static void Add(ATMContext context)
        {
            context.Accounts.AddOrUpdate(new Account
            {
                Id = 1,
                AccountHolder = "Fred",
                AccountNumber = 12345678,
                SortCode = 123456,
                Pin = "0123"
            });

            context.Transactions.AddOrUpdate(new Transaction
            {
                Id = 1,
                AccountId = 1,
                Amount = 2000,
                TransactionTime = DateTime.UtcNow
            });
        }
    }
}