using CrescentCoffee.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrescentCoffee.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ICoffeeRepository _coffeeRepository;

        public SearchController(ICoffeeRepository coffeeRepository)
        {
            _coffeeRepository = coffeeRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var allCoffees = _coffeeRepository.AllCoffees;
            return Ok(allCoffees);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (!_coffeeRepository.AllCoffees.Any(c => c.CoffeeId.Equals(id)))
            {
                return NotFound();
            }

            return Ok(_coffeeRepository.AllCoffees.Where(c => c.CoffeeId.Equals(id)));
        }

        [HttpPost]
        public IActionResult SearchCoffees([FromBody] string searchQuery)
        {
            IEnumerable<Coffee> coffees = new List<Coffee>();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                coffees = _coffeeRepository.SearchCoffees(searchQuery);
            }
            return new JsonResult(coffees);
        }
    }
}
