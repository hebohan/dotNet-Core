using StackExchange.Redis;
using System;

namespace Core.Cache
{
    public class BaseRedisHelper
    {
        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect("127.0.0.1:6379,password=southinfo@2012,abortConnect=false");
        });

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}
