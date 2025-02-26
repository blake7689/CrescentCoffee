using System.IO.Pipelines;
using CrescentCoffee.Models;
using Microsoft.EntityFrameworkCore;

namespace CrescentCoffee.Models
{
    public static class DbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            CrescentCoffeeDbContext context = applicationBuilder.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<CrescentCoffeeDbContext>();

            if (!context.CoffeeTypes.Any())
            {
                context.CoffeeTypes.AddRange(CoffeeTypes.Select(c => c.Value));
            }

            if (!context.Coffees.Any())
            {
                context.AddRange
                (
                    new Coffee
                    {
                        //CoffeeId = 1,
                        Name = "Crescent Beans",
                        Price = 19.99M,
                        ShortDescription = "Signiture Crescent Beans",
                        LongDescription = @"These are our signiture Crescent Coffee Beans. Made here in Alpharetta Georigia, these beans are of the highest qaulity. Dark roasted and as powerfull as they come, these beans are a sure way to kickstart your morning.",
                        Size = "250g",
                        CoffeeType = CoffeeTypes["Arabica"],
                        ImagePath = "Images/coffee1.jpg",
                        InStock = true,
                        IsCoffeeOfTheWeek = false,
                        ImageThumbnailPath = "Images/coffee1.jpg",
                        AllergyInformation = "Although these beans do not contain nuts, they may make you a little nuts."
                    },
                    new Coffee
                    {
                        //CoffeeId = 2,
                        Name = "Moon Beans",
                        Price = 24.99M,
                        ShortDescription = "Far Out Moon Beans",
                        LongDescription = @"These are our Moon Coffee Beans. Made here in Alpharetta Georigia, these beans are of the highest qaulity. Dark roasted and as powerfull as they come, these beans are a sure way to kickstart your morning.",
                        Size = "250g",
                        CoffeeType = CoffeeTypes["Arabica"],
                        ImagePath = "Images/coffee2.jpg",
                        InStock = true,
                        IsCoffeeOfTheWeek = true,
                        ImageThumbnailPath = "/Images/coffee2.jpg",
                        AllergyInformation = "Although these beans do not contain nuts, they may make you a little nuts."
                    },
                    new Coffee
                    {
                        //CoffeeId = 3,
                        Name = "Luna Beans",
                        Price = 24.99M,
                        ShortDescription = "Spacey Luna Beans",
                        LongDescription = @"These are our Luna Coffee Beans. Made here in Alpharetta Georigia, these beans are of the highest qaulity. Dark roasted and as powerfull as they come, these beans are a sure way to kickstart your morning.",
                        Size = "250g",
                        CoffeeType = CoffeeTypes["Robusta"],
                        ImagePath = "Images/coffee3.jpg",
                        InStock = true,
                        IsCoffeeOfTheWeek = true,
                        ImageThumbnailPath = "Images/coffee3.jpg",
                        AllergyInformation = "Although these beans do not contain nuts, they may make you a little nuts."
                    }
                );
            }

            context.SaveChanges();
        }

        private static Dictionary<string, CoffeeType>? coffeeTypes;

        public static Dictionary<string, CoffeeType> CoffeeTypes
        {
            get
            {
                if (coffeeTypes == null)
                {
                    var genresList = new CoffeeType[]
                    {
                        new CoffeeType { Type = "Arabica" },
                        new CoffeeType { Type = "Robusta" },
                        new CoffeeType { Type = "Liberica" },
                        new CoffeeType { Type = "Excelsa" }
                    };

                    coffeeTypes = new Dictionary<string, CoffeeType>();

                    foreach (CoffeeType genre in genresList)
                    {
                        CoffeeTypes.Add(genre.Type, genre);
                    }
                }

                return coffeeTypes;
            }
        }
    }
}
