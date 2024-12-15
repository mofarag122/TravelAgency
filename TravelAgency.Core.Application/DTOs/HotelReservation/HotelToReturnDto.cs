using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Domain.Entities.Hotel_Reservation;

namespace TravelAgency.Core.Application.DTOs.HotelReservation
{
    public class HotelToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string ImagePath { get; set; } = null!;

        public Location Location { get; set; } = null!;


        public List<RoomToReturnDto> Rooms = new List<RoomToReturnDto>();
    }
}
