using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Domain.Entities.Hotel_Reservation;
using TravelAgency.Core.Domain.Repository_Contracts;
using TravelAgency.Infrastructure.Persistence.Data_Storage;

namespace TravelAgency.Infrastructure.Persistence.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private _StorageManagement<Hotel> StorageManagement;

        private const string FilePath = "C:\\Users\\Asus\\Source\\Repos\\TravelAgency\\TravelAgency.Infrastructure.Persistence\\Data Storage\\Hotels.json";

        public HotelRepository()
        {
            StorageManagement = new _StorageManagement<Hotel>(FilePath);
        }

        public void AddHotel(Hotel hotel)
        {
            StorageManagement.Add(hotel);   
        }

        public List<Hotel> GetAllHotels()
        {
            return StorageManagement.GetAll();
        }
    }
}
