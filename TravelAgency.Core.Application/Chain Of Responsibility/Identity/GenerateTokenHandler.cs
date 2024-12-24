using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Application.DTOs.Identity;
using TravelAgency.Core.Application.Exceptions;
using TravelAgency.Core.Domain.Entities.Identity;
using TravelAgency.Core.Domain.Repository_Contracts;

namespace TravelAgency.Core.Application.Chain_Of_Responsibility.Identity
{
    internal class GenerateTokenHandler : BaseHandler<string>
    {
        private IAuthenticationRepository _authenticationRepository;

        public GenerateTokenHandler(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }
        public override string Handle(object? TParameter)
        {
            var user = TParameter as User;
            if (user == null)
            {
                throw new ArgumentException("Invalid parameter type. Expected UserToRegisterDto.");
            }

            string token = _authenticationRepository.GetTokenByUserId(user.Id)!;

            if (token is not null)
                 return $"Token: {token}";
            else
            {
                token = Guid.NewGuid().ToString();
                Authentication authentication = new Authentication();
                authentication.UserId = user.Id;
                authentication.Token = token;
                _authenticationRepository.AddAuthentication(authentication);      
            }


            if(handler is not null)
                return handler.Handle(token);
           
            return $"Token: {token}";
        }

    }
}
