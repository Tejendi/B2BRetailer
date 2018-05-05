using CustomerApi.Data;
using CustomerApi.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CustomerApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IRepository<Customer> _repository;

        public ValuesController(IRepository<Customer> repository)
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

        [HttpGet, Route("[action]/{id}")]
        public bool ValidateCreditStanding(string id)
        {
            RestClient c = new RestClient { BaseUrl = new Uri("http://localhost:55557/api/orders/GetAllFromCustomer") };
            // You may need to change the port number in the BaseUrl below
            // before you can run the request.
            RestRequest request = new RestRequest(id, Method.GET);
            IRestResponse<List<Order>> response = c.Execute<List<Order>>(request);
            List<Order> orders = response.Data;
            return orders != null && orders.All(x => x.Status != 2); //None are "completed".            
        }
    }
}
