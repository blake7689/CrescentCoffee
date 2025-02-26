using CrescentCoffee.Models;
using CrescentCoffee.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CrescentCoffee.Components
{
    public class ShoppingCartSummary: ViewComponent
    {
        private readonly IShoppingCart _shoppingCart;

        public ShoppingCartSummary(IShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public IViewComponentResult Invoke()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            int itemCount = _shoppingCart.GetShoppingCartItemCount();
            _shoppingCart.ShoppingCartItemCount = itemCount;

            var shoppingCartViewModel = new ShoppingCartViewModel(_shoppingCart, _shoppingCart.GetShoppingCartTotal());

            return View(shoppingCartViewModel);
        }
    }
}
