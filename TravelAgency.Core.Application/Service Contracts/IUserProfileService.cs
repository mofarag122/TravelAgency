using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Application.DTOs.UserProfile;
using TravelAgency.Core.Domain.Entities.Hotel_Reservation;

namespace TravelAgency.Core.Application.Service_Contracts
{
    public interface IUserProfileService
    {
        public string ChangeProfile(string? token, UserToChangeProfileDto userProfileDto);

        public List<ReservationToReturnDto> ReturnAllReservation(string? token);

        public string UpdateReservation(string? token, ReservationToChangeDto reservationDto);

        public string CancelReservation(string? token , int reservationId);
    }
}
