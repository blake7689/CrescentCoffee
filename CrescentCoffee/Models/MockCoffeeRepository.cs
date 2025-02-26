namespace CrescentCoffee.Models
{
    public class MockCoffeeRepository : ICoffeeRepository
    {
        private readonly ICoffeeTypeRepository _coffeeTypeRepository = new MockCoffeeTypeRepository();

        public IEnumerable<Coffee> AllCoffees =>
            new List<Coffee>
            {
                new Coffee {
                    CoffeeId = 1, 
                    Name= "Crescent Beans", 
                    Price=19.99M, 
                    ShortDescription="Signiture Crescent Beans",
                    LongDescription=@"These are our signiture Crescent Coffee Beans. Made here in Alpharetta Georigia, these beans are of the highest qaulity. Dark roasted and as powerfull as they come, these beans are a sure way to kickstart your morning.", 
                    Size="250g", 
                    CoffeeType = _coffeeTypeRepository.AllCoffeeTypes.ToList()[0], 
                    ImagePath="Images/carousel1.jpg", 
                    InStock=true, 
                    IsCoffeeOfTheWeek=false,
                    ImageThumbnailPath="Images/carousel1.jpg",
                    AllergyInformation="Although these beans do not contain nuts, they may make you a little nuts."},
                new Coffee {
                    CoffeeId = 2,
                    Name= "Moon Beans",
                    Price=24.99M,
                    ShortDescription="Far Out Moon Beans",
                    LongDescription=@"These are our Moon Coffee Beans. Made here in Alpharetta Georigia, these beans are of the highest qaulity. Dark roasted and as powerfull as they come, these beans are a sure way to kickstart your morning.",
                    Size="250g",
                    CoffeeType = _coffeeTypeRepository.AllCoffeeTypes.ToList()[0],
                    ImagePath="Images/carousel2.jpg",
                    InStock=true,
                    IsCoffeeOfTheWeek=true,
                    ImageThumbnailPath="Images/carousel2.jpg",
                    AllergyInformation="Although these beans do not contain nuts, they may make you a little nuts."},
                new Coffee {
                    CoffeeId = 3,
                    Name= "Luna Beans",
                    Price=24.99M,
                    ShortDescription="Spacey Luna Beans",
                    LongDescription=@"These are our Luna Coffee Beans. Made here in Alpharetta Georigia, these beans are of the highest qaulity. Dark roasted and as powerfull as they come, these beans are a sure way to kickstart your morning.",
                    Size="250g",
                    CoffeeType = _coffeeTypeRepository.AllCoffeeTypes.ToList()[1],
                    ImagePath="Images/carousel3.jpg",
                    InStock=true,
                    IsCoffeeOfTheWeek=true,
                    ImageThumbnailPath="Images/carousel3.jpg",
                    AllergyInformation="Although these beans do not contain nuts, they may make you a little nuts."}
            };

        public IEnumerable<Coffee> CoffeesOfTheWeek
        {
            get
            {
                return AllCoffees.Where(c => c.IsCoffeeOfTheWeek);
            }
        }

        public Coffee? GetCoffeeById(int coffeeId) => AllCoffees.FirstOrDefault(c => c.CoffeeId == coffeeId);
        
        public IEnumerable<Coffee> SearchCoffees(string searchQuery)
        {
            throw new NotImplementedException();
        }
    }
}
