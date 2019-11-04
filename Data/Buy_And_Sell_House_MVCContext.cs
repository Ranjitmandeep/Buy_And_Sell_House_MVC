using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Buy_And_Sell_House_Core_Webapp.Business;

namespace Buy_And_Sell_House_MVC.Models
{
    public class Buy_And_Sell_House_MVCContext : DbContext
    {
        public Buy_And_Sell_House_MVCContext (DbContextOptions<Buy_And_Sell_House_MVCContext> options)
            : base(options)
        {
        }

        public DbSet<Buy_And_Sell_House_Core_Webapp.Business.Buyer> Buyer { get; set; }

        public DbSet<Buy_And_Sell_House_Core_Webapp.Business.House> House { get; set; }

        public DbSet<Buy_And_Sell_House_Core_Webapp.Business.Seller> Seller { get; set; }

        public DbSet<Buy_And_Sell_House_Core_Webapp.Business.Transaction> Transaction { get; set; }
    }
}
