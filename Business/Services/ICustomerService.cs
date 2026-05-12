using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Models;
using Data.Entities;

namespace Business.Services
{
    public interface ICustomerService
    {
        void AddCustomer(CustomerModel model);
        IEnumerable<Customer> GetAllCustomers();
    }
}
