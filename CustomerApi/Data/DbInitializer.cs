using CustomerApi.Models;
using System.Collections.Generic;

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

            List<Customer> orders = new List<Customer>
            {
                new Customer
                {
                    CompanyName = "Cardboard Murders Inc.",
                    Email = "sales@cardboardmurders.com",
                    ShippingAddress = new Address()
                    {
                        City = "Nowhere",
                        StreetAddress = "Cardboard box at the corner of 53rd and 22nd",
                        ZipCode = "????"
                    },
                    Phone = "Payphone at 53rd st",
                    BillingAddress = new Address()
                    {
                        City = "Washington, DC",
                        StreetAddress = "The White House, 1600 Pennsylvania Ave NW",
                        ZipCode = "20500"
                    }

                }
            };

            context.Customers.AddRange(orders);
            context.SaveChanges();
        }
    }
}
