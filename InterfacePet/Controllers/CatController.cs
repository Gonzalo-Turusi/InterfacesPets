using InterfacePet.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InterfacesPet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatController : ControllerBase
    {
        private readonly IStorage _storage;

        public CatController(IStorage storage)
        {
            _storage = storage;
        }

        [HttpGet("list")]
        public ActionResult<IEnumerable<Cat>> ListCats()
        {
            var cats = _storage.Pets.OfType<Cat>().ToList();
            return Ok(cats);
        }

        [HttpPost("add")]
        public ActionResult AddCat([FromBody] Cat newCat)
        {
            _storage.Pets.Add(newCat);
            return Ok();
        }
    }
}
