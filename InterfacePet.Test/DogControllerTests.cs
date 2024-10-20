using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using InterfacePet.Interfaces;
using InterfacesPet.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using InterfacesPet;
using InterfacesPet.Interfaces;

namespace InterfacePet.Test
{
    [TestClass]
    public class DogControllerTests
    {
        private Mock<IStorage> _mockStorage = null!;
        private DogController _controller = null!;
        private List<IPet> _pets = null!;

        [TestInitialize]
        public void Setup()
        {
            _pets = new List<IPet>();
            _mockStorage = new Mock<IStorage>();
            _mockStorage.Setup(s => s.Pets).Returns(_pets);
            _controller = new DogController(_mockStorage.Object);
        }

        [TestMethod]
        public void ListDogs_ReturnsListOfDogs()
        {
            // Arrange
            var dogs = new List<IPet> { new Dog { Name = "Buddy" }, new Dog { Name = "Max" } };
            _pets.AddRange(dogs);

            // Act
            var result = _controller.ListDogs().Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            var returnedDogs = result.Value as IEnumerable<Dog>;
            Assert.IsNotNull(returnedDogs);
            Assert.AreEqual(2, returnedDogs.Count());
            Assert.AreEqual("Buddy", returnedDogs.First().Name);
        }

        [TestMethod]
        public void ListDogs_ReturnsEmptyListWhenNoDogs()
        {
            // Act
            var result = _controller.ListDogs().Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            var returnedDogs = result.Value as IEnumerable<Dog>;
            Assert.IsNotNull(returnedDogs);
            Assert.AreEqual(0, returnedDogs.Count());
        }

        [TestMethod]
        public void AddDog_AddsDogAndReturnsOk()
        {
            // Arrange
            var newDog = new Dog { Name = "Rex" };

            // Act
            var result = _controller.AddDog(newDog) as OkResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, _pets.Count);
            Assert.AreEqual("Rex", _pets.First().Name);
        }

        [TestMethod]
        public void AddDog_ReturnsBadRequestWhenDogIsNull()
        {
            // Act
            var result = _controller.AddDog(null) as BadRequestResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
