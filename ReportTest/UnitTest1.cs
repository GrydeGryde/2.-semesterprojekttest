using System;
using _2._semesterprojekttest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReportTest
{
    [TestClass]
    public class UnitTest1
    {
        private CruizeUser user;

        [TestInitialize]
        public void BeforeTest()
        {
            user = new CruizeUser();
        }

        [TestMethod]
        public void TestMethodValid()
        {
            //Arrange
            int expectedZipCode = 4000;

            //Act
            user.Zipcode = 4000;

            //Assert
            Assert.AreEqual(expectedZipCode, user.Zipcode);
        }
        [TestMethod]
        public void TestMethodUnderBoundary()
        {
            //Arrange

            //Act
            user.Zipcode = 4000;

            //Assert
            Assert.ThrowsException<ArgumentException>(() => user.Zipcode = 999);
        }
        [TestMethod]
        public void TestMethodOnLowerBoundary()
        {
            //Arrange
            int expectedZipCode = 1000;

            //Act
            user.Zipcode = 1000;

            //Assert
            Assert.AreEqual(expectedZipCode, user.Zipcode);
        }
        [TestMethod]
        public void TestMethodOnUpperBoundary()
        {
            //Arrange
            int expectedZipCode = 9999;

            //Act
            user.Zipcode = 9999;

            //Assert
            Assert.AreEqual(expectedZipCode, user.Zipcode);
        }
        [TestMethod]
        public void TestMethodOverBoundary()
        {
            //Arrange

            //Act
            user.Zipcode = 4000;

            //Assert
            Assert.ThrowsException<ArgumentException>(() => user.Zipcode = 10000);
        }
        [TestMethod]
        public void TestMethodNegative()
        {
            //Arrange

            //Act
            user.Zipcode = 4000;

            //Assert
            Assert.ThrowsException<ArgumentException>(() => user.Zipcode = -5);
        }
    }
}
