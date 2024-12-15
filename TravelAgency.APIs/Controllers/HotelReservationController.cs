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

        [HttpGet] // GET: api/hotelReservation
        public ActionResult<List<HotelToReturnDto>> GetAll()
        {
            return Ok(_hotelReservationService.GetAllHotels());
        }
    }
}
