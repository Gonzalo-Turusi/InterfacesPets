using InterfacePet.Interfaces;
using InterfacesPet.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace InterfacesPet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IStorage _storage;

        public PetController(IStorage storage)
        {
            _storage = storage;
        }

        [HttpGet("list")]
        public ActionResult<IEnumerable<IPet>> ListPets()
        {
            return Ok(_storage.Pets);
        }

        [HttpGet("totals")]
        public ActionResult<string> GetTotals()
        {
            int totalPets = _storage.Pets.Count;
            int totalDogs = _storage.Pets.OfType<Dog>().Count();
            int totalCats = _storage.Pets.OfType<Cat>().Count();

            StringBuilder result = new StringBuilder();
            result.AppendLine($"Total Pets: {totalPets}");
            result.AppendLine($"Total Dogs: {totalDogs}");
            result.AppendLine($"Total Cats: {totalCats}");

            return Ok(result.ToString());
        }
    }
}
