namespace CrescentCoffee.Models
{
    public interface ICoffeeTypeRepository
    {
        IEnumerable<CoffeeType> AllCoffeeTypes { get; }
    }
}
