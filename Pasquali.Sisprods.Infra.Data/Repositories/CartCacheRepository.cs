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
    public class CartCacheRepository : RedisCacheBaseRepository, ICartRepository
    {
        private readonly IDatabase _cacheDB;

        //public CartCacheRepository()

        public CartCacheRepository(IDatabase cacheDB)
        {
            _cacheDB = cacheDB;
        }

        public void Create(Cart Cart)
        {
            SetObjectLoopIgnore(_cacheDB, $"Cart_{Cart.Id}", Cart);
        } 

        public IEnumerable<Cart> GetAll()
        {
            var values = GetObject(_cacheDB, "Carts");
            return ((IEnumerable<Cart>)(values != null ? JsonConvert.DeserializeObject<IEnumerable<Cart>>(values.ToString()) : default));
        }

        public Cart GetById(int id)
        {            
            var value = GetObject(_cacheDB, $"Cart_{id}");
            return (Cart)(value != null ? JsonConvert.DeserializeObject<Cart>((string)value) : default);
        }

        public void Update(Cart Cart)
        {
            SetObjectLoopIgnore(_cacheDB, $"Cart_{Cart.Id}", Cart);
        }

        public void Delete(Cart Cart)
        {
            DelObject(_cacheDB, $"Cart_{Cart.Id}");
        }

        public Cart GetByIdWithProducts(int id)
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
