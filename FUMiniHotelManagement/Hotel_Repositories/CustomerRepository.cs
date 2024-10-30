using Hotel_BussinessObjects;
using Hotel_DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public Customer GetCustomerByEmail(string email) => CustomerDAO.GetCustomerByEmail(email);

        public Customer GetCustomerById(string customerID) => CustomerDAO.GetCustomerById(customerID);
    }
}
