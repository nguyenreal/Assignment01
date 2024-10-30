using Hotel_BussinessObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_DAOs
{
    public class CustomerDAO
    {
        public static Customer GetCustomerById (string customerID)
        {
            using var db = new FuminiHotelManagementContext();
            return db.Customers.FirstOrDefault(c => c.CustomerId.Equals(customerID));
        }
        public static Customer GetCustomerByEmail (string email)
        {
            using var db = new FuminiHotelManagementContext();
            return db.Customers.FirstOrDefault(c => c.EmailAddress.Equals(email));
        }
    }
}
