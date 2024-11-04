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
        public static ArrayList GetBookingReservations()
        {
            var listBookingReservation = new ArrayList();
            try
            {
                using var context = new FuminiHotelManagementContext();
                listBookingReservation.AddRange(context.BookingReservations.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listBookingReservation;
        }

        public static bool CreateBookingReservation(BookingReservation bookingRe)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                context.BookingReservations.Add(bookingRe);
                context.SaveChanges();
                return true; // Indicates success
            }
            catch (Exception ex)
            {
                // Log the exception to better diagnose issues
                Console.WriteLine("Error while creating booking detail: " + ex.Message);
                return false; // Indicates failure
            }
        }
    }
}
