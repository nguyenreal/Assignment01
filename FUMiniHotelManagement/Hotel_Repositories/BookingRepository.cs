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
    public class BookingRepository : IBookingRepository
    {
        public bool CreateBookingDetail(BookingDetail bookingDetail) => BookingDAO.CreateBookingDetail(bookingDetail);

        public void DeleteBooking(BookingDetail booking) => BookingDAO.DeleteBooking(booking);

        public BookingDetail GetBookingDetailById(int id) => BookingDAO.GetBookingDetailById(id);

        public ArrayList GetBookingDetails() => BookingDAO.GetBookingDetails();

        public void SaveBooking(BookingDetail booking) => BookingDAO.SaveBooking(booking);

        public void UpdateBooking(BookingDetail booking) => BookingDAO.UpdateBooking(booking);
    }
}
