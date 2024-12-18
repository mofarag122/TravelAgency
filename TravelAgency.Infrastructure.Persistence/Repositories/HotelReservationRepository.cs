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

        private const string FilePath = "D:\\SDA Project\\TravelAgency\\TravelAgency.Infrastructure.Persistence\\Data Storage\\HotelReservations.json";
        public HotelReservationRepository()
        {
            StorageManagement = new _StorageManagement<HotelReservation>(FilePath);
        }
        public void AddReservation(HotelReservation hotelReservation)
        {
            StorageManagement.Add(hotelReservation);    
        }

        public List<HotelReservation?> GetReservations(int hotelId, int roomId)
        {
            return StorageManagement.GetAll().Where(hr => hr.RoomId == roomId && hr.HotelId == hotelId).ToList()!;
        }

        public void UpdateReservation(HotelReservation hotelReservation)
        {
            StorageManagement.Update(hotelReservation);
        }

        public List<HotelReservation?> GetUserReservations(int userId)
        {
            return StorageManagement.GetAll().Where(r => r.UserId == userId).ToList()!;
        }

        public void DeleteReservation(int reservationId)
        {
            StorageManagement.Delete(reservationId);
        }

        public HotelReservation GetReservation(int reservationId)
        {
            return StorageManagement.GetAll().FirstOrDefault(r => r.Id == reservationId)!;
        }
    }
}
