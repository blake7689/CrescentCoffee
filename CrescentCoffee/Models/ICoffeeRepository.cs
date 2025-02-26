namespace CrescentCoffee.Models
{
    public interface ICoffeeRepository
    {
        IEnumerable<Coffee> AllCoffees { get; }
        IEnumerable<Coffee> CoffeesOfTheWeek { get; }
        Coffee? GetCoffeeById(int CoffeeId);
        IEnumerable<Coffee> SearchCoffees(string searchQuery);
    }
}
