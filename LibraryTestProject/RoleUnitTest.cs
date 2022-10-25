using Microsoft.VisualStudio.TestTools.UnitTesting;

using DataAcessClassLibrary;
using System.Collections.Generic;
using DataAccessClassLibrary;

namespace LibraryTestProject
{
    [TestClass]
    public class RoleUnitTest
    {

        [TestMethod]
        public void TestRoleName()
        {
            string actual;
            string expected;
            DARole Role = new DARole();
            actual = Role.GetRole(1).RoleName;
            expected = "Guest";
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestRoleName2()
        {
            string actual;
            string expected;
            DARole Role = new DARole();
            actual = Role.GetRole(2).RoleName;
            expected = "Patron";
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestRoleNamd3()
        {
            string actual;
            string expected;
            DARole Role = new DARole();
            actual = Role.GetRole(3).RoleName;
            expected = "Librarian";
            Assert.AreEqual(expected, actual);
        }



    }
}
