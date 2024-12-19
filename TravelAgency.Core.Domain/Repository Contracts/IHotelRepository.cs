using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Domain.Entities.Hotel_Reservation;
using TravelAgency.Core.Domain.Specifications;

namespace TravelAgency.Core.Domain.Repository_Contracts
{
    public interface IHotelRepository
    {
        public void AddHotel(Hotel hotel);
        public void AddRoom(Room room);
        public List<Hotel> GetAllHotels();
        public List<Hotel> GetHotels(ISpecifications<Hotel> specifications);
        public Hotel GetHotel(int id);  
        public List<Hotel> GetAllHotelsWithRooms();
        public List<Room> GetRooms(int hotelId, ISpecifications<Room> specifications);
        public List<Room> GetRoomsById(int hotelId);
        public Room GetRoomById(int hotelId, int roomId);
    }
}
