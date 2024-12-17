using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TravelAgency.Core.Domain.Entities.Hotel_Reservation;
using TravelAgency.Core.Domain.Repository_Contracts;
using TravelAgency.Infrastructure.Persistence.Data_Storage;

namespace TravelAgency.Infrastructure.Persistence.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private _StorageManagement<Hotel> StorageManagementForHotels;
        private _StorageManagement<Room> StorageManagementForRooms;

        private const string HotelsFilePath = "C:\\Users\\Asus\\Source\\Repos\\TravelAgency\\TravelAgency.Infrastructure.Persistence\\Data Storage\\Hotels.json";
        private const string RoomsFilePath = "C:\\Users\\Asus\\Source\\Repos\\TravelAgency\\TravelAgency.Infrastructure.Persistence\\Data Storage\\Rooms.json";

        public HotelRepository()
        {
            StorageManagementForHotels = new _StorageManagement<Hotel>(HotelsFilePath);
            StorageManagementForRooms  =  new _StorageManagement<Room>(RoomsFilePath);
        }

        public List<Hotel> GetAllHotels()
        {
            return StorageManagementForHotels.GetAll();
        }

        public List<Hotel> GetAllHotelsWithRooms()
        {
            List<Room> rooms = StorageManagementForRooms.GetAll();
            List<Hotel> hotels = StorageManagementForHotels.GetAll();

            foreach (var hotel in hotels)
            {
                hotel.Rooms = rooms.Where(room => room.HotelId == hotel.Id).ToList();
            }

            return hotels;
        }

        public List<Room> GetRoomsById(int hotelId)
        {
            return StorageManagementForRooms.GetAll().Where(r => r.HotelId == hotelId).ToList();
        }

        public Room GetRoomById(int hotelId , int roomId)
        {
            return StorageManagementForRooms.GetAll().FirstOrDefault(r => r.Id == roomId && r.HotelId == hotelId)!;
        }


    }
}
