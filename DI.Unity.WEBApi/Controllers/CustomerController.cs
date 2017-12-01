using DI.Unity.WEBApi.Models;
using DI.Unity.WEBApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DI.Unity.WEBApi.Controllers
{
    public class CustomerController : ApiController
    {
        ICustomerRepository _repository;

        public CustomerController(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Customer> Get()
        {
            return _repository.GetAll();
        }

        public IHttpActionResult Get(int id)
        {
            var customer = _repository.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
    }
}
