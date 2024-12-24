using TravelAgency.Core.Application.DTOs.UserProfile;
using TravelAgency.Core.Domain.Repository_Contracts;

namespace TravelAgency.Core.Application.Service_Contracts
{
    public interface IUserProfileService
    {
        public string ChangeProfile(string? token, UserToChangeProfileDto userProfileDto, IIdentityRepository identityRepository);
        public List<ReservationToReturnDto> ReturnAllReservation(string? token);
        public string UpdateReservation(string? token, ReservationToChangeDto reservationDto);
        public string CancelReservation(string? token , int reservationId);
    }
}
