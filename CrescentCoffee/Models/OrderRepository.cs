namespace CrescentCoffee.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CrescentCoffeeDbContext _crescentCoffeeDbContext;
        private readonly IShoppingCart _shoppingCart;

        public OrderRepository(CrescentCoffeeDbContext crescentCoffeeDbContext, IShoppingCart shoppingCart)
        {
            _crescentCoffeeDbContext = crescentCoffeeDbContext;
            _shoppingCart = shoppingCart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            List<ShoppingCartItem>? shoppingCartItems = _shoppingCart.ShoppingCartItems;
            order.OrderTotal = _shoppingCart.GetShoppingCartTotal();

            order.OrderDetails = new List<OrderDetail>();

            foreach (ShoppingCartItem? shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = shoppingCartItem.Amount,
                    CoffeeId = shoppingCartItem.Coffee.CoffeeId,
                    Price = shoppingCartItem.Coffee.Price
                };

                order.OrderDetails.Add(orderDetail);
            }

            _crescentCoffeeDbContext.Orders.Add(order);

            _crescentCoffeeDbContext.SaveChanges();
        }
    }
}
