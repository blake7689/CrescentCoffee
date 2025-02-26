using Microsoft.EntityFrameworkCore;
using System.IO.Pipelines;

namespace CrescentCoffee.Models
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly CrescentCoffeeDbContext _crescentCoffeeDbContext;

        public string? ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;

        int IShoppingCart.ShoppingCartItemCount { get; set; }

        private ShoppingCart(CrescentCoffeeDbContext crescentCoffeeDbContext)
        {
            _crescentCoffeeDbContext = crescentCoffeeDbContext;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

            CrescentCoffeeDbContext context = services.GetService<CrescentCoffeeDbContext>() ?? throw new Exception("Error initializing");

            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();

            session?.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Coffee coffee)
        {
            var shoppingCartItem =
                    _crescentCoffeeDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Coffee.CoffeeId == coffee.CoffeeId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Coffee = coffee,
                    Amount = 1
                };

                _crescentCoffeeDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _crescentCoffeeDbContext.SaveChanges();
        }

        public bool RemoveFromCart(Coffee coffee)
        {
            var shoppingCartItem =
                    _crescentCoffeeDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Coffee.CoffeeId == coffee.CoffeeId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _crescentCoffeeDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _crescentCoffeeDbContext.SaveChanges();

            var isCartEmpty = !_crescentCoffeeDbContext.ShoppingCartItems.Any(s => s.ShoppingCartId == ShoppingCartId);

            return isCartEmpty;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??=
                       _crescentCoffeeDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                           .Include(s => s.Coffee)
                           .ToList();
        }

        public int GetShoppingCartItemCount()
        {
            int cartCount = 0;
            List<ShoppingCartItem> ShoppingCartItems =
                _crescentCoffeeDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                    .Include(s => s.Coffee)
                    .ToList();

            foreach (ShoppingCartItem item in ShoppingCartItems)
            {
                cartCount += item.Amount;
            }

            return cartCount;
        }

        public void ClearCart()
        {
            var cartItems = _crescentCoffeeDbContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _crescentCoffeeDbContext.ShoppingCartItems.RemoveRange(cartItems);

            _crescentCoffeeDbContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _crescentCoffeeDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Coffee.Price * c.Amount).Sum();
            return total;
        }
    }
}
