using Microsoft.AspNetCore.Mvc;
using TravelAgency.Core.Application.DTOs.UserProfile;
using TravelAgency.Core.Application.Service_Contracts;

namespace TravelAgency.APIs.Controllers
{
    public class UserProfileController : BaseApiController
    {
        private IUserProfileService _profileService;

        public UserProfileController(IUserProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpPut("changeProfile")]
        public ActionResult<string> ChangeProfile(string? token , [FromBody]UserToChangeProfileDto userProfileDto)
        {
            return Ok(_profileService.ChangeProfile(token, userProfileDto));
        }

        [HttpGet("returnReservations")]
        public ActionResult<ReservationToReturnDto?> GetUserReservations(string? token)
        {
            return Ok(_profileService.ReturnAllReservation(token));
        }

        [HttpDelete("cancelReservation")]
        public ActionResult<string> CancelReservation(string? token, int reservationId)
        {
            return Ok( _profileService.CancelReservation(token, reservationId));
        }
        [HttpPut("updateReservation")]
        public ActionResult<string> UpdateReservation(string? token , ReservationToChangeDto reservationDto)
        {
            return Ok(_profileService.UpdateReservation(token, reservationDto));
        }
    }
}
