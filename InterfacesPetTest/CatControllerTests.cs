using InterfacePet.Interfaces;
using InterfacesPet;
using InterfacesPet.Controllers;
using InterfacesPet.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace InterfacesPetTest
{
    [TestFixture]
    public class CatControllerTests
    {
        private Mock<IStorage> _mockStorage;
        private CatController _controller;
        private List<IPet> _pets;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ListCats_ShouldReturnListOfCats()
        {
            // Arrange
            _mockStorage = new Mock<IStorage>();
            _controller = new CatController(_mockStorage.Object);
            _pets = new List<IPet>();

            var cats = new List<Cat> { new Cat { Name = "Whiskers" } };
            _mockStorage.Setup(s => s.Pets).Returns(cats.Cast<IPet>().ToList());

            // Act
            var actionResult = _controller.ListCats();
            var result = actionResult.Result as OkObjectResult;
            var resultCats = result?.Value as List<Cat>;

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(resultCats, Is.Not.Null);
            Assert.That(resultCats.Count, Is.EqualTo(1));
            Assert.That(resultCats[0].Name, Is.EqualTo("Whiskers"));
        }

        [Test]
        public void ListCats_ShouldReturnEmptyListWhenNoCats()
        {
            // Arrange
            _mockStorage = new Mock<IStorage>();
            _controller = new CatController(_mockStorage.Object);
            _pets = new List<IPet>();

            _mockStorage.Setup(s => s.Pets).Returns(new List<IPet>());

            // Act
            var actionResult = _controller.ListCats();
            var result = actionResult.Result as OkObjectResult;
            var resultCats = result?.Value as List<Cat>;

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(resultCats, Is.Not.Null);
            Assert.That(resultCats.Count, Is.EqualTo(0));
        }

        [Test]
        public void AddCat_ShouldAddCatToStorage()
        {
            // Arrange
            _mockStorage = new Mock<IStorage>();
            _controller = new CatController(_mockStorage.Object);
            _pets = new List<IPet>();

            var newCat = new Cat { Name = "Whiskers", Age = 3, Price = 50 };
            _mockStorage.Setup(s => s.Pets).Returns(_pets);

            // Act
            var result = _controller.AddCat(newCat);

            // Assert
            Assert.That(result, Is.TypeOf<OkResult>());
            Assert.That(_pets, Contains.Item(newCat));
            _mockStorage.Verify(s => s.Pets.Add(newCat), Times.Once);
        }
    }

}
