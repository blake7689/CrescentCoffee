using Microsoft.EntityFrameworkCore;

namespace CrescentCoffee.Models
{
    public class CoffeeRepository : ICoffeeRepository
    {
        private readonly CrescentCoffeeDbContext _crescentCoffeeDbContext;

        public CoffeeRepository(CrescentCoffeeDbContext crescentCoffeeDbContext)
        {
            _crescentCoffeeDbContext = crescentCoffeeDbContext;
        }

        public IEnumerable<Coffee> AllCoffees
        {
            get
            {
                return _crescentCoffeeDbContext.Coffees.Include(c => c.CoffeeType);
            }
        }

        public IEnumerable<Coffee> CoffeesOfTheWeek
        {
            get
            {
                return _crescentCoffeeDbContext.Coffees.Include(c => c.CoffeeType).Where(c => c.IsCoffeeOfTheWeek);
            }
        }

        public Coffee? GetCoffeeById(int coffeeId)
        {
            return _crescentCoffeeDbContext.Coffees.FirstOrDefault(c => c.CoffeeId.Equals(coffeeId));
        }

        public IEnumerable<Coffee> SearchCoffees(string searchQuery)
        {
            return _crescentCoffeeDbContext.Coffees.Where(c => c.Name.Contains(searchQuery));
        }
    }
}
