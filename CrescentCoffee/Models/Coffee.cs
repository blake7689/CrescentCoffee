namespace CrescentCoffee.Models
{
    public class Coffee
    {
        public int CoffeeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Size { get; set; }
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }
        public string? AllergyInformation { get; set; }
        public decimal Price { get; set; }
        public string? ImagePath { get; set; }
        public string? ImageThumbnailPath { get; set; }
        public bool IsCoffeeOfTheWeek { get; set; }
        public bool InStock { get; set; }
        public int CoffeeTypeId { get; set; }
        public CoffeeType CoffeeType { get; set; } = default!;
    }
}
