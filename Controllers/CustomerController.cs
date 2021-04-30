using AutoMapper;
using BookingSystem.Data;
using BookingSystem.Data.Entities;
using BookingSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _repo;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CustomerViewModel>> GetAllCustomers()
        {
            var customers = _repo.GetAllCustomers();
            try
            {
                return Ok(_mapper.Map<IEnumerable<CustomerViewModel>>(customers));
            }
            catch(NullReferenceException ex)
            {
                return BadRequest($"Could not fetch all customers, {ex}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<CustomerViewModel> GetCustomerById(int id)
        {
            try
            {
                var customer = _repo.GetCustomerById(id);
                return Ok(_mapper.Map<CustomerViewModel>(customer));
            }
            catch (ArgumentException ex)
            {
                return NotFound($"No customer matching Id {id}, {ex}");
            }
        }

        [HttpPost]
        public ActionResult<CustomerViewModel> CreateCustomer([FromBody] Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repo.CreateNewCustomer(customer);
                    if (!_repo.SaveAll()) { return BadRequest(); }
                    return Ok(_mapper.Map<CustomerViewModel>(customer));
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch(ArgumentException ex)
            {
                return BadRequest($"No valid customer found {ex}");
            }
        }

        [HttpPut]
        public ActionResult<CustomerViewModel> UpdateCustomer([FromBody] Customer customer)
        {
            try
            {
                if (!_repo.UpdateCustomer(customer)) { return BadRequest(); }

                if (!_repo.SaveAll()) { return BadRequest(); }
                return Ok(_mapper.Map<CustomerViewModel>(customer));
            }
            catch(ArgumentException ex)
            {
                return BadRequest($"No valid customer could be found, {ex}");
            }
        }

        [HttpDelete]
        public ActionResult DeleteCustomerById(int id)
        {
            try
            {
                if (!_repo.DeleteCustomerById(id)) { return NotFound("Customer Id Not Valid."); }
                if (!_repo.SaveAll()) { return BadRequest("Changes could not be saved."); }
                return Ok("Customer  Deleted");
            }
            catch(ArgumentException ex)
            {
                return BadRequest($"No valid customer was found, {ex}");
            }
        }

    }
}
