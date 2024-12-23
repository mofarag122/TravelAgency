using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TravelAgency.Core.Domain.Data_Storage_Initializer_Contract;
using TravelAgency.Core.Domain.Entities.Hotel_Reservation;
using TravelAgency.Core.Domain.Repository_Contracts;

namespace TravelAgency.Infrastructure.Persistence.Data_seeding
{
    public class DataStorageInitializer : IDataStorageInitializer
    {
        private readonly IHotelRepository _hotelRepository;

        public DataStorageInitializer(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public void Seed()
        {
            // Seed hotels
            List<Hotel> hotels = _hotelRepository.GetAllHotels();
            if (!hotels.Any())
            {
                string hotelSeedDataPath = "D:\\SDA Project\\TravelAgency\\TravelAgency.Infrastructure.Persistence\\Data seeding\\HotelSeedData.json";

                if (!File.Exists(hotelSeedDataPath))
                    return;

                string hotelSeedData = File.ReadAllText(hotelSeedDataPath);

                var hotelsData = JsonSerializer.Deserialize<List<Hotel>>(hotelSeedData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (hotelsData == null || !hotelsData.Any())
                    return;

                foreach (var hotelData in hotelsData)
                {
                    _hotelRepository.AddHotel(hotelData);
                }

                hotels = _hotelRepository.GetAllHotels();
            }

            // Seed rooms
            bool hasRoomData = hotels.Any(hotel => _hotelRepository.GetRoomsById(hotel.Id).Any());
            if (!hasRoomData)
            {
                string roomSeedDataPath = "D:\\SDA Project\\TravelAgency\\TravelAgency.Infrastructure.Persistence\\Data seeding\\RoomSeedData.json";

                if (!File.Exists(roomSeedDataPath))
                    return;

                string roomSeedData = File.ReadAllText(roomSeedDataPath);

                var roomsData = JsonSerializer.Deserialize<List<Room>>(roomSeedData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (roomsData == null || !roomsData.Any())
                    return;

                foreach (var room in roomsData)
                {
                    // Add the room directly as it contains the HotelId
                    _hotelRepository.AddRoom(room);
                }
            }
        }
    }
}
