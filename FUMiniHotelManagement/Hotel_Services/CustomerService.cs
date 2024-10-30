using Hotel_BussinessObjects;
using Hotel_Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository iCustomerRepository;
        public CustomerService()
        {
            iCustomerRepository = new CustomerRepository();
        }

        public Customer GetCustomerByEmail(string email)
        {
            return iCustomerRepository.GetCustomerByEmail(email);
        }

        public Customer GetCustomerById(string customerID)
        {
            return iCustomerRepository.GetCustomerById(customerID);
        }
    }
}
