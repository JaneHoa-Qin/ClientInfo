namespace ClientInfo.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ClientInfo.DataLayer.ClientAddressContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ClientInfo.DataLayer.ClientAddressContext context)
        {
            context.Clients.AddOrUpdate(
               c => c.ClientId,
               new Model.Client { ClientId = 1, FullName = "John Doe" },
               new Model.Client { ClientId = 2, FullName = "Jane Smith" }
           );
            context.Addresses.AddOrUpdate(
                a => a.AddressId,
                new Model.Address { AddressId = 1, Street = "123 Main St", Email = "jd@gmail.com", ClientId = 1 },
                new Model.Address { AddressId = 2, Street = "456 Elm St", Email = "js@yahoo.com", ClientId = 2 }
            );
        }
    }
}
