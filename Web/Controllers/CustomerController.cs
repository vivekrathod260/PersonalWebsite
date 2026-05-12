using Microsoft.AspNetCore.Mvc;
using Business.Models;
using Business.Services;
using Business.Services.Implementation;
using Data.Entities;
using Data;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService) 
        {
            this.customerService = customerService;
        }

        [HttpGet]
        public IEnumerable<Customer> GetAllCustomers()
        {
            return this.customerService.GetAllCustomers();
        }

        [HttpPost]
        public bool AddCustomer(CustomerModel customer)
        {
            this.customerService.AddCustomer(customer);
            return true;
        }
    }
}
