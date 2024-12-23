using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Core.Application.DTOs.UserProfile
{
    public class ReservationToChangeDto
    {
        public int ReservationId { get; set; }

        [Required(ErrorMessage = "Start Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Start Date must be a valid date.")]
        public string StartDate { get; set; } = null!;

        [Required(ErrorMessage = "End Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "End Date must be a valid date.")]
        public string EndDate { get; set; } = null!;
    }
}
