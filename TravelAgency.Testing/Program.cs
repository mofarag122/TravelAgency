using System.Data.SqlTypes;
using TravelAgency.Core.Domain.Entities.Identity;
using TravelAgency.Core.Domain.Repository_Contracts;
using TravelAgency.Infrastructure.Persistence.Data_Storage;
using TravelAgency.Infrastructure.Persistence.Repositories.Identity;

namespace TravelAgency.Testing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StorageManagement<User> storageManagement = new StorageManagement<User>("D:\\Java\\SoftwareArchitecture\\TravelAgency\\TravelAgency.Infrastructure.Persistence\\Data Storage\\Users.json");

            User user = new User()
            {
                Id = 8,
                FirstName = "Omar",
                LastName = "Hatem",
                Email = "AnasAraby@gmail.com",
                HashedPassword = "AnasPassword",
                Address = new Address()
                {
                    Country = "Egypt",
                    City = "Cairo"

                },
                Role = "user"
                
            };

            storageManagement.Update(user);
            storageManagement.Delete(1);
            
        }
    }
}
