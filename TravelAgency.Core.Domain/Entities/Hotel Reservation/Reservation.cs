using System;
using System.Collections.Generic;
using TravelAgency.Core.Domain.Entities._Common;

namespace TravelAgency.Core.Domain.Entities.Hotel_Reservation
{
    public class Reservation : Entity
    {
        public int UserId { get; set; }

        public int HotelId { get; set; }

        public int RoomsId { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public DateTime ReservationDate { get; set; } = DateTime.UtcNow;

        public decimal Cost { get; set; }

        public int Duration
        {
            get
            {
                return (EndDate.ToDateTime(new TimeOnly()) - StartDate.ToDateTime(new TimeOnly())).Days;
            }
        }

    
    }
}
