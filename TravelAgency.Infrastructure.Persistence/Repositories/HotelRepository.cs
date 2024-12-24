using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TravelAgency.Core.Domain.Entities.Hotel_Reservation;
using TravelAgency.Core.Domain.Entities.Identity;
using TravelAgency.Core.Domain.Repository_Contracts;
using TravelAgency.Core.Domain.Specifications;
using TravelAgency.Infrastructure.Persistence.Data_Storage;

namespace TravelAgency.Infrastructure.Persistence.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private _StorageManagement<Hotel> _StorageManagementForHotels;
        private _StorageManagement<Room>  _StorageManagementForRooms;
        public HotelRepository(IConfiguration configuration)
        {
            string hotelsfilePath = configuration["FileStorage:HotelsFilePath"]!;
            _StorageManagementForHotels = new _StorageManagement<Hotel>(hotelsfilePath);

            string roomsfilePath = configuration["FileStorage:RoomsFilePath"]!;
            _StorageManagementForRooms = new _StorageManagement<Room>(roomsfilePath);
        }


        public void AddHotel(Hotel hotel)
        {
            _StorageManagementForHotels.Add(hotel);
        }
        public void AddRoom(Room room)
        {
            _StorageManagementForRooms.Add(room);
        }
        public List<Hotel> GetAllHotels()
        {
            return _StorageManagementForHotels.GetAll();
        }
        public List<Hotel> GetHotels(ISpecifications<Hotel> specifications)
        {
            List<Hotel> hotels = _StorageManagementForHotels.GetAll();
            return _SpecificationsEvaluator<Hotel>.LambdaExpressionsBuilder(hotels, specifications).ToList();
        }
        public Hotel GetHotel(int id)
        {
            return _StorageManagementForHotels.GetAll().FirstOrDefault(h => h.Id == id)!;
        }
        public List<Hotel> GetAllHotelsWithRooms()
        {
            List<Room> rooms = _StorageManagementForRooms.GetAll();
            List<Hotel> hotels = _StorageManagementForHotels.GetAll();

            foreach (var hotel in hotels)
            {
                hotel.Rooms = rooms.Where(room => room.HotelId == hotel.Id).ToList();
            }

            return hotels;
        }
        public List<Room> GetRooms(int hotelId , ISpecifications<Room> specifications)
        {
            List<Room> rooms = _StorageManagementForRooms.GetAll().Where(r => r.HotelId == hotelId).ToList();
            return _SpecificationsEvaluator<Room>.LambdaExpressionsBuilder(rooms, specifications).ToList(); 
        }
        public List<Room> GetRoomsById(int hotelId)
        {
            return _StorageManagementForRooms.GetAll().Where(r => r.HotelId == hotelId).ToList();
        }
        public Room GetRoomById(int hotelId , int roomId)
        {
            return _StorageManagementForRooms.GetAll().FirstOrDefault(r => r.Id == roomId && r.HotelId == hotelId)!;
        }

        
    }
}
