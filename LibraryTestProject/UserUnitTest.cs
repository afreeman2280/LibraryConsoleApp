using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogicClassLibrary;
namespace LibraryTestProject
{
    [TestClass]
    public class UserUnitTest
    {
      
        [TestMethod]
        public void TestUserExist()
        {
            bool actual = false;
            bool expected = false;
            BLLUser user = new BLLUser();
            actual = user.userExist(1);
            Assert.AreEqual(expected, actual);  
        }
        [TestMethod]
        public void TestLogin()
        {
            bool actual = false;
            bool expected = false;
            BLLUser login = new BLLUser();
            actual = login.Login("Antoine", "Password");
            Assert.AreEqual(expected , actual); 
        }
        [TestMethod]
        public void TestUserGetRole()
        {
            bool actual = false;
            Roles expected = Roles.guest;
            BLLUser login = new BLLUser();
            Roles roles = login.getUserRole("Antoine", "Password");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestGetUser()
        {
            BLLUser actual;
            BLLUser expected = new BLLUser(0,"Ant",Roles.guest,"pass");
            BLLUser login = new BLLUser();
            actual = login.getUser(1);
            Assert.AreEqual(expected, actual);
        }
    }
}
