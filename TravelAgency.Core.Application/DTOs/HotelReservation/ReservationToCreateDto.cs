using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Core.Application.DTOs.HotelReservation
{
    public class ReservationToCreateDto
    {
        [Required(ErrorMessage = "Hotel Id is required.")]
        public int HotelId { get; set; }

        [Required(ErrorMessage = "Room Id is required.")]
        public int RoomId { get; set; }

        [Required(ErrorMessage = "Start Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Start Date must be a valid date.")]
        public string StartDate { get; set; } = null!;

        [Required(ErrorMessage = "End Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "End Date must be a valid date.")]
        public string EndDate { get; set; } = null!;

      
    }
}
