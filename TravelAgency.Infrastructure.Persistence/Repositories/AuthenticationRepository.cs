using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Domain.Entities.Identity;
using TravelAgency.Core.Domain.Repository_Contracts;
using TravelAgency.Infrastructure.Persistence.Data_Storage;

namespace TravelAgency.Infrastructure.Persistence.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private _StorageManagement<Authentication> _StorageManagement;    
        public AuthenticationRepository(IConfiguration configuration)
        {
            string filePath = configuration["FileStorage:AuthenticationsFilePath"]!;
            _StorageManagement = new _StorageManagement<Authentication>(filePath);
        }

        public void AddAuthentication(Authentication authentication)
        {
            _StorageManagement.Add(authentication); 
        }
        public void RemoveAuthentication(int userId) 
        {
            List<Authentication> authentications = _StorageManagement.GetAll();
            Authentication authentication = authentications.FirstOrDefault(u => u.UserId == userId) ?? null!;
            authentications.Remove(authentication);
            _StorageManagement.Save(authentications);
        }
        public string? GetTokenByUserId(int userId)
        {
            Authentication authentication = _StorageManagement.GetAll().FirstOrDefault(u => u.UserId == userId)!;
            if(authentication is null)
                return null!;
            return authentication.Token;
        }
        public Authentication? FindUserByToken(string token)
        {
            return _StorageManagement.GetAll().FirstOrDefault(T => T.Token == token) ?? null!;
        }
    }
}
