using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ClientInfo.Model;

namespace ClientInfo.DataLayer
{
    public class ClientAddressContext:DbContext
    {
        public ClientAddressContext() : base("ClientAddressDB")
        {
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Address> Addresses { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasMany(c => c.Addresses)
                .WithRequired(a => a.Client)
                .HasForeignKey(a => a.ClientId);
            base.OnModelCreating(modelBuilder);
        }
    }
}