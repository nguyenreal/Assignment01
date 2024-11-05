using System;
using System.Collections.Generic;

namespace Hotel_BussinessObjects;

public partial class BookingDetail
{
    public BookingDetail() { }  
    public BookingDetail(int bookingReservationId, int roomId, DateOnly startDate,
        DateOnly endDate, decimal actualPrice)
    {
        this.BookingReservationId = bookingReservationId;
        this.RoomId = roomId;
        this.StartDate = startDate;
        this.EndDate = endDate;
        this.ActualPrice = actualPrice;
    }
    public int BookingReservationId { get; set; } 

    public int RoomId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public decimal? ActualPrice { get; set; }

    public virtual BookingReservation BookingReservation { get; set; } = null!;

    public virtual RoomInformation Room { get; set; } = null!;
}
