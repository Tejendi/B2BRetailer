using System.Collections.Generic;
using System.Linq;
using ProductApi.Models;

namespace ProductApi.Data
{
    public static class DbInitializer
    {
        // This method will create and seed the database.
        public static void Initialize(ProductApiContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Look for any Products
            if (context.Products.Any())
            {
                return;   // DB has been seeded
            }

            List<Product> products = new List<Product>
            {
                new Product { Name = "Hammer", Price = 100, ItemsInStock = 10, Category = "Blunt murder tool", Description = "Good for blunt force trauma. Can last for many murders. Good quality."},
                new Product { Name = "Screwdriver", Price = 70, ItemsInStock = 20, Category = "Pointy murder utility", Description = "Really pointy. Easily penetrates the skin of the victim. Hides away in the sleeve with ease for stealthy murder."},
                new Product { Name = "Drill", Price = 500, ItemsInStock = 2, Category = "Powered massacre machine", Description = "Powered drill for tormenting the victim followed by death. Runs on an easily replacable battery, so you don't have to worry about pesky cables. Additional batteries can be purchased separately."}
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
