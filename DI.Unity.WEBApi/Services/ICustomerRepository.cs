using System.Collections.Generic;
using DI.Unity.WEBApi.Models;

namespace DI.Unity.WEBApi.Services
{
    public interface ICustomerRepository
    {
        Customer GetById(int id);
        List<Customer> GetAll();
        void Update(Customer customer);
    }
}
