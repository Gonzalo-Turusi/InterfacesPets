using InterfacePet.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InterfacesPet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogController : ControllerBase
    {
        private readonly IStorage _storage;

        public DogController(IStorage storage)
        {
            _storage = storage;
        }

        [HttpGet("list")]
        public ActionResult<IEnumerable<Dog>> ListDogs()
        {
            var dogs = _storage.Pets.OfType<Dog>().ToList();
            return Ok(dogs);
        }

        [HttpPost("add")]
        public ActionResult AddDog([FromBody] Dog newDog)
        {
            _storage.Pets.Add(newDog);
            return Ok();
        }
    }
}
