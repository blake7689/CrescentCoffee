using CrescentCoffee.Models;
using CrescentCoffee.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CrescentCoffee.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICoffeeRepository _coffeeRepository;

        public HomeController(ICoffeeRepository coffeeRepository)
        {
            _coffeeRepository = coffeeRepository;
        }

        public IActionResult Index()
        {
            var coffeesOfTheWeek = _coffeeRepository.CoffeesOfTheWeek;
            var homeViewModel = new HomeViewModel(coffeesOfTheWeek);
            return View(homeViewModel);
        }
    }
}
