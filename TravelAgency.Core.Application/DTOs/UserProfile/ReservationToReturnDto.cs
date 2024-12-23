using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Core.Application.DTOs.UserProfile
{
    public class ReservationToReturnDto
    {
        public int Id { get; set; }

        public int HotelId { get; set; }

        
        public int RoomId { get; set; }

      
        public string StartDate { get; set; } = null!;

      
        public string EndDate { get; set; } = null!;

        public decimal Cost { get; set; }
    }
}
