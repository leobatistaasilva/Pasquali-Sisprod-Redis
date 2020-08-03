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
    public class CartRepository : ICartRepository
    {
        private SisprodsContext _ctx;

        public CartRepository(SisprodsContext ctx)
        {
            _ctx = ctx;
        }

        public void Create(Cart Cart)
        {
            _ctx.Carts.Add(Cart);
            _ctx.SaveChanges();
        } 

        public IEnumerable<Cart> GetAll()
        {
            return _ctx.Carts
                    //.AsNoTracking()
                    .ToList();
        }

        public Cart GetById(int id)
        {
            return null;
            //return _ctx
            //    .Carts
            //    .FirstOrDefault(CartQueries.GetById(id));
        }

        public void Update(Cart Cart)
        {
            _ctx.Entry(Cart).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public void Delete(Cart Cart)
        {
            _ctx.Carts.Remove(Cart);
            _ctx.SaveChanges();
        }


        public Cart GetByIdWithProducts(int id)
        {
            return null;
        }

        public void Bind<T>(T entities, string named = null)
        {
            throw new NotImplementedException();
        }
    }
}
