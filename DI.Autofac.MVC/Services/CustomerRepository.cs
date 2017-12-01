using System.Collections.Generic;
using System.Linq;

namespace DI.MVC
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
                    new Customer() { Id = 2, Name = "John V. Petersen", Email = "johnvpetersen@gmail.com", Twitter = "@johnvpetersen" },
                };

            return customers;
        }

        public void Update(Customer customer)
        {
        }
    }
}
