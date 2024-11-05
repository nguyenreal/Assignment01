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
        public bool CreateBookingDetail(BookingDetail bookingDetail) => BookingDAO.Instance.CreateBookingDetail(bookingDetail);

        public void DeleteBooking(BookingDetail booking) => BookingDAO.Instance.DeleteBooking(booking);

        public BookingDetail GetBookingDetailById(int id) => BookingDAO.Instance.GetBookingDetailById(id);

        public ArrayList GetBookingDetails() => BookingDAO.Instance.GetBookingDetails();

        public void UpdateBooking(BookingDetail booking) => BookingDAO.Instance.UpdateBooking(booking);
    }
}
