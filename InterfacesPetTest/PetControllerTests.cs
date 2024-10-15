using InterfacePet.Interfaces;
using InterfacesPet;
using InterfacesPet.Controllers;
using InterfacesPet.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfacesPetTest
{
    [TestFixture]
    public class PetControllerTests
    {
        private Mock<IStorage> _mockStorage;
        private PetController _controller;
        private List<IPet> _pets;

        [SetUp]
        public void Setup()
        {
            _mockStorage = new Mock<IStorage>();
            _controller = new PetController(_mockStorage.Object);
            _pets = new List<IPet>();
        }

        [Test]
        public void ListPets_ShouldReturnListOfPets()
        {
            // Arrange
            var pets = new List<IPet> { new Dog { Name = "Buddy" }, new Cat { Name = "Whiskers" } };
            _mockStorage.Setup(s => s.Pets).Returns(pets);

            // Act
            var actionResult = _controller.ListPets();
            var result = actionResult.Result as OkObjectResult;
            var resultPets = result?.Value as List<IPet>;

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(resultPets, Is.Not.Null);
            Assert.That(resultPets.Count, Is.EqualTo(2));
            Assert.That(resultPets[0].Name, Is.EqualTo("Buddy"));
            Assert.That(resultPets[1].Name, Is.EqualTo("Whiskers"));
        }

        [Test]
        public void ListPets_ShouldReturnEmptyListWhenNoPets()
        {
            // Arrange
            _mockStorage.Setup(s => s.Pets).Returns(new List<IPet>());

            // Act
            var actionResult = _controller.ListPets();
            var result = actionResult.Result as OkObjectResult;
            var resultPets = result?.Value as List<IPet>;

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(resultPets, Is.Not.Null);
            Assert.That(resultPets.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetTotals_ShouldReturnCorrectTotals()
        {
            // Arrange
            var pets = new List<IPet> { new Dog { Name = "Buddy" }, new Cat { Name = "Whiskers" }, new Dog { Name = "Max" } };
            _mockStorage.Setup(s => s.Pets).Returns(pets);

            // Act
            var actionResult = _controller.GetTotals();
            var result = actionResult.Result as OkObjectResult;
            var resultString = result?.Value as string;

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(resultString, Is.Not.Null);
            Assert.That(resultString, Contains.Substring("Total Pets: 3"));
            Assert.That(resultString, Contains.Substring("Total Dogs: 2"));
            Assert.That(resultString, Contains.Substring("Total Cats: 1"));
        }

        [Test]
        public void GetTotals_ShouldReturnZeroWhenNoPets()
        {
            // Arrange
            _mockStorage.Setup(s => s.Pets).Returns(new List<IPet>());

            // Act
            var actionResult = _controller.GetTotals();
            var result = actionResult.Result as OkObjectResult;
            var resultString = result?.Value as string;

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(resultString, Is.Not.Null);
            Assert.That(resultString, Contains.Substring("Total Pets: 0"));
            Assert.That(resultString, Contains.Substring("Total Dogs: 0"));
            Assert.That(resultString, Contains.Substring("Total Cats: 0"));
        }
    }
}
