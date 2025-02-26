namespace CrescentCoffee.Models
{
    public class MockCoffeeTypeRepository : ICoffeeTypeRepository
    {
        public IEnumerable<CoffeeType> AllCoffeeTypes =>
            new List<CoffeeType>
            {
                new CoffeeType{CoffeeTypeId=1, Type="Arabica", Description="Originating from Ethiopia, Arabica beans are known for their smooth, sweet flavor with hints of chocolate, caramel, and berries." },
                new CoffeeType{CoffeeTypeId=2, Type="Robusta", Description="Robusta beans are known for their strong, bitter flavor and nutty, earthy notes." },
                new CoffeeType{CoffeeTypeId=3, Type="Liberica", Description="Liberica beans are known for their irregular shape and unique aroma." },
                new CoffeeType{CoffeeTypeId=4, Type="Excelsa", Description="Excelsa is a less common coffee bean with a unique flavor profile that can range from tart and fruity to dark and smoky." }
            };
    }
}
