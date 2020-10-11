using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnuCash.DataModel.DatabaseModel
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Database names")]
    public class GnuCashContext : DbContext
    {
        public GnuCashContext() : base() { }
        public GnuCashContext(DbContextOptions options) : base(options) { }

        
        public DbSet<Price> prices { get; set; }

        public DbSet<Commodity> commodities { get; set; }
        
        public DbSet<Transaction> transactions { get; set; }
        
        public DbSet<Split> splits { get; set; }
        
        public DbSet<Account> accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
