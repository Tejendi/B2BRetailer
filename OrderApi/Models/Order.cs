using System;
namespace OrderApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int CustomerRegNo { get; set; }
        public OrderStatusEnum Status { get; set; }
    }

    public enum OrderStatusEnum
    {   
        Requested = 0,
        Shipped = 1,
        Completed = 2,
        Canceled = 3,
        Paid = 4
    }
}
