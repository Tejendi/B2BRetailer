using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Data;
using OrderApi.Models;
using RestSharp;

namespace OrderApi.Controllers
{
    [Route("api/Orders")]
    public class OrdersController : Controller
    {
        private readonly IRepository<Order> repository;

        public OrdersController(IRepository<Order> repos)
        {
            repository = repos;
        }

        // GET: api/orders
        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return repository.GetAll();
        }

        // GET api/products/5
        [HttpGet("{id}", Name = "GetOrder")]
        public IActionResult Get(int id)
        {
            var item = repository.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST api/orders
        [HttpPost]
        public IActionResult Post([FromBody]Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }

            // Call ProductApi to get the product ordered
            RestClient c = new RestClient {BaseUrl = new Uri("http://localhost:5000/api/products/")};
            // You may need to change the port number in the BaseUrl below
            // before you can run the request.
            var request = new RestRequest(order.ProductId.ToString(), Method.GET);
            var response = c.Execute<Product>(request);
            var orderedProduct = response.Data;

            if (order.Quantity <= orderedProduct.ItemsInStock)
            {
                // reduce the number of items in stock for the ordered product,
                // and create a new order.
                orderedProduct.ItemsInStock -= order.Quantity;
                var updateRequest = new RestRequest(orderedProduct.Id.ToString(), Method.PUT);
                updateRequest.AddJsonBody(orderedProduct);
                var updateResponse = c.Execute(updateRequest);

                if (updateResponse.IsSuccessful)
                {
                    var newOrder = repository.Add(order);
                    return CreatedAtRoute("GetOrder", new { id = newOrder.Id }, newOrder);
                }
            }

            // If the order could not be created, "return no content".
            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            repository.Remove(id);
        }
        // DELETE api/values/5
        [HttpPost]
        public void Cancel(Order order)
        {
            //THIS IS SO WRONG NEVER DO THIS PLX.
            order.Status = OrderStatusEnum.Canceled;
            repository.Edit(order);
        }
        [HttpGet]
        public decimal CalaculateShippingCharge()
        {
            return (decimal) 1000.3;
        }
        [HttpGet]
        public DateTime CalaculateDeliveryDate()
        {
            return DateTime.Today;
        }
    }
}
