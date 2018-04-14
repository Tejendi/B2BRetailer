namespace CustomerApi.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Address BillingAddress { get; set; }
        public Address ShippingAddress { get; set; }
    }
}
