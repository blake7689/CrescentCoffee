using System.IO.Pipelines;

namespace CrescentCoffee.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int CoffeeId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public Coffee Coffee { get; set; } = default!;
        public Order Order { get; set; } = default!;
    }
}
