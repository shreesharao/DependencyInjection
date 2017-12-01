using System.Collections.Generic;
using System.Linq;
using DI.Unity.WEBApi.Models;

namespace DI.Unity.WEBApi.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        public Customer GetById(int id)
        {
            List<Customer> customers = GetAll();
            return customers.Where(item => item.Id == id).FirstOrDefault();
        }

        public List<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>()
                {
                    new Customer() { Id = 1, Name = "Rushang Chauhan", Email = "chauhanrushang@gmail.com", Twitter = "@chauhanrushang" },
                    new Customer() { Id = 2, Name = "John Doe", Email = "johndoe.com", Twitter = "@johndoe" },
                };

            return customers;
        }

        public void Update(Customer customer)
        {
        }
    }
}
