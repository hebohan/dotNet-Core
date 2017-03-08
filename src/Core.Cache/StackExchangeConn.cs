using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Cache
{
    class StackExchangeConn
    {
        private static ConnectionMultiplexer _connection;
        private static readonly object SyncObject = new object();

        static IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        static IConfigurationRoot configuration = builder.Build();

        public static ConnectionMultiplexer GetFactionConn
        {
            get
            {
                if (_connection == null || !_connection.IsConnected)
                {
                    lock (SyncObject)
                    {
                        var configurationOptions = new ConfigurationOptions()
                        {
                            Password = configuration["RedisConnectionStrings:RedisPassword"],
                            EndPoints = { { configuration["RedisConnectionStrings:RedisIp"], Convert.ToInt32(configuration["RedisConnectionStrings:RedisPort"]) } }
                        };
                        _connection = ConnectionMultiplexer.Connect(configurationOptions);
                    }
                }
                return _connection;
            }
        }
    }
}
