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
    public class ProductCacheRepository : RedisCacheBase, IProductRepository
    {
        private readonly IDatabase _cacheDB;

        //public ProductCacheRepository(IDatabase cacheDB)
        public ProductCacheRepository()
        {
            //_cacheDB = cacheDB;
        }

        public void Create(Product product)
        {
            _cacheDB.StringSet($"Product_{product.ProductId}", JsonConvert.SerializeObject(product));
        } 

        public IEnumerable<Product> GetAll()
        {
            var value = _cacheDB.StringGet("Products");
            return ((IEnumerable<Product>)(value.HasValue ? JsonConvert.DeserializeObject(value) : default));
        }

        public Product GetById(int id)
        {
            var value = _cacheDB.StringGet($"Product_{id}");
            return (Product)(value.HasValue ? JsonConvert.DeserializeObject(value) : default);
        }

        public void Update(Product product)
        {
            _cacheDB.StringSet($"Product_{product.ProductId}", JsonConvert.SerializeObject(product, Formatting.Indented,
                                 new JsonSerializerSettings()
                                 {
                                     ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                                 }
                             ));
        }

        public void Delete(Product product)
        {
            _cacheDB.KeyDelete($"Product_{product.ProductId}");
        }


    }
}
