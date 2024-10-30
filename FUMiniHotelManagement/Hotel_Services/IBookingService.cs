using Hotel_BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Services
{
    public interface IBookingService
    {
        List<BookingDetail> GetBookingDetails();
        void SaveBooking(BookingDetail booking);
        void DeleteBooking(BookingDetail booking);
        void UpdateBooking(BookingDetail booking);
        BookingDetail GetBookingDetailById(int id);
        bool CreateBookingDetail(BookingDetail bookingDetail);
    }
}
