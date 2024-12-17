using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TravelAgency.Core.Domain.Data_Storage_Initializer_Contract;
using TravelAgency.Core.Domain.Entities.Hotel_Reservation;
using TravelAgency.Core.Domain.Repository_Contracts;
using TravelAgency.Infrastructure.Persistence.Repositories;

namespace TravelAgency.Infrastructure.Persistence.Data_seeding
{
    public class DataStorageInitializer : IDataStorageInitializer
    {
        private  IHotelRepository _hotelRepository;

        public DataStorageInitializer(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public void Seed()
        {
         
                if (!_hotelRepository.GetAllHotels().Any())
                {
                    
                    string hotelSeedDataPath = "C:\\Users\\Asus\\Source\\Repos\\TravelAgency\\TravelAgency.Infrastructure.Persistence\\Data seeding\\HotelSeedData.json";

                    if (!File.Exists(hotelSeedDataPath))
                        return;
                    

                    string hotelSeedData = File.ReadAllText(hotelSeedDataPath);

                   
                    var hotelsData = JsonSerializer.Deserialize<List<Hotel>>(hotelSeedData, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (hotelsData == null || !hotelsData.Any())
                         return;
                    

                  
                   // foreach (var hotelData in hotelsData)
                    //{
                      //  _hotelRepository.AddHotel(hotelData);
                    //}

                  
                }
              
        }
       
        
    }
}
