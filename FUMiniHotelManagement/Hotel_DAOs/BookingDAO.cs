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
        private FuminiHotelManagementContext dbcontext;
        private static BookingDAO instance;
        private static ArrayList listBookingDetails = new ArrayList();

        public static BookingDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BookingDAO();
                }
                return instance;
            }
        }

        public BookingDAO()
        {
            // Initialize with sample data
            BookingDetail booking1 = new BookingDetail(1, 101, DateOnly.Parse("2024-03-01"),
                DateOnly.Parse("2024-03-03"), 200.00m);
            BookingDetail booking2 = new BookingDetail(2, 102, DateOnly.Parse("2024-03-02"),
                DateOnly.Parse("2024-03-04"), 250.00m);
            // Add more sample data as needed

            listBookingDetails.Add(booking1);
            listBookingDetails.Add(booking2);
        }

        public ArrayList GetBookingDetails()
        {
            return listBookingDetails;
        }

        public BookingDetail GetBookingDetailById(int id)
        {
            foreach (BookingDetail detail in listBookingDetails)
            {
                if (detail.BookingReservationId == id)
                {
                    return detail;
                }
            }
            return null;
        }

        public bool CreateBookingDetail(BookingDetail bookingDetail)
        {
            bool isSuccess = false;
            BookingDetail existing = GetBookingDetailById(bookingDetail.BookingReservationId);
            if (existing == null)
            {
                listBookingDetails.Add(bookingDetail);
                isSuccess = true;
            }
            return isSuccess;
        }

        public bool DeleteBooking(BookingDetail booking)
        {
            bool isSuccess = false;
            BookingDetail existing = GetBookingDetailById(booking.BookingReservationId);
            if (existing != null)
            {
                listBookingDetails.Remove(existing);
                isSuccess = true;
            }
            return isSuccess;
        }

        public bool UpdateBooking(BookingDetail booking)
        {
            bool isSuccess = false;
            BookingDetail existing = GetBookingDetailById(booking.BookingReservationId);
            if (existing != null)
            {
                existing.RoomId = booking.RoomId;
                existing.StartDate = booking.StartDate;
                existing.EndDate = booking.EndDate;
                existing.ActualPrice = booking.ActualPrice;
                isSuccess = true;
            }
            return isSuccess;
        }


    }
}
