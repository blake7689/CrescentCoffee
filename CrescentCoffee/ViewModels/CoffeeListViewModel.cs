using CrescentCoffee.Models;

namespace CrescentCoffee.ViewModels
{
    public class CoffeeListViewModel
    {
        public IEnumerable<Coffee> Coffees { get; }
        public string? CurrentCoffeeType { get; }

        public CoffeeListViewModel(IEnumerable<Coffee> coffees, string? currentCoffeeType)
        {
            Coffees = coffees;
            CurrentCoffeeType = currentCoffeeType;
        }
    }
}
