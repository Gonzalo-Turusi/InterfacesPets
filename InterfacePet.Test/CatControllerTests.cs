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
    public class CatControllerTests
    {
        private Mock<IStorage> _mockStorage = null!;
        private CatController _controller = null!;
        private List<IPet> _pets = null!;

        [TestInitialize]
        public void Setup()
        {
            _pets = new List<IPet>();
            _mockStorage = new Mock<IStorage>();
            _mockStorage.Setup(s => s.Pets).Returns(_pets);
            _controller = new CatController(_mockStorage.Object);
        }

        [TestMethod]
        public void ListCats_ReturnsListOfCats()
        {
            // Arrange
            var cats = new List<IPet> { new Cat { Name = "Whiskers" }, new Cat { Name = "Tom" } };
            _pets.AddRange(cats);

            // Act
            var result = _controller.ListCats().Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            var returnedCats = result.Value as IEnumerable<Cat>;
            Assert.IsNotNull(returnedCats);
            Assert.AreEqual(2, returnedCats.Count());
            Assert.AreEqual("Whiskers", returnedCats.First().Name);
        }

        [TestMethod]
        public void ListCats_ReturnsEmptyListWhenNoCats()
        {
            // Act
            var result = _controller.ListCats().Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            var returnedCats = result.Value as IEnumerable<Cat>;
            Assert.IsNotNull(returnedCats);
            Assert.AreEqual(0, returnedCats.Count());
        }

        [TestMethod]
        public void AddCat_AddsCatAndReturnsOk()
        {
            // Arrange
            var newCat = new Cat { Name = "Garfield" };

            // Act
            var result = _controller.AddCat(newCat) as OkResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, _pets.Count);
            Assert.AreEqual("Garfield", _pets.First().Name);
        }

        [TestMethod]
        public void AddCat_ReturnsBadRequestWhenCatIsNull()
        {
            // Act
            var result = _controller.AddCat(null) as BadRequestResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
