using CrescentCoffee.Models;
using Microsoft.AspNetCore.Mvc;

namespace CrescentCoffee.Components
{
    public class CoffeeTypeMenu : ViewComponent
    {
        private readonly ICoffeeTypeRepository _coffeeTypeRepository;
        private readonly ICoffeeRepository _coffeeRepository;

        public CoffeeTypeMenu(ICoffeeTypeRepository coffeeTypeRepository, ICoffeeRepository coffeeRepository)
        {
            _coffeeTypeRepository = coffeeTypeRepository;
            _coffeeRepository = coffeeRepository;
        }   

        public IViewComponentResult Invoke()
        {
            List<int> typeIds = new List<int>();
            var coffees = _coffeeRepository.AllCoffees.OrderBy(c => c.CoffeeTypeId);
            foreach (var coffee in coffees)
            {
                if (!typeIds.Contains(coffee.CoffeeTypeId))
                {
                    typeIds.Add(coffee.CoffeeTypeId);
                }
            }

            var coffeeTypes = _coffeeTypeRepository.AllCoffeeTypes
                .Where(c => typeIds.Contains(c.CoffeeTypeId))
                .OrderBy(c => c.Type);
            
            return View(coffeeTypes);
        }
    }
}
