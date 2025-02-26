namespace CrescentCoffee.Models
{
    public interface IShoppingCart
    {
        void AddToCart(Coffee coffee);
        bool RemoveFromCart(Coffee coffee);
        List<ShoppingCartItem> GetShoppingCartItems();
        int GetShoppingCartItemCount();
        void ClearCart();
        decimal GetShoppingCartTotal();
        List<ShoppingCartItem> ShoppingCartItems { get; set; }
        int ShoppingCartItemCount { get; set; }
    }
}
