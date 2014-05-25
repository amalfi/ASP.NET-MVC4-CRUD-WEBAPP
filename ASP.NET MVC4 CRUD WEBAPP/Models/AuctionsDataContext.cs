using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ASP.NET_MVC4_CRUD_WEBAPP.Models
{
    public class AuctionsDataContext : DbContext
    {
        public DbSet<Auction> Auctions { get; set; }

        static AuctionsDataContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AuctionsDataContext>());
        }
    }
}