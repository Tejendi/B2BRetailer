using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Data
{
    public class DbInitializer
    {
        // This method will create and seed the database.
        public static void Initialize(CustomerApiContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            //// Look for any Products
            //if (context.Orders.Any())
            //{
            //    return;   // DB has been seeded
            //}

            //List<Order> orders = new List<Order>
            //{
            //    new Order { Date = DateTime.Today, ProductId = 1, Quantity = 2 }
            //};

            //context.Orders.AddRange(orders);
            context.SaveChanges();
        }
    }
}
