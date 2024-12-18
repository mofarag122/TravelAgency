using Microsoft.AspNetCore.Mvc;
using TravelAgency.Core.Application.DTOs.HotelReservation;
using TravelAgency.Core.Application.Service_Contracts;
using TravelAgency.Core.Domain.Entities.Hotel_Reservation;

namespace TravelAgency.APIs.Controllers
{
    public class HotelReservationController : BaseApiController
    {
        private IHotelReservationService _hotelReservationService;

        public HotelReservationController(IHotelReservationService hotelReservationService)
        {
            _hotelReservationService = hotelReservationService;
        }


        [HttpGet("getAllHotels")] // GET: api/hotelReservation
        public ActionResult<List<HotelToReturnDto>> GetAllHotels(string? token)
        {          
            return Ok(_hotelReservationService.GetAllHotels(token));
        }

        [HttpGet("getHotels")]// GET: api/getHotels
        public ActionResult<List<HotelToReturnDto>> GetHotels(string? token ,[FromQuery] HotelSpecParmas hotelSpecs)
        {
            return Ok(_hotelReservationService.GetHotels(token, hotelSpecs));
        }

        [HttpGet("getHotel")] // GET : api/getHotel
        public ActionResult<HotelToReturnDto> GetHotel(string? token, int id)
        {
            return Ok(_hotelReservationService.GetHotel(token, id));
        }

        [HttpGet("getRoom")] // GET: api/getRoom
        public ActionResult<RoomToReturnDto> GetRoom(string? token , int hotelId , int roomId)
        {
            return Ok(_hotelReservationService.GetRoom(token, hotelId, roomId));
        }

        [HttpGet("getRooms")] // GET: api/getRooms
        public ActionResult<List<HotelToReturnDto>> GetRooms(string? token , int hotelId , [FromQuery] RoomSpecParams roomSpecs)
        {
            return Ok(_hotelReservationService.GetRooms(token , hotelId , roomSpecs));
        }

        [HttpPost("reserveRoom")] // POST: api/reserveRoom
        public ActionResult<string> Reserve(string? token ,[FromBody] ReservationToCreateDto reservationDto)
        {
            return Ok(_hotelReservationService.ReserveRoom(token, reservationDto));
        }

        

    }
}
