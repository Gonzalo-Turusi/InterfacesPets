using InterfacesPet.Interfaces;

namespace InterfacePet.Interfaces
{
    public interface IStorage
    {
        List<IPet> Pets { get; }
    }
}
