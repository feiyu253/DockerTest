using Microsoft.VisualStudio.TestTools.UnitTesting;
using DockerConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockerConsole.Tests
{
    [TestClass()]
    public class CommonHelperTests
    {
        private const string UserName = "Michael2";
        CommonHelper helper = new CommonHelper();
        [TestMethod()]
        public void TestMethodTest()
        {
            
            Assert.AreEqual(UserName,helper.TestMethod());
        }

        [TestMethod()]
        public void GetUserNameTest()
        {
            try
            {
                Assert.AreEqual(UserName, helper.GetUserName(), $"expected:{UserName}, actual:{helper.GetUserName()}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}