using InterfacePet.Interfaces;
using InterfacesPet.Interfaces;

namespace InterfacesPet
{
    public class Storage : IStorage
    {
        public List<IPet> Pets { get; } = new List<IPet>
        {
            new Dog { Name = "Buddy", Age = 3, Price = 200.00m },
            new Dog { Name = "Max", Age = 5, Price = 250.00m },
            new Cat { Name = "Whiskers", Age = 2, Price = 150.00m },
            new Cat { Name = "Shadow", Age = 4, Price = 180.00m },
            new Cat { Name = "Mittens", Age = 1, Price = 120.00m }
        };
    }
}
