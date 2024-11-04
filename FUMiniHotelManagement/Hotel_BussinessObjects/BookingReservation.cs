using System;
using System.Collections;
using System.Collections.Generic;

namespace Hotel_BussinessObjects;

public partial class BookingReservation
{
    public int BookingReservationId { get; set; }

    public DateOnly? BookingDate { get; set; }

    public decimal? TotalPrice { get; set; }

    public int CustomerId { get; set; }

    public byte? BookingStatus { get; set; }

    private ArrayList _bookingDetails = new ArrayList();
    public virtual ArrayList BookingDetails
    {
        get => _bookingDetails;
        set => _bookingDetails = value;
    }

    public virtual Customer Customer { get; set; } = null!;
}
