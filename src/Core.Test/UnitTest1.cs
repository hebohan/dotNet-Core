using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;

namespace Core.Test
{
    [TestClass]
    public class UnitTest1
    {
        public IDatabase db { get; set; }

        /// <summary>
        /// Key name of the list in the Redis database.
        /// </summary>
        public static string ListKeyName = "MessageList";

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

        [TestMethod]
        public void TestMethod1()
        {
            db = Connection.GetDatabase();
            if (db.IsConnected(ListKeyName) && (!db.KeyExists(ListKeyName) || !db.KeyType(ListKeyName).Equals(RedisType.List)))
            {
                //Add sample data.
                db.KeyDelete(ListKeyName);
                //Push data from the left
                db.ListLeftPush(ListKeyName, "TestMsg1");
                db.ListLeftPush(ListKeyName, "TestMsg2");
                db.ListLeftPush(ListKeyName, "TestMsg3");
                db.ListLeftPush(ListKeyName, "TestMsg4");
            }

            if (db.IsConnected(ListKeyName))
            {
                //get 5 items from the left
                List<string> messageList = db.ListRange(ListKeyName, 0, 4).Select(o => (string)o).ToList();
            }
        }


    }
}
