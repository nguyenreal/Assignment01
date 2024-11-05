using Hotel_BussinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_DAOs
{
    public class BookingReservationDAO
    {
        private FuminiHotelManagementContext dbcontext;
        private static BookingReservationDAO instance;
        private static ArrayList listBookingReservations = new ArrayList();

        public static BookingReservationDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BookingReservationDAO();
                }
                return instance;
            }
        }

        public BookingReservationDAO()
        {
            // Initialize with sample data
            BookingReservation booking1 = new BookingReservation(1, DateOnly.Parse("2024-03-01"),
                400.00m, 1001, 1);
            BookingReservation booking2 = new BookingReservation(2, DateOnly.Parse("2024-03-02"),
                500.00m, 1002, 1);
            // Add more sample data as needed

            listBookingReservations.Add(booking1);
            listBookingReservations.Add(booking2);
        }

        public ArrayList GetBookingReservations()
        {
            return listBookingReservations;
        }

        public BookingReservation GetBookingReservationById(int id)
        {
            foreach (BookingReservation reservation in listBookingReservations)
            {
                if (reservation.BookingReservationId == id)
                {
                    return reservation;
                }
            }
            return null;
        }

        public bool CreateBookingReservation(BookingReservation bookingReservation)
        {
            bool isSuccess = false;
            BookingReservation existing = GetBookingReservationById(bookingReservation.BookingReservationId);
            if (existing == null)
            {
                listBookingReservations.Add(bookingReservation);
                isSuccess = true;
            }
            return isSuccess;
        }
    }
}
