namespace CrescentCoffee.Models
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }
        public Coffee Coffee { get; set; } = default!;
        public int  Amount { get; set; }
        public string? ShoppingCartId { get; set; }
    }
}
