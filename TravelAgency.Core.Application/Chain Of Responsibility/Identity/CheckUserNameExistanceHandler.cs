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
    public class CheckUserNameExistanceHandler : BaseHandler<User>
    {
        private IIdentityRepository _identityRepository;

        public CheckUserNameExistanceHandler(IIdentityRepository identityRepository)
        {
            _identityRepository = identityRepository;
        }

        public override User Handle(object? TParameter)
        {
            var userDto = TParameter as UserToRegisterDto;
            if (userDto == null)
            {
                throw new ArgumentException("Invalid parameter type. Expected UserToRegisterDto.");
            }

            if (_identityRepository.FindUserByUserName(userDto.UserName) is not null)
                throw new BadRequest("UserName already Exists");

            if (handler is not null)
                return handler.Handle(TParameter);
            else
                return null!;
        }

    }
}
