using CrescentCoffee.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CrescentCoffeeTests.Mocks
{
    public class RepositoryMocks
    {
        public static Mock<ICoffeeRepository> GetCoffeeRepository(int id = 1)
        {
            var coffees = new List<Coffee>
            {
                new Coffee
                {
                    CoffeeId = 1,
                    Name= "Crescent Beans",
                    Price=19.99M,
                    ShortDescription="Signiture Crescent Beans",
                    LongDescription=@"These are our signiture Crescent Coffee Beans. Made here in Alpharetta Georigia, these beans are of the highest qaulity. Dark roasted and as powerfull as they come, these beans are a sure way to kickstart your morning.",
                    Size="250g",
                    CoffeeTypeId = 1,
                    CoffeeType = CoffeeTypes["Arabica"],
                    ImagePath="Images/coffee1.jpg",
                    InStock=true,
                    IsCoffeeOfTheWeek=false,
                    ImageThumbnailPath="Images/coffee1.jpg",
                    AllergyInformation="Although these beans do not contain nuts, they may make you a little nuts."
                },
                new Coffee
                {
                    CoffeeId = 2,
                    Name= "Moon Beans",
                    Price=24.99M,
                    ShortDescription="Far Out Moon Beans",
                    LongDescription=@"These are our Moon Coffee Beans. Made here in Alpharetta Georigia, these beans are of the highest qaulity. Dark roasted and as powerfull as they come, these beans are a sure way to kickstart your morning.",
                    Size="250g",
                    CoffeeTypeId = 1,
                    CoffeeType = CoffeeTypes["Arabica"],
                    ImagePath="Images/coffee2.jpg",
                    InStock=true,
                    IsCoffeeOfTheWeek=true,
                    ImageThumbnailPath="Images/coffee2.jpg",
                    AllergyInformation="Although these beans do not contain nuts, they may make you a little nuts."
                },
                new Coffee
                {
                    CoffeeId = 3,
                    Name= "Luna Beans",
                    Price=24.99M,
                    ShortDescription="Spacey Luna Beans",
                    LongDescription=@"These are our Luna Coffee Beans. Made here in Alpharetta Georigia, these beans are of the highest qaulity. Dark roasted and as powerfull as they come, these beans are a sure way to kickstart your morning.",
                    Size="250g",
                    CoffeeTypeId = 2,
                    CoffeeType = CoffeeTypes["Robusta"],
                    ImagePath="Images/coffee3.jpg",
                    InStock=true,
                    IsCoffeeOfTheWeek=true,
                    ImageThumbnailPath="Images/coffee3.jpg",
                    AllergyInformation="Although these beans do not contain nuts, they may make you a little nuts."
                }
            };

            var mockCoffeeRepository = new Mock<ICoffeeRepository>();
            mockCoffeeRepository.Setup(repo => repo.AllCoffees).Returns(coffees);
            mockCoffeeRepository.Setup(repo => repo.CoffeesOfTheWeek).Returns(coffees.Where(c => c.IsCoffeeOfTheWeek));
            mockCoffeeRepository.Setup(repo => repo.GetCoffeeById(id)).Returns(coffees.FirstOrDefault(c => c.CoffeeId == id, null));
            return mockCoffeeRepository;
        }

        public static Mock<ICoffeeTypeRepository> GetCoffeeTypeRepository()
        {
            var coffeeTypes = new List<CoffeeType>
            {
                new CoffeeType()
                {
                    CoffeeTypeId = 1,
                    Type = "Arabica",
                    Description = "Originating from Ethiopia, Arabica beans are known for their smooth, sweet flavor with hints of chocolate, caramel, and berries"
                },
                new CoffeeType()
                {
                    CoffeeTypeId = 2,
                    Type = "Robusta",
                    Description = "Robusta beans are known for their strong, bitter flavor and nutty, earthy notes"
                },
                new CoffeeType()
                {
                    CoffeeTypeId = 3,
                    Type = "Liberica",
                    Description = "Liberica beans are known for their irregular shape and unique aroma"
                },
                new CoffeeType()
                {
                    CoffeeTypeId = 4,
                    Type = "Excelsa",
                    Description = "Excelsa is a less common coffee bean with a unique flavor profile that can range from tart and fruity to dark and smoky"
                }
            };

            var mockCoffeeTypeRepository = new Mock<ICoffeeTypeRepository>();
            mockCoffeeTypeRepository.Setup(repo => repo.AllCoffeeTypes).Returns(coffeeTypes);

            return mockCoffeeTypeRepository;
        }

        private static Dictionary<string, CoffeeType>? _coffeeType;
        public static Dictionary<string, CoffeeType> CoffeeTypes
        {
            get
            {
                if (_coffeeType == null)
                {
                    var genresList = new CoffeeType[]
                    {
                        new CoffeeType { Type = "Arabica" },
                        new CoffeeType { Type = "Robusta" },
                        new CoffeeType { Type = "Liberica" },
                        new CoffeeType { Type = "Excelsa" }
                    };

                    _coffeeType = new Dictionary<string, CoffeeType>();

                    foreach (var genre in genresList)
                    {
                        _coffeeType.Add(genre.Type, genre);
                    }
                }

                return _coffeeType;
            }
        }

        public static Mock<IShoppingCart> GetShoppingCart()
        {
            //var mockDbContext = new Mock<CrescentCoffeeDbContext>();
            //mockDbContext.Setup<DbSet<ShoppingCartItem>>(x => x.ShoppingCartItems);

            var mockCoffeeRepository = GetCoffeeRepository();
            var coffee = mockCoffeeRepository.Object.AllCoffees.First(c => c.CoffeeId.Equals(1));

            var shoppingCartItems = new List<ShoppingCartItem>
            {
                new ShoppingCartItem
                {
                    ShoppingCartItemId = 1,
                    Coffee = coffee,
                    Amount = 1,
                    ShoppingCartId = "cartid-GUID"
                }
            };

            var mockShoppingCart = new Mock<IShoppingCart>();
            mockShoppingCart.Setup(repo => repo.AddToCart(coffee));
            mockShoppingCart.Setup(repo => repo.RemoveFromCart(coffee)).Returns(true);
            mockShoppingCart.Setup(repo => repo.GetShoppingCartItems()).Returns(shoppingCartItems);
            mockShoppingCart.Setup(repo => repo.ShoppingCartItems).Returns(shoppingCartItems);
            mockShoppingCart.Setup(repo => repo.GetShoppingCartItemCount()).Returns(1);
            mockShoppingCart.Setup(repo => repo.ShoppingCartItemCount).Returns(1);
            mockShoppingCart.Setup(repo => repo.ClearCart());
            mockShoppingCart.Setup(repo => repo.GetShoppingCartTotal()).Returns(19.99M);
            return mockShoppingCart;
        }

        public static Mock<IShoppingCart> GetEmptyShoppingCart()
        {
            var mockCoffeeRepository = GetCoffeeRepository();
            var coffee = mockCoffeeRepository.Object.AllCoffees.First(c => c.CoffeeId.Equals(1));

            var shoppingCartItems = new List<ShoppingCartItem>();

            var mockShoppingCart = new Mock<IShoppingCart>();
            mockShoppingCart.Setup(repo => repo.AddToCart(coffee));
            mockShoppingCart.Setup(repo => repo.RemoveFromCart(coffee)).Returns(true);
            mockShoppingCart.Setup(repo => repo.GetShoppingCartItems()).Returns(shoppingCartItems);
            mockShoppingCart.Setup(repo => repo.ShoppingCartItems).Returns(shoppingCartItems);
            mockShoppingCart.Setup(repo => repo.GetShoppingCartItemCount()).Returns(0);
            mockShoppingCart.Setup(repo => repo.ShoppingCartItemCount).Returns(0);
            mockShoppingCart.Setup(repo => repo.ClearCart());
            mockShoppingCart.Setup(repo => repo.GetShoppingCartTotal()).Returns(0);
            return mockShoppingCart;
        }

        public static Mock<IOrderRepository> GetOrderRepository()
        {
            var mockCoffeeRepository = GetCoffeeRepository();

            var orders = new List<Order>
            {
                new Order
                {
                    OrderId = 1,
                    //OrderDetails = details,
                    FirstName = "Dave",
                    LastName = "Gunn",
                    AddressLine1 = "123 Somewhere Rd NE",
                    AddressLine2 = "",
                    ZipCode = "12345",
                    City = "Wilmingdon",
                    State = "SC",
                    Country = "United States",
                    PhoneNumber = "6783456789",
                    Email = "nope@nope.com",
                    //OrderTotal = 19.99M,
                    //OrderPlaced = DateTime.Now
                }
            };

            var mockOrderRepository = new Mock<IOrderRepository>();
            mockOrderRepository.Setup(repo => repo.CreateOrder(orders[0]));
            return mockOrderRepository;
        }

        private static Dictionary<Coffee, OrderDetail>? _orderDetail;
        public static Dictionary<Coffee, OrderDetail> OrderDetails
        {
            get
            {
                var mockCoffeeRepository = GetCoffeeRepository();

                if (_orderDetail == null)
                {
                    var genresList = new OrderDetail[]
                    {
                        new OrderDetail
                        {
                            OrderDetailId = 1,
                            OrderId = 1,
                            CoffeeId = 1,
                            Amount = 1,
                            Price = 19.99M,
                            Coffee = mockCoffeeRepository.Object.AllCoffees.First(c => c.CoffeeId.Equals(1))
                        }
                    };

                    _orderDetail = new Dictionary<Coffee, OrderDetail>();

                    foreach (var genre in genresList)
                    {
                        _orderDetail.Add(genre.Coffee, genre);
                    }
                }

                return _orderDetail;
            }
        }
    }
}
