using Hotel_BussinessObjects;
using Hotel_DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Repositories
{
    public interface ICustomerRepository
    {
        Customer GetCustomerById(string customerID);
        Customer GetCustomerByEmail(string email);
    }
}
