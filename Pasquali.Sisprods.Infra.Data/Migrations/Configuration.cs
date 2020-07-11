namespace Pasquali.Sisprods.Infra.Data.Migrations
{
    using Pasquali.Sisprods.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Pasquali.Sisprods.Infra.Data.Contexts.SisprodsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Pasquali.Sisprods.Infra.Data.Contexts.SisprodsContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            var clients = new List<Client>
            {
                new Client("Leonardo"),
                new Client("Arthur"),
                new Client("Aidenir"),
                new Client("Ana Paula")
            };
            clients.ForEach(x => context.Clients.Add(x));
            context.SaveChanges();

            var products = new List<Product>
            {
                new Product("Camisa Azul"),
                new Product("Calça Jeans corsalle"),
                new Product("Pijama rosa"),
                new Product("Sandália Havaiana"),
                new Product("Boné preto"),
                new Product("Calça de Moleton"),
                new Product("Short esportivo"),
                new Product("Meia arrastao")
            };
            products.ForEach(p => context.Products.Add(p));
            context.SaveChanges();
        }
    }
}
