using System;
using System.Collections.Generic;
using Pasquali.Sisprods.Domain.Entities;

namespace Pasquali.Sisprods.Domain.Repositories
{
    public interface IProductRepository
    {
        void Create(Product Product);
        void Update(Product Product);
        Product GetById(int id);
        IEnumerable<Product> GetAll();
        void Delete(Product product);
        //Product GetByIdWithClients(int id);
        
    }
}