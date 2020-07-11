using System;
using System.Collections.Generic;
using System.Linq;
using Pasquali.Sisprods.Infra.Data.Contexts;
using Pasquali.Sisprods.Domain.Entities;
using Pasquali.Sisprods.Domain.Repositories;
using Pasquali.Sisprods.Domain.Queries;
using System.Data.Entity;

namespace Pasquali.Sisprods.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private SisprodsContext _ctx;

        public ProductRepository(SisprodsContext ctx)
        {
            _ctx = ctx;
        }

        public void Create(Product product)
        {
            _ctx.Products.Add(product);
            _ctx.SaveChanges();
        } 

        public IEnumerable<Product> GetAll()
        {
            return _ctx.Products
                    //.AsNoTracking()
                    .ToList();
        }

        public Product GetById(int id)
        {
            return _ctx
                .Products
                .FirstOrDefault(ProductGetByIdQuery.Constraint(id));
        }

        public void Update(Product product)
        {
            _ctx.Entry(product).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public void Delete(Product product)
        {
            _ctx.Products.Remove(product);
            _ctx.SaveChanges();
        }


    }
}
