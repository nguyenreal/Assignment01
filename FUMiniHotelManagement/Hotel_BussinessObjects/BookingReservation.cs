using System;
using System.Collections;
using System.Collections.Generic;

namespace Hotel_BussinessObjects;

public partial class BookingReservation
{
    public BookingReservation()
    {
        BookingDetails = new List<BookingDetail>();
    }

    public BookingReservation(int bookingReservationId, DateOnly bookingDate,
        decimal totalPrice, int customerId, byte bookingStatus)
    {
        this.BookingReservationId = bookingReservationId;
        this.BookingDate = bookingDate;
        this.TotalPrice = totalPrice;
        this.CustomerId = customerId;
        this.BookingStatus = bookingStatus;
        this.BookingDetails = new List<BookingDetail>();
    }

    public int BookingReservationId { get; set; }
    public DateOnly? BookingDate { get; set; }
    public decimal? TotalPrice { get; set; }
    public int CustomerId { get; set; }
    public byte? BookingStatus { get; set; }

    public virtual ICollection<BookingDetail> BookingDetails { get; set; }
    public virtual Customer Customer { get; set; } = null!;
}
