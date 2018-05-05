using Microsoft.AspNetCore.Mvc;
using OrderApi.Data;
using OrderApi.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderApi.Controllers
{
    [Route("api/Orders")]
    public class OrdersController : Controller
    {
        private readonly IRepository<Order> _repository;

        public OrdersController(IRepository<Order> repos)
        {
            _repository = repos;
        }

        // GET: api/orders
        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return _repository.GetAll();
        }

        // GET api/products/5
        [HttpGet("{id}", Name = "GetOrder")]
        public IActionResult Get(int id)
        {
            var item = _repository.Get(id);
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
                    var newOrder = _repository.Add(order);
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
            _repository.Remove(id);
        }
        // DELETE api/values/5
        [HttpPost]
        public void Cancel(Order order)
        {
            //THIS IS SO WRONG NEVER DO THIS PLX.
            order.Status = OrderStatusEnum.Canceled;
            _repository.Edit(order);
        }
        [HttpGet, Route("[action]")]
        public decimal CalaculateShippingCharge()
        {
            return (decimal) 1000.3;
        }

        [HttpGet, Route("[action]")]
        public DateTime CalaculateDeliveryDate()
        {
            return DateTime.Today;
        }

        [HttpGet, Route("[action]/{id}")]
        public IEnumerable<Order> GetAllFromCustomer(int id)
        {
            return _repository.GetAll().Where(x => x.CustomerRegNo == id);
        }
    }
}
