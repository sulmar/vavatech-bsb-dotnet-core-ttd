using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Fundamentals.MSUnitTests
{
    [TestClass]
    public class RentTests
    {
        private Rent rent;
        private User rentee;

        [TestInitialize]
        public void Setup()
        {
            rent = new Rent();
            rentee = new User();
            rent.Rentee = rentee;
        }

        // jestesmy adminem i mozemy zwrocic

        [TestMethod]
        public void CanReturn_UserIsAdmin_ReturnsTrue()
        {
            // Arrange
            User admin = new User { IsAdmin = true };

            // Act
            var result = rent.CanReturn(admin);

            // Assert
            Assert.IsTrue(result);
        }

        // jestem wypozyczajacym i mozemy zwrocic
        [TestMethod]
        public void CanReturn_UserIsRentee_ReturnsTrue()
        {
            // Arrange

            // Act
            var result = rent.CanReturn(rentee);

            // Assert
            Assert.IsTrue(result);
            
        }

        // uzytkownik nie jest wypozyczającym i nie mozemy zwrocic
        [TestMethod]
        public void CanReturn_UserIsNotRentee_ReturnsFalse()
        { 
            // Arrange

            // Act
            var result = rent.CanReturn(new User());

            // Assert
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void CanReturn_UserIsEmpty_ThrowsArgumentNullException()
        {
            // Arrange
            User empty = null;

            // Act
            Action act = () => rent.CanReturn(empty);

            // Assert
            Assert.ThrowsException<ArgumentNullException>(act);
        }

    }
}
