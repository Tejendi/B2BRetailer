using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerApi.Data;
using CustomerApi.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;


namespace CustomerApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly CustomerRepository _repository;

        public ValuesController(CustomerRepository repository)
        {
            _repository = repository;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return _repository.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Customer Get(int id)
        {

            return _repository.Get(id);
        }

        // POST api/values
        [HttpPost]
        public Customer Post([FromBody]Customer customer)
        {
            if (customer.Id <= 0)
                return _repository.Add(customer);
            _repository.Edit(customer);
            return _repository.Get(customer.Id);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repository.Remove(id);
        }

        [HttpGet("{id}")]
        public bool ValidateCreditStanding(string id)
        {
            RestClient c = new RestClient { BaseUrl = new Uri("http://localhost:5000/api/orders/GetAllFromCustomer") };
            // You may need to change the port number in the BaseUrl below
            // before you can run the request.
            var request = new RestRequest(id, Method.GET);
            var response = c.Execute<List<Order>>(request);
            var orders = response.Data;
            return orders.Any(x => x.Status == 3);            
        }
    }
}
