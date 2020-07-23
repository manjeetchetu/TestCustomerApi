using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestApi.BAL.Interfaces;
using TestApi.DTO;
using TestApi.Models;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class CustomersController : ControllerBase
    {
        private readonly ICustomer _customer;
        private readonly IMapper _mapper;
        public CustomersController(ICustomer customer, IMapper mapper)
        {
            _customer = customer;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllCustomerAsync()
        {
            var customers = await _customer.GetAllCustomers();
            var result = _mapper.Map<List<CustomerDTO>>(customers);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerDTO customerDTO)
        {
            if (ModelState.IsValid)
            {
                var model = _mapper.Map<Customer>(customerDTO);
                var result = await _customer.CreateNewCustomer(model);
                return Ok(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

    }
}