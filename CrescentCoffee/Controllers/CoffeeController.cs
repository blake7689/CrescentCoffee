using CrescentCoffee.Models;
using CrescentCoffee.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Dynamic;

namespace CrescentCoffee.Controllers
{
    public class CoffeeController : Controller
    {
        private readonly ICoffeeRepository _coffeeRepository;
        private readonly ICoffeeTypeRepository _coffeeTypeRepository;

        public CoffeeController
            (ICoffeeRepository coffeeRepository, ICoffeeTypeRepository coffeeTypeRepository)
        {
            _coffeeRepository = coffeeRepository;
            _coffeeTypeRepository = coffeeTypeRepository;
        }

        public ViewResult CoffeeList(string coffeeType)
        {
            IEnumerable<Coffee> coffees;
            string? currentCoffeeType;

            if (string.IsNullOrEmpty(coffeeType) || 
                (_coffeeRepository.AllCoffees.Where(c => c.CoffeeType.Type.Equals(coffeeType)).IsNullOrEmpty() && !coffeeType.IsNullOrEmpty()))
            {
                coffees = _coffeeRepository.AllCoffees.OrderBy(c => c.CoffeeId);
                currentCoffeeType = "All Coffees";
            }
            else
            {
                coffees = _coffeeRepository.AllCoffees.Where(c => c.CoffeeType.Type.Equals(coffeeType)).OrderBy(c => c.CoffeeId);
                currentCoffeeType = _coffeeTypeRepository.AllCoffeeTypes.FirstOrDefault(c => c.Type.Equals(coffeeType))?.Type;
            }

            return View(new CoffeeListViewModel(coffees, currentCoffeeType));
        }

        public IActionResult Details(int id)
        {
            dynamic mymodel = new ExpandoObject();

            var coffee = _coffeeRepository.GetCoffeeById(id);
            if (coffee == null)
            {
                return NotFound();
            }

            mymodel.Coffee = coffee;
            mymodel.CoffeeType = _coffeeTypeRepository.AllCoffeeTypes.FirstOrDefault(t => t.CoffeeTypeId.Equals(coffee.CoffeeTypeId));

            var test = mymodel.Coffee;

            return View(mymodel);
        }

        public IActionResult Search()
        {
            return View();
        }
    }
}
