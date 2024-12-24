using Microsoft.Extensions.Configuration;
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
        private _StorageManagement<HotelReservation> _StorageManagement;
        public HotelReservationRepository(IConfiguration configuration)
        {
            string filePath = configuration["FileStorage:HotelReservationsFilePath"]!;
            _StorageManagement = new _StorageManagement<HotelReservation>(filePath);
        }
      
        public void AddReservation(HotelReservation hotelReservation)
        {
            _StorageManagement.Add(hotelReservation);    
        }
        public List<HotelReservation?> GetReservations(int hotelId, int roomId)
        {
            return _StorageManagement.GetAll().Where(hr => hr.RoomId == roomId && hr.HotelId == hotelId).ToList()!;
        }
        public void UpdateReservation(HotelReservation hotelReservation)
        {
            _StorageManagement.Update(hotelReservation);
        }
        public List<HotelReservation?> GetUserReservations(int userId)
        {
            return _StorageManagement.GetAll().Where(r => r.UserId == userId).ToList()!;
        }
        public void DeleteReservation(int reservationId)
        {
            _StorageManagement.Delete(reservationId);
        }
        public HotelReservation GetReservation(int reservationId)
        {
            return _StorageManagement.GetAll().FirstOrDefault(r => r.Id == reservationId)!;
        }
    }
}
