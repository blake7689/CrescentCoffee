namespace CrescentCoffee.Models
{
    public class CoffeeType
    {
        public int CoffeeTypeId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string? Description { get; set; }
        public List<Coffee>? Coffees { get; set; }
    }
}
