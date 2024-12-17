using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Domain.Entities.Hotel_Reservation;

namespace TravelAgency.Core.Application.DTOs.HotelReservation
{
    public class RoomToReturnDto
    {
        public int Id { get; set; }

        public string RoomType { get; set; } = null!;

        public List<string>? ImagePaths { get; set; }

        public decimal PricePerNight { get; set; }

    }
}
