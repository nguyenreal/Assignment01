using Hotel_BussinessObjects;
using Hotel_Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Services
{
    public class BookingService : IBookingService
    {
        public readonly IBookingRepository iBookingRepository;
        public BookingService()
        {
            iBookingRepository = new BookingRepository();
        }

        public bool CreateBookingDetail(BookingDetail bookingDetail)
        {
            return iBookingRepository.CreateBookingDetail(bookingDetail);   
        }

        public void DeleteBooking(BookingDetail booking)
        {
            iBookingRepository.DeleteBooking(booking);
        }

        public BookingDetail GetBookingDetailById(int id)
        {
            return iBookingRepository.GetBookingDetailById(id);
        }

        public List<BookingDetail> GetBookingDetails()
        {
            return iBookingRepository.GetBookingDetails();
        }

        public void SaveBooking(BookingDetail booking)
        {
            iBookingRepository.SaveBooking(booking);
        }

        public void UpdateBooking(BookingDetail booking)
        {
            iBookingRepository.UpdateBooking(booking);
        }
    }
}
