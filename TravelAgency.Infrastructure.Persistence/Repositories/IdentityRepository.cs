using Microsoft.Extensions.Configuration;
using TravelAgency.Core.Domain.Entities.Identity;
using TravelAgency.Core.Domain.Repository_Contracts;
using TravelAgency.Infrastructure.Persistence.Data_Storage;

namespace TravelAgency.Infrastructure.Persistence.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private _StorageManagement<User> _StorageManagement;
        public IdentityRepository(IConfiguration configuration)
        {
            string filePath = configuration["FileStorage:UsersFilePath"]!;
            _StorageManagement = new _StorageManagement<User>(filePath);
        }

        public bool AddUser(User user)
        {
            return _StorageManagement.Add(user);
        }
        public void UpdateUser(User user)
        {
            _StorageManagement.Update(user);
        }
        public User FindUserByEmail(string email)
        {
            return _StorageManagement.GetAll().FirstOrDefault(e => e.Email == email) ?? null!;
        }
        public User FindUserByUserName(string userName)
        {
            return _StorageManagement.GetAll().FirstOrDefault(e => e.UserName == userName) ?? null!;
        }
        public User FindUserByPhoneNumber(string phoneNumber)
        {
            return _StorageManagement.GetAll().FirstOrDefault(e => e.PhoneNumber == phoneNumber) ?? null!;
        }
        public User FindUserById(int userId)
        {
            return _StorageManagement.GetById(userId);
        }
        public bool CheckEmailWithPassword(string email, string hashedPassword)
        {
            User User = FindUserByEmail(email);
            return User.HashedPassword == hashedPassword;
        }
    }

}
