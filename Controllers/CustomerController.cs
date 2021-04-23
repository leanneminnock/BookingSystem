using AutoMapper;
using BookingSystem.Data;
using BookingSystem.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _repo;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public ActionResult<IEnumerable<Customer>> GetAllCustomers()
        {
            var customers = _repo.GetAllCustomers();
            return Ok(_mapper.Map<IEnumerable<Customer>>(customers));
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomerById(int id)
        {
            try
            {
                var customer = _repo.GetCustomerById(id);
                return Ok(_mapper.Map<Customer>(customer));
            }
            catch (ArgumentException ex)
            {
                return NotFound($"No customer matching Id {id}");
            }
        }

        [HttpPost]
        public ActionResult<Customer> CreateCustomer([FromBody] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _repo.CreateNewCustomer(customer);
                if (!_repo.SaveAll()) { return BadRequest(); }
                return Ok(_mapper.Map<Customer>(customer));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public ActionResult<Customer> UpdateCustomer([FromBody] Customer customer)
        {
            if (!_repo.UpdateCustomer(customer)) { return BadRequest(); }

            if (!_repo.SaveAll()) { return BadRequest(); }
            return Ok(_mapper.Map<Customer>(customer));
        }

        [HttpDelete]
        public ActionResult DeleteCustomerById(int id)
        {
            if (!_repo.DeleteCustomerById(id)) { return NotFound("Customer Id Not Valid."); }
            if (!_repo.SaveAll()) { return BadRequest("Changes could not be saved."); }
            return Ok("Customer  Deleted");
        }

    }
}
