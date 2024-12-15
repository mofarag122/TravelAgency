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
    public class HotelReservationRepository : IHotelReservationRepository
    {
        private _StorageManagement<HotelReservation> StorageManagement;

        private const string FilePath = "C:\\Users\\Asus\\Source\\Repos\\TravelAgency\\TravelAgency.Infrastructure.Persistence\\Data Storage\\HotelReservations.json";
        public HotelReservationRepository()
        {
            StorageManagement = new _StorageManagement<HotelReservation>(FilePath);
        }
        public void AddHotelReservation(HotelReservation hotelReservation)
        {
            StorageManagement.Add(hotelReservation);    
        }
    }
}
