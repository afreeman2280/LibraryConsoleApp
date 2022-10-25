using Microsoft.VisualStudio.TestTools.UnitTesting;

using DataAcessClassLibrary;
using System.Collections.Generic;

namespace LibraryTestProject
{
    [TestClass]
    public class BookUnitTest
    {

        [TestMethod]
        public void TestBookName()
        {
            string actual;
            string expected;
            DABook Book = new DABook();
            actual = Book.GetBook(1).BookName;
            expected = "The Giver";
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestAuthor()
        {
            string actual;
            string expected;
            DABook Book = new DABook();
            actual = Book.GetBook(3).Author;
            expected = "Hero";
            Assert.AreEqual(expected, actual);
        }

    

    }
}
