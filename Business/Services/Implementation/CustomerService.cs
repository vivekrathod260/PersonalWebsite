using Business.Models;
using Data;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Implementation
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext dbContext;

        public CustomerService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddCustomer(CustomerModel model)
        {
            var customer = new Customer
            {
                FullName = model.FullName,
                Email = model.Email
            };

            this.dbContext.Customers.Add(customer);
            this.dbContext.SaveChanges();
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return this.dbContext.Customers;
        }
    }
}
