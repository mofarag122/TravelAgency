using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Application.Exceptions;
using TravelAgency.Core.Domain.Entities.Identity;
using TravelAgency.Core.Domain.Repository_Contracts;

namespace TravelAgency.Core.Application._Common
{
    public static class AuthenticationSchema
    {
        
        public static Authentication CheckAuthentication(IAuthenticationRepository authenticationRepository , string? token)
        {
            if (token is null)
                throw new UnAutherized("You Are Not Autherized!");
            Authentication authentication = authenticationRepository.FindUserByToken(token)!;
            if (authentication is null)
                throw new UnAutherized("You Are Not Autherized!");

            return authentication;
        }
    }
}
