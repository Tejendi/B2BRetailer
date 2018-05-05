﻿using OrderApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderApi.Data
{
    public static class DbInitializer
    {
        // This method will create and seed the database.
        public static void Initialize(OrderApiContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Look for any Products
            if (context.Orders.Any())
            {
                return;   // DB has been seeded
            }

            List<Order> orders = new List<Order>
            {
                new Order { Date = DateTime.Today, ProductId = 1, Quantity = 2, CustomerRegNo = 1, Status = OrderStatusEnum.Completed}
            };

            context.Orders.AddRange(orders);
            context.SaveChanges();
        }
    }
}
