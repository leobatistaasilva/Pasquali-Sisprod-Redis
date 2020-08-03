using System;
using System.Collections.Generic;
using System.Linq;
using Pasquali.Sisprods.Infra.Data.Contexts;
using Pasquali.Sisprods.Domain.Entities;
using Pasquali.Sisprods.Domain.Repositories;
using Pasquali.Sisprods.Domain.Queries;
using System.Data.Entity;
using StackExchange.Redis;
using Newtonsoft.Json;
using Pasquali.Sisprods.Infra.Data.Cache;

namespace Pasquali.Sisprods.Infra.Data.Repositories
{
    public class ClientCacheRepository : RedisCacheBaseRepository, IClientRepository
    {
        private readonly IDatabase _cacheDB;


        //public ClientCacheRepository()
        public ClientCacheRepository(IDatabase cacheDB)
        {
            _cacheDB = cacheDB;
        }

        public void Create(Client client)
        {
            _cacheDB.StringSet($"Client_{client.ClientId}", JsonConvert.SerializeObject(client));
        } 

        public IEnumerable<Client> GetAll()
        {
            var value = _cacheDB.StringGet("Clients");
            return ((IEnumerable<Client>)(value.HasValue ? JsonConvert.DeserializeObject<IEnumerable<Client>>(value) : default));
        }

        public Client GetById(int id)
        {
            var value = _cacheDB.StringGet($"Client_{id}");
            return (Client)(value.HasValue ? JsonConvert.DeserializeObject<Client>(value) : default);
        }

        public void Update(Client client)
        {
            _cacheDB.StringSet($"Client_{client.ClientId}", JsonConvert.SerializeObject(client, Formatting.Indented,
                                 new JsonSerializerSettings()
                                 {
                                     ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                                 }
                             ));
        }

        public void Delete(Client client)
        {
            _cacheDB.KeyDelete($"Client_{client.ClientId}");
        }

        public Client GetByIdWithProducts(int id)
        {
            throw new NotImplementedException();
        }

        public void Bind<T>(T entities, string named = null)
        {
            SetObjectLoopIgnore<T>(_cacheDB, named ?? typeof(T).Name, entities);
            //What this method above is doing:
            //cache.StringSet(key, JsonConvert.SerializeObject(value, Formatting.Indented,
            //                     new JsonSerializerSettings()
            //                     {
            //                         ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            //                     }
            //                 ));
        }
    }
}
