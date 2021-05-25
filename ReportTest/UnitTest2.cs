using System;
using _2._semesterprojekttest.Interfaces;
using _2._semesterprojekttest.Models;
using _2._semesterprojekttest.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReportTest
{
    [TestClass]
    public class UnitTest2
    {
        private CruizeUser user;
        private UserService service;

        [TestInitialize]
        public void BeforeTest()
        {
            service = new UserService();

            user = new CruizeUser
            {
                FirstName = "Carl",
                LastName = "Schwennesen",
                Address = "CarlSchwennesenvej 3",
                Email = "carlschwennesen@edu.easj.dk",
                Password = "1234",
                Zipcode = 2300
            };
        }

        [TestMethod]
        public void TestMethodAddUserValid()
        {
            //Arrange

            //Act
            bool truthBool = service.AddUser(user);

            //Assert
            Assert.IsTrue(truthBool);
        }

        [TestMethod]
        public void TestMethodAddUserInvalid()
        {
            //Arrange
            user.Email = "carlschwennesen@gmail.dk";

            //Act
            bool truthBool = service.AddUser(user);

            //Assert
            Assert.IsFalse(truthBool);
        }

        [TestMethod]
        public void TestMethodDeleteUserValidId()
        {
            //Arrange
            int id = service.GetUserId("carlschwennesen@edu.easj.dk");

            //Act
            bool truthBool = service.DeleteUser(id);

            //Assert
            Assert.IsTrue(truthBool);
        }

        [TestMethod]
        public void TestMethodDeleteUserInvalidId()
        {
            //Arrange
            int id = -1;

            //Act
            bool truthBool = service.DeleteUser(id);

            //Assert
            Assert.IsFalse(truthBool);
        }
    }
}