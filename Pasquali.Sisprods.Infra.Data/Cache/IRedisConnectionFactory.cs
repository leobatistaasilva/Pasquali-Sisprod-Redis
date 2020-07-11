using StackExchange.Redis;

namespace Pasquali.Sisprods.Infra.Data.Cache
{
    public interface IRedisConnectionFactory
    {
        ConnectionMultiplexer Connection();
    }
}
