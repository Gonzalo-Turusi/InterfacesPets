using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Moq;
using InterfacesPet.Controllers;
using InterfacePet.Interfaces;
using InterfacesPet;
using InterfacesPet.Interfaces;

namespace InterfacesPetTest
{
    [TestFixture]
    public class DogControllerTests
    {
        private Mock<IStorage> _mockStorage;
        private DogController _controller;
        private List<IPet> _pets;

        [SetUp]
        public void Setup()
        {
            _pets = new List<IPet>();
        }

        [Test]
        public void ListDogs_ShouldReturnOnlyDogs_WhenStorageContainsDogsAndCats()
        {
            // Arrange
            _mockStorage = new Mock<IStorage>();
            _controller = new DogController(_mockStorage.Object);
            var dog1 = new Dog { Name = "Buddy" };
            var dog2 = new Dog { Name = "Max" };
            var cat1 = new Cat { Name = "Whiskers" };
            _pets.Add(dog1);
            _pets.Add(dog2);
            _pets.Add(cat1);
            _mockStorage.Setup(s => s.Pets).Returns(_pets);

            // Act
            var actionResult = _controller.ListDogs();
            var result = actionResult.Result as OkObjectResult;
            var resultDogs = result?.Value as List<Dog>;

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(resultDogs, Is.Not.Null);
            Assert.That(resultDogs.Count, Is.EqualTo(2));
            Assert.That(resultDogs, Contains.Item(dog1));
            Assert.That(resultDogs, Contains.Item(dog2));
        }

        [Test]
        public void ListDogs_ShouldReturnEmptyList_WhenStorageContainsOnlyCats()
        {
            // Arrange
            _mockStorage = new Mock<IStorage>();
            _controller = new DogController(_mockStorage.Object);
            var cat1 = new Cat { Name = "Whiskers" };
            var cat2 = new Cat { Name = "Mittens" };
            _pets.Add(cat1);
            _pets.Add(cat2);
            _mockStorage.Setup(s => s.Pets).Returns(_pets);

            // Act
            var actionResult = _controller.ListDogs();
            var result = actionResult.Result as OkObjectResult;
            var resultDogs = result?.Value as List<Dog>;

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(resultDogs, Is.Not.Null);
            Assert.That(resultDogs.Count, Is.EqualTo(0));
        }

        [Test]
        public void ListDogs_ShouldReturnEmptyList_WhenStorageIsEmpty()
        {
            // Arrange
            _mockStorage = new Mock<IStorage>();
            _controller = new DogController(_mockStorage.Object);
            _mockStorage.Setup(s => s.Pets).Returns(new List<IPet>());

            // Act
            var actionResult = _controller.ListDogs();
            var result = actionResult.Result as OkObjectResult;
            var resultDogs = result?.Value as List<Dog>;

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(resultDogs, Is.Not.Null);
            Assert.That(resultDogs.Count, Is.EqualTo(0));
        }
    }
}
