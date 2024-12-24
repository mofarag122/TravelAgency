using Microsoft.AspNetCore.Mvc;
using ThirdParty.Events.BLL.DTOs;
using TravelAgency.Core.Application.DTOs.UserProfile;
using TravelAgency.Core.Application.Service_Contracts;
using TravelAgency.Core.Domain.Repository_Contracts;
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
        public ActionResult<string> ChangeProfile(string? token, [FromBody] UserToChangeProfileDto userProfileDto , [FromServices] IIdentityRepository identityRepository)
        {
            return Ok(_profileService.ChangeProfile(token, userProfileDto , identityRepository));
        }

        [HttpGet("returnReservations")]
        public ActionResult<ReservationToReturnDto?> GetUserReservations(string? token, IHotelRepository hotelRepository, IHotelReservationRepository reservationRepository)
        {
            return Ok(_profileService.ReturnAllReservation(token));
        }

        [HttpGet("returnEventReservations")]
        public ActionResult<List<EventReservationToReturnDto>> GetEventReservations(string? token, IHotelRepository hotelRepository, IHotelReservationRepository reservationRepository)
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
        public ActionResult<List<EventToReturnDto>> GetRecommendedEvents(string? token,[FromServices] IHotelRepository hotelRepository,[FromServices] IHotelReservationRepository reservationRepository)
        {
            return _eventAdapterService.RecommendEvents(token , hotelRepository , reservationRepository);
        }

        [HttpPut("updateReservation")]
        public ActionResult<string> UpdateReservation(string? token , ReservationToChangeDto reservationDto)
        {
            return Ok(_profileService.UpdateReservation(token, reservationDto));
        }
    }
}
