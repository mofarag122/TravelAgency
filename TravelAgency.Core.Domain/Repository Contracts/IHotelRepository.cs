using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Domain.Entities.Hotel_Reservation;

namespace TravelAgency.Core.Domain.Repository_Contracts
{
    public interface IHotelRepository
    {
        List<Hotel> GetAllHotels();
        public List<Hotel> GetAllHotelsWithRooms();
        public List<Room> GetRoomsById(int hotelId);

        public Room GetRoomById(int hotelId, int roomId);
    }
}
