using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Pasquali.Sisprods.Infra.Data.Contexts;
using Pasquali.Sisprods.Domain.Entities;

namespace Pasquali.Sisprods.Infra.Data.Configuration.Seeds
{
    public class SisprodsDbInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<SisprodsContext>
    {
        protected override void Seed(SisprodsContext context)
        {
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
            new Product("Meia arrastao"),
            };
            products.ForEach(p => context.Products.Add(p));
            context.SaveChanges();
        }
    }
}