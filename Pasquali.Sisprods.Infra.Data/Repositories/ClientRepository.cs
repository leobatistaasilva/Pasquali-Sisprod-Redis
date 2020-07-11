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
    public class ClientRepository : IClientRepository
    {
        private SisprodsContext _ctx;

        public ClientRepository(SisprodsContext ctx)
        {
            _ctx = ctx;
        }

        public void Create(Client client)
        {
            _ctx.Clients.Add(client);
            _ctx.SaveChanges();
        } 

        public IEnumerable<Client> GetAll()
        {
            return _ctx.Clients
                    .Include(x=>x.Products)
                    //.AsNoTracking()
                    .ToList();
        }

        public Client GetById(int id)
        {
            return _ctx
                .Clients
                .FirstOrDefault(ClientQueries.GetById(id));
        }

        public void Update(Client client)
        {
            _ctx.Entry(client).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public void Delete(Client client)
        {
            _ctx.Clients.Remove(client);
            _ctx.SaveChanges();
        }


        public Client GetByIdWithProducts(int id)
        {
            return _ctx
               .Clients
               .Include(x => x.Products)
               .Where(ClientQueries.GetByIdWithProducts(id))
               .FirstOrDefault(ClientQueries.GetById(id));
        }

        public void Bind<T>(T entities, string named = null)
        {
            throw new NotImplementedException();
        }
    }
}
