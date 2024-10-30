using Hotel_BussinessObjects;
using Hotel_DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Repositories
{
    public class BookingReservationRepository : IBookingReservationRepository
    {
        public bool CreateBookingReservation(BookingReservation bookingRe) => BookingReservationDAO.CreateBookingReservation(bookingRe);
        public List<BookingReservation> GetBookingReservations() => BookingReservationDAO.GetBookingReservations();

        
    }
}
