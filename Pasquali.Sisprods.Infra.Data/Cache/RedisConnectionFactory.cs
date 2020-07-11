using Microsoft.Extensions.Options;
using Pasquali.Sisprods.Infra.Data.Migrations;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pasquali.Sisprods.Infra.Data.Cache
{
    public class RedisConnectionFactory : IRedisConnectionFactory
    {
        private readonly Lazy<ConnectionMultiplexer> _connection;        
        private readonly IOptions<ConfigurationOptions> _redisConfigOptions;
        private readonly IOptions<RedisConfiguration> redis;

        //public RedisConnectionFactory(IOptions<ConfigurationOptions> redisConfigOptions)
        //{
        //    _redisConfigOptions = redisConfigOptions;
        //    this._connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(_redisConfigOptions.Value));            
        //}        
        
        public RedisConnectionFactory()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["RedisCacheConnection"].ConnectionString;
            this._connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(connectionString));
            
        }
        
        //public RedisConnectionFactory(IOptions<RedisConfiguration> redis)
        //{
        //    this._connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(redis));
        //}

        public ConnectionMultiplexer Connection()
        {
            return this._connection.Value;
        }

    }

    //public class RedisConnectorHelper
    //{
    //    static RedisConnectorHelper()
    //    {
    //        RedisConnectorHelper.lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
    //        {
    //            return ConnectionMultiplexer.Connect("localhost:6379");
    //        });
    //    }

    //    private static Lazy<ConnectionMultiplexer> lazyConnection;

    //    public static ConnectionMultiplexer Connection
    //    {
    //        get
    //        {
    //            return lazyConnection.Value;
    //        }
    //    }

}
