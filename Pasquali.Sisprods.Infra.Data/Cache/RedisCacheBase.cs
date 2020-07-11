using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pasquali.Sisprods.Infra.Data.Cache
{
    public class RedisCacheBase
    {


        protected void SetObject<T>(IDatabase cache, string key, T value)
        {
            cache.StringSet(key, JsonConvert.SerializeObject(value));
        }

        protected void SetObjectLoopIgnore<T>(IDatabase cache, string key, T value)
        {
            cache.StringSet(key, JsonConvert.SerializeObject(value, Formatting.Indented,
                                 new JsonSerializerSettings()
                                 {
                                     ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                                 }
                             ));
        }


        // get
        protected T GetEntity<T>(IDatabase cache, string key)
        {
            var value = cache.StringGet(key);
            return value.HasValue ? JsonConvert.DeserializeObject<T>(value) : default;
        }
        protected object GetObject(IDatabase cache, string key)
        {
            var value = cache.StringGet(key);
            return value.HasValue ? JsonConvert.DeserializeObject(value) : default;
        }
        // verify if an object exists
        protected bool ExistObject<T>(IDatabase cache, string key)
        {
            var value = cache.StringGet(key);
            return value.HasValue;
        }
        // del
        protected void DelObject(IDatabase cache, string key)
        {
            cache.KeyDelete(key);
        }
    }
}
