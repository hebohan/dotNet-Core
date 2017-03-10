using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using Core.Cache;
using Core.Business;

namespace Core.Test
{
    [TestClass]
    public class UnitTest1
    {

        private StackExchangeHelper _stackExchangeHelper = new StackExchangeHelper();
        private JsonHelper _jsonHelper = new JsonHelper();

        public class Temp
        {
            //属性
            #region  主键ID
            /// <summary>
            /// 主键ID
            /// </summary>
            public virtual UInt32 ID { get; set; }
            #endregion

            #region  状态 -1：删除，0：正常
            /// <summary>
            /// 状态 -1：删除，0：正常
            /// </summary>
            public virtual Int32 State { get; set; }
            #endregion

            #region  创建时间
            /// <summary>
            /// 创建时间
            /// </summary>
            public virtual DateTime CreateTime { get; set; }
            #endregion

            #region  更新时间
            /// <summary>
            /// 更新时间
            /// </summary>
            public virtual DateTime UpdateTime { get; set; }
            #endregion

            #region 名称
            /// <summary>
            /// 名称
            /// </summary>
            public virtual String Name { get; set; }
            #endregion

            #region 备注
            /// <summary>
            /// 备注
            /// </summary>
            public virtual String Mark { set; get; }
            #endregion
        }

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

        [TestMethod]
        public void TestMethod2()
        {
            var a = _stackExchangeHelper.GetStringKey("StackExchangeHelperTest1");
        }

        [TestMethod]
        public void TestMethod3()
        {
            var temp = new Temp()
            {
                ID = 1,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                State = 1,
                Mark = "sjiaj"
            };

            Object obj = new Object();

            var str = _jsonHelper.SerializeObject(temp);
        }

        [TestMethod]
        public void TestMethod4()
        {
            var str = "{\"ID\":1,\"State\":1,\"CreateTime\":\"2017-03-09T15:01:26.2702134+08:00\",\"UpdateTime\":\"2017-03-09T15:01:26.3152446+08:00\",\"Name\":null,\"Mark\":\"sjiaj\"}";

            var a = _jsonHelper.DeserializeJsonToObject<Temp>(str);
        }
    }
}
