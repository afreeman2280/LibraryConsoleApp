using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAcessClassLibrary;

namespace LibraryTestProject
{
    [TestClass]
    public class UserUnitTest
    {
      
        [TestMethod]
        public void TestUserName()
        {
            string actual;
            string expected;
            DAUser user = new DAUser();
            actual = user.GetUser(1).UserName;
            expected = "Antoine";
            Assert.AreEqual(expected, actual);  
        }
        [TestMethod]
        public void TestPassword()
        {
            string actual;
            string expected;
            DAUser user = new DAUser();
            actual = user.GetUser(3).password;
            expected = "Hero";
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestRole()
        {
            int actual;
            int expected;
            DAUser user = new DAUser();
            actual = user.GetUser(3).Role;
            expected = 3;
            Assert.AreEqual(expected, actual);
        }
        public void TestCount()
        {
            int actual;
            int expected;
            DAUser user = new DAUser();
            actual = user.GetAllUser().Count;
            expected = 3;
            Assert.AreEqual(expected, actual);
        }

      

    }
}
