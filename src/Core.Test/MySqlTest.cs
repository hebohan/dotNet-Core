using MySql.Data.MySqlClient;
using Dapper;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.Extensions.Configuration;

namespace Core.Test
{
    [TestClass]
    public class MySqlTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            MySqlConnection con = new MySqlConnection("server=192.168.1.8;port=33060;database=open;uid=dev2;pwd=dev2@20160101;charset='gbk';SslMode=None");

            //查询数据
            var list = con.Query<ServiceTemp>("select * from service_temp");
        }
    }    
}
