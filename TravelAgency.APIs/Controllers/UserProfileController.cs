using Microsoft.AspNetCore.Mvc;
using ThirdParty.Events.BLL.DTOs;
using TravelAgency.Core.Application.DTOs.UserProfile;
using TravelAgency.Core.Application.Service_Contracts;
using TravelAgency.Infrastructure.ThirdParty.Services;

namespace TravelAgency.APIs.Controllers
{
    public class UserProfileController : BaseApiController
    {
        private IUserProfileService _profileService;
        private IEventAdapterService _eventAdapterService;

        public UserProfileController(IUserProfileService profileService, IEventAdapterService eventAdapterService)
        {
            _profileService = profileService;
            _eventAdapterService = eventAdapterService;
        }

        [HttpPut("changeProfile")]
        public ActionResult<string> ChangeProfile(string? token, [FromBody] UserToChangeProfileDto userProfileDto)
        {
            return Ok(_profileService.ChangeProfile(token, userProfileDto));
        }

        [HttpGet("returnReservations")]
        public ActionResult<ReservationToReturnDto?> GetUserReservations(string? token)
        {
            return Ok(_profileService.ReturnAllReservation(token));
        }

        [HttpGet("returnEventReservations")]
        public ActionResult<List<EventReservationToReturnDto>> GetEventReservations(string? token)
        {
            return Ok(_eventAdapterService.GetUserReservations(token));
        }

        [HttpDelete("cancelReservation")]
        public ActionResult<string> CancelReservation(string? token, int reservationId)
        {
            return Ok(_profileService.CancelReservation(token, reservationId));
        }

        [HttpDelete("cancelEventReservation")]
        public ActionResult<string> CancelEventReservation(string? token, int reservationId)
        {
            return Ok(_eventAdapterService.CancelReservation(token , reservationId));
        }

        [HttpGet("getRecommendedEvents")]
        public ActionResult<List<EventToReturnDto>> GetRecommendedEvents(string? token)
        {
            return _eventAdapterService.RecommendEvents(token);
        }

        [HttpPut("updateReservation")]
        public ActionResult<string> UpdateReservation(string? token , ReservationToChangeDto reservationDto)
        {
            return Ok(_profileService.UpdateReservation(token, reservationDto));
        }
    }
}
