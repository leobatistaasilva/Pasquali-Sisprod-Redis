using System;
using System.Collections.Generic;
using Pasquali.Sisprods.Domain.Entities;

namespace Pasquali.Sisprods.Domain.Repositories
{
    public interface IClientRepository
    {
        void Create(Client client);
        void Update(Client client);
        Client GetById(int id);
        IEnumerable<Client> GetAll();
        void Delete(Client client);
        Client GetByIdWithProducts(int id);
        void Bind<T>(T entities, string named = null);
        
    }
}