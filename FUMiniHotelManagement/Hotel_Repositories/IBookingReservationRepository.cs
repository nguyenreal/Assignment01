using Hotel_BussinessObjects;
using Hotel_DAOs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Repositories
{
    public interface IBookingReservationRepository
    {
        ArrayList GetBookingReservations();
        bool CreateBookingReservation(BookingReservation bookingRe);
    }
}
