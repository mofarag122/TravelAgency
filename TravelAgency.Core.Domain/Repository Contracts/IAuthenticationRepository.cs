using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Domain.Entities.Identity;

namespace TravelAgency.Core.Domain.Repository_Contracts
{
    public interface IAuthenticationRepository
    {
        public void AddAuthentication(Authentication authentication);

        public void RemoveAuthentication(int userId);

        public string? GetTokenByUserId(int userId);
        public Authentication? FindUserByToken(string Token);
    }
}
