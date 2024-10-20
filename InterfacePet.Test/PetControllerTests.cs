using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using InterfacePet.Interfaces;
using InterfacesPet.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using InterfacesPet;
using InterfacesPet.Interfaces;
using System.Text;

namespace InterfacePet.Test
{
    [TestClass]
    public class PetControllerTests
    {
        private Mock<IStorage> _mockStorage = null!;
        private PetController _controller = null!;
        private List<IPet> _pets = null!;

        [TestInitialize]
        public void Setup()
        {
            _pets = new List<IPet>
            {
                new Dog { Name = "Buddy", Age = 3, Price = 200.00m },
                new Dog { Name = "Max", Age = 5, Price = 250.00m },
                new Cat { Name = "Whiskers", Age = 2, Price = 150.00m },
                new Cat { Name = "Shadow", Age = 4, Price = 180.00m },
                new Cat { Name = "Mittens", Age = 1, Price = 120.00m }
            };
            _mockStorage = new Mock<IStorage>();
            _mockStorage.Setup(s => s.Pets).Returns(_pets);
            _controller = new PetController(_mockStorage.Object);
        }

        [TestMethod]
        public void ListPets_ReturnsListOfPets()
        {
            // Act
            var result = _controller.ListPets().Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            var returnedPets = result.Value as IEnumerable<IPet>;
            Assert.IsNotNull(returnedPets);
            Assert.AreEqual(5, returnedPets.Count());
        }

        [TestMethod]
        public void GetTotals_ReturnsCorrectTotals()
        {
            // Act
            var result = _controller.GetTotals().Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            var resultString = result.Value as string;
            Assert.IsNotNull(resultString);

            var expectedString = new StringBuilder()
                .AppendLine("Total Pets: 5")
                .AppendLine("Total Dogs: 2")
                .AppendLine("Total Cats: 3")
                .ToString();

            Assert.AreEqual(expectedString, resultString);
        }

        [TestMethod]
        public void ListPets_ReturnsEmptyListWhenNoPets()
        {
            // Arrange
            _pets.Clear();

            // Act
            var result = _controller.ListPets().Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            var returnedPets = result.Value as IEnumerable<IPet>;
            Assert.IsNotNull(returnedPets);
            Assert.AreEqual(0, returnedPets.Count());
        }

        [TestMethod]
        public void GetTotals_ReturnsZeroWhenNoPets()
        {
            // Arrange
            _pets.Clear();

            // Act
            var result = _controller.GetTotals().Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            var resultString = result.Value as string;
            Assert.IsNotNull(resultString);

            var expectedString = new StringBuilder()
                .AppendLine("Total Pets: 0")
                .AppendLine("Total Dogs: 0")
                .AppendLine("Total Cats: 0")
                .ToString();

            Assert.AreEqual(expectedString, resultString);
        }

        [TestMethod]
        public void ListPets_ReturnsOnlyDogs()
        {
            // Arrange
            _pets.Clear();
            _pets.AddRange(new List<IPet>
            {
                new Dog { Name = "Buddy", Age = 3, Price = 200.00m },
                new Dog { Name = "Max", Age = 5, Price = 250.00m }
            });

            // Act
            var result = _controller.ListPets().Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            var returnedPets = result.Value as IEnumerable<IPet>;
            Assert.IsNotNull(returnedPets);
            Assert.AreEqual(2, returnedPets.Count());
            Assert.IsTrue(returnedPets.All(p => p is Dog));
        }

        [TestMethod]
        public void ListPets_ReturnsOnlyCats()
        {
            // Arrange
            _pets.Clear();
            _pets.AddRange(new List<IPet>
            {
                new Cat { Name = "Whiskers", Age = 2, Price = 150.00m },
                new Cat { Name = "Shadow", Age = 4, Price = 180.00m },
                new Cat { Name = "Mittens", Age = 1, Price = 120.00m }
            });

            // Act
            var result = _controller.ListPets().Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            var returnedPets = result.Value as IEnumerable<IPet>;
            Assert.IsNotNull(returnedPets);
            Assert.AreEqual(3, returnedPets.Count());
            Assert.IsTrue(returnedPets.All(p => p is Cat));
        }
    }
}
