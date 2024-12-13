using TravelAgency.Core.Domain.Entities.Identity;
using TravelAgency.Core.Domain.Repository_Contracts;
using TravelAgency.Infrastructure.Persistence.Data_Storage;

namespace TravelAgency.Infrastructure.Persistence.Repositories.Identity
{
    public class IdentityRepository : IIdentityRepository
    {
        private StorageManagement<User> StorageManagement;

        private const string FilePath = "D:\\Java\\SoftwareArchitecture\\TravelAgency\\TravelAgency.Infrastructure.Persistence\\Data Storage\\Users.json";

        public IdentityRepository()
        {
            StorageManagement = new StorageManagement<User>(FilePath);
        }

        public bool AddUser(User User)
        {
           return StorageManagement.Add(User);
        }

        public User FindUserByEmail(string Email)
        {
            return StorageManagement.GetAll().FirstOrDefault(e => e.Email == Email) ?? null!;
        }

        public User FindUserByUserName(string UserName)
        {
            return StorageManagement.GetAll().FirstOrDefault(e => e.UserName == UserName) ?? null!;
        }

        public bool CheckEmailWithPassword(string Email, string HashedPassword)
        {
            User User = FindUserByEmail(Email);
            return User.HashedPassword == HashedPassword;   

        }
    }
}
