using BookingSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Data
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomerById(int Id);
        void CreateNewCustomer(Customer customer);
        bool UpdateCustomer(Customer cust);
        bool SaveAll();
        bool DeleteCustomerById(int id);
    }
}
