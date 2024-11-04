﻿using Hotel_BussinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Services
{
    public interface IBookingReservastionService
    {
        ArrayList GetBookingReservations();
        bool CreateBookingReservation(BookingReservation bookingRe);

    }
}
