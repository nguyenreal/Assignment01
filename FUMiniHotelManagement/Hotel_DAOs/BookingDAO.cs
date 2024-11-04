using Hotel_BussinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_DAOs
{
    public class BookingDAO
    {
        public static ArrayList GetBookingDetails()
        {
            var listBookingDetails = new ArrayList();
            try
            {
                using var db = new FuminiHotelManagementContext();
                listBookingDetails.AddRange(db.BookingDetails.ToList());
            }
            catch (Exception e) { }
            return listBookingDetails;
        }

        public static void SaveBooking(BookingDetail booking)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                context.BookingDetails.Add(booking);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void DeleteBooking(BookingDetail booking)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                var p1 = context.BookingDetails.SingleOrDefault(c => c.BookingReservationId == booking.BookingReservationId);
                context.BookingDetails.Remove(p1);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateBooking(BookingDetail booking)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                context.Entry<BookingDetail>(booking).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static BookingDetail GetBookingDetailById(int id)
        {
            using var db = new FuminiHotelManagementContext();
            return db.BookingDetails.FirstOrDefault(c => c.BookingReservationId.Equals(id));
        }

        public static bool CreateBookingDetail(BookingDetail bookingDetail)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                context.BookingDetails.Add(bookingDetail);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while creating booking detail: " + ex.Message);
                return false;
            }
        }
    }
}
