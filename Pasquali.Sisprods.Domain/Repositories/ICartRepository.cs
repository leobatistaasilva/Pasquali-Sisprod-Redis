using System;
using System.Collections.Generic;
using Pasquali.Sisprods.Domain.Entities;

namespace Pasquali.Sisprods.Domain.Repositories
{
    public interface ICartRepository
    {
        void Create(Cart Cart);
        void Update(Cart Cart);
        Cart GetById(int id);
        IEnumerable<Cart> GetAll();
        void Delete(Cart Cart);
        Cart GetByIdWithProducts(int id);
        void Bind<T>(T entities, string named = null);
        
    }
}