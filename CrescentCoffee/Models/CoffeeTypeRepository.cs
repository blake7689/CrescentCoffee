namespace CrescentCoffee.Models
{
    public class CoffeeTypeRepository : ICoffeeTypeRepository
    {
        private readonly CrescentCoffeeDbContext _crescentCoffeeDbContext;

        public CoffeeTypeRepository(CrescentCoffeeDbContext crescentCoffeeDbContext)
        {
            _crescentCoffeeDbContext = crescentCoffeeDbContext;
        }

        public IEnumerable<CoffeeType> AllCoffeeTypes => _crescentCoffeeDbContext.CoffeeTypes.OrderBy(c => c.Type);
    }
}
