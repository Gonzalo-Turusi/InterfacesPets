using InterfacesPet.Interfaces;

namespace InterfacesPet
{
    public class Dog : IPet
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Price { get; set; }
    }
}
