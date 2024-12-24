using Microsoft.AspNetCore.Mvc;
using ThirdParty.Events.BLL.DTOs;
using TravelAgency.Core.Application.Builder.Notification_Builder;
using TravelAgency.Core.Application.DTOs.HotelReservation;
using TravelAgency.Core.Application.Service_Contracts;
using TravelAgency.Core.Domain.Entities.Hotel_Reservation;
using TravelAgency.Core.Domain.Repository_Contracts;
using TravelAgency.Infrastructure.ThirdParty.Services;

namespace TravelAgency.APIs.Controllers
{
    public class HotelReservationController : BaseApiController
    {
        private IHotelReservationService _hotelReservationService;
        public HotelReservationController(IHotelReservationService hotelReservationService)
        {
            _hotelReservationService = hotelReservationService;
        }


        [HttpGet("getAllHotels")] // GET: api/HotelReservation/hotelReservation
        public ActionResult<List<HotelToReturnDto>> GetAllHotels(string? token)
        {          
            return Ok(_hotelReservationService.GetAllHotels(token));
        }

        [HttpGet("getHotels")]// GET: api/HotelReservation/getHotels
        public ActionResult<List<HotelToReturnDto>> GetHotels(string? token ,[FromQuery] HotelSpecParmas hotelSpecs)
        {
            return Ok(_hotelReservationService.GetHotels(token, hotelSpecs));
        }

        [HttpGet("getHotel")] // GET : api/HotelReservation/getHotel
        public ActionResult<HotelToReturnDto> GetHotel(string? token, int id)
        {
            return Ok(_hotelReservationService.GetHotel(token, id));
        }

        [HttpGet("getRoom")] // GET: api/HotelReservation/getRoom
        public ActionResult<RoomToReturnDto> GetRoom(string? token , int hotelId , int roomId)
        {
            return Ok(_hotelReservationService.GetRoom(token, hotelId, roomId));
        }

        [HttpGet("getRooms")] // GET: api/HotelReservation/getRooms
        public ActionResult<List<HotelToReturnDto>> GetRooms(string? token , int hotelId , [FromQuery] RoomSpecParams roomSpecs)
        {
            return Ok(_hotelReservationService.GetRooms(token , hotelId , roomSpecs));
        }

        [HttpPost("reserveRoom")] // POST: api/HotelReservation/reserveRoom
        public ActionResult<List<EventToReturnDto>> Reserve(string? token ,[FromBody] ReservationToCreateDto reservationDto , [FromServices] INotificationRepository notificationRepository , [FromServices] INotificationTemplateRepository notificationTemplateRepository , [FromServices] INotificationContentBuilder notificationContentBuilder, [FromServices] IIdentityRepository identityRepository , [FromServices] IEventAdapterService eventAdapterService , [FromServices] IHotelReservationRepository reservationRepository)
        {
            if (_hotelReservationService.ReserveRoom(token, reservationDto , notificationRepository , notificationTemplateRepository , notificationContentBuilder , identityRepository , reservationRepository))
            {
                var hotelDto = _hotelReservationService.GetHotel(token, reservationDto.HotelId);
                return Ok(eventAdapterService.RecommendEvents(hotelDto.Location, reservationDto.StartDate, reservationDto.EndDate));
            }
            return Ok();
               
        }

    }
}
