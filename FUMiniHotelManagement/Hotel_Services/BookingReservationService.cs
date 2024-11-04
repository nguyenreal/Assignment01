using Hotel_BussinessObjects;
using Hotel_Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Services
{
    public class BookingReservationService : IBookingReservastionService
    {
        private readonly IBookingReservationRepository iBookingReservationRepository;
        public BookingReservationService()
        {
            iBookingReservationRepository = new BookingReservationRepository();
        }

        public bool CreateBookingReservation(BookingReservation bookingRe)
        {
            return iBookingReservationRepository.CreateBookingReservation(bookingRe);
        }

        public ArrayList GetBookingReservations()
        {
            return iBookingReservationRepository.GetBookingReservations();
        }


    }
}
