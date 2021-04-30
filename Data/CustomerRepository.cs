using BookingSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BookingContext _ctx;

        public CustomerRepository(BookingContext ctx)
        {
            _ctx = ctx;
        }


        public IEnumerable<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            foreach (Customer c in _ctx.Customers)
            {
                customers.Add(c);
            }
            if(customers == null)
            {
                throw new NullReferenceException();
            }
            return customers; 
        }

        public Customer GetCustomerById(int Id)
        {
            Customer customer = _ctx.Customers.FirstOrDefault(c => c.Id == Id);
            if(customer == null)
            {
                throw new ArgumentException();
            }
            return customer;
        }

        public void CreateNewCustomer(Customer customer)
        {
            _ctx.Add(customer);
        }

        public bool UpdateCustomer(Customer updateCustomer)
        {
            Customer currentCustomer = GetCustomerById(updateCustomer.Id);
            if (currentCustomer == null)
            {
               throw new ArgumentException();
            }
            _ctx.Customers.Remove(currentCustomer);
            _ctx.Customers.Add(updateCustomer);
            return true;
        }

        public bool DeleteCustomerById(int id)
        {
            Customer customer = GetCustomerById(id);
            if(customer == null)
            {
                throw new ArgumentException();
            }
            _ctx.Customers.Remove(customer);
            return true;
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}
