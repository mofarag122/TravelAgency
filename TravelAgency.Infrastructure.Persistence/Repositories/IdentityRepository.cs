using TravelAgency.Core.Domain.Entities.Identity;
using TravelAgency.Core.Domain.Repository_Contracts;
using TravelAgency.Infrastructure.Persistence.Data_Storage;

namespace TravelAgency.Infrastructure.Persistence.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private _StorageManagement<User> StorageManagement;

        private const string FilePath = "D:\\SDA Project\\TravelAgency\\TravelAgency.Infrastructure.Persistence\\Data Storage\\Users.json";

        public IdentityRepository()
        {
            StorageManagement = new _StorageManagement<User>(FilePath);
        }

        public bool AddUser(User User)
        {
            return StorageManagement.Add(User);
        }

        public void UpdateUser(User user)
        {
            StorageManagement.Update(user);
        }
        public User FindUserByEmail(string Email)
        {
            return StorageManagement.GetAll().FirstOrDefault(e => e.Email == Email) ?? null!;
        }

        public User FindUserByUserName(string UserName)
        {
            return StorageManagement.GetAll().FirstOrDefault(e => e.UserName == UserName) ?? null!;
        }
        public User FindUserByPhoneNumber(string PhoneNumber)
        {
            return StorageManagement.GetAll().FirstOrDefault(e => e.PhoneNumber == PhoneNumber) ?? null!;
        }


        public bool CheckEmailWithPassword(string Email, string HashedPassword)
        {
            User User = FindUserByEmail(Email);
            return User.HashedPassword == HashedPassword;

        }

        public User FindUserById(int userId)
        {
           return StorageManagement.GetById(userId);
        }
    }
}
