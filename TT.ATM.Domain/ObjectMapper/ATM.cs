using System.Data.Entity;
using TT.ATM.Domain.Model;

namespace TT.ATM.Domain.ObjectMapper
{
    public class ATM : DbContext
    {
        // Your context has been configured to use a 'ATM' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'TT.ATM.Domain.ATM' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ATM' 
        // connection string in the application configuration file.
        public ATM()
            : base("name=ATM.DataModel")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Transaction> Transactions { get; set; }

        public virtual DbSet<Authentication> Authentications { get; set; }
    }
}