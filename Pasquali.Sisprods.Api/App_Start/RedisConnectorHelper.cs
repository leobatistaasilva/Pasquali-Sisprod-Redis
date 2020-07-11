using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Pasquali.Sisprods.Api
{
    public class RedisConnectorHelper
    {
        static RedisConnectorHelper()
        {
            RedisConnectorHelper.lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect("localhost:6379");
            });
        }

        private static Lazy<ConnectionMultiplexer> lazyConnection;

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }

    public static class RedisCache
    {
        // save 
        public static async Task SetObjectAsync<T>(IDatabase cache, string key, T value)
        {
            await cache.StringSetAsync(key, JsonConvert.SerializeObject(value));
        }
        // get
        public static async Task<T> GetObjectAsync<T>(IDatabase cache, string key)
        {
            var value = await cache.StringGetAsync(key);
            return value.HasValue ? JsonConvert.DeserializeObject<T>(value) : default ;
        }
        // verify if an object exists
        public static async Task<bool> ExistObjectAsync<T>(IDatabase cache, string key)
        {
            var value = await cache.StringGetAsync(key); 
            return value.HasValue;
        }


        public static void SetObject<T>(IDatabase cache, string key, T value)
        {
           cache.StringSet(key, JsonConvert.SerializeObject(value));
        }

        public static void SetObjectLoopIgnore<T>(IDatabase cache, string key, T value)
        {
           cache.StringSet(key, JsonConvert.SerializeObject(value, Formatting.Indented,
                                new JsonSerializerSettings()
                                {
                                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                                }
                            ));
        }


        // get
        public static T GetEntity<T>(IDatabase cache, string key)
        {
            var value = cache.StringGet(key);
            return value.HasValue ? JsonConvert.DeserializeObject<T>(value) : default;
        }
        public static object GetObject(IDatabase cache, string key)
        {
            var value = cache.StringGet(key);
            return value.HasValue ? JsonConvert.DeserializeObject(value) : default;
        }
        // verify if an object exists
        public static bool ExistObject<T>(IDatabase cache, string key)
        {
            var value = cache.StringGet(key);
            return value.HasValue;
        }
        // del
        public static void DelObject(IDatabase cache, string key)
        {
            cache.KeyDelete(key);
        }
    }
}