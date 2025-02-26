using CrescentCoffee.Models;

namespace CrescentCoffee.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Coffee> CoffeesOfTheWeek{ get; }

        public HomeViewModel(IEnumerable<Coffee> coffeesOfTheWeek)
        {
            CoffeesOfTheWeek = coffeesOfTheWeek;
        }
    }
}
