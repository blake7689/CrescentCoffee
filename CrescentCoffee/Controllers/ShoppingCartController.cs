using CrescentCoffee.Models;
using CrescentCoffee.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CrescentCoffee.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ICoffeeRepository _coffeeRepository;
        private readonly IShoppingCart _shoppingCart;

        public ShoppingCartController(ICoffeeRepository coffeeRepository, IShoppingCart shoppingCart)
        {
            _coffeeRepository = coffeeRepository;
            _shoppingCart = shoppingCart;
        }

        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel(_shoppingCart, _shoppingCart.GetShoppingCartTotal());

            return View(shoppingCartViewModel);
        }

        public void AddToShoppingCart(int coffeeId)
        {
            var selectCoffee = _coffeeRepository.AllCoffees.FirstOrDefault(c => c.CoffeeId.Equals(coffeeId));

            if (selectCoffee != null)
            {
                _shoppingCart.AddToCart(selectCoffee);
            }
        }

        public RedirectToActionResult RemoveFromShoppingCart(int coffeeId)
        {
            var selectCoffee = _coffeeRepository.AllCoffees.FirstOrDefault(c => c.CoffeeId.Equals(coffeeId));
            bool isCartEmpty = true;

            if (selectCoffee != null)
            {
                isCartEmpty = _shoppingCart.RemoveFromCart(selectCoffee);
            }

            if (isCartEmpty)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index");
        }

        public RedirectToActionResult ClearShoppingCart()
        {
            _shoppingCart.ClearCart();
            return RedirectToAction("Index", "Home");
        }
    }
}
