using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Application.DTOs.Identity;
using TravelAgency.Core.Application.Exceptions;
using TravelAgency.Core.Domain.Entities.Identity;

namespace TravelAgency.Core.Application.Chain_Of_Responsibility.Identity
{
    public class HashPasswordHandler : BaseHandler<User>
    {
        public override User Handle(object? TParameter)
        {
            var userDto = TParameter as UserToRegisterDto;
            if (userDto == null)
            {
                throw new ArgumentException("Invalid parameter type. Expected UserToRegisterDto.");
            }
            var passwordHasher = new PasswordHasher<User>();
            string hashedPassword = passwordHasher.HashPassword(null!, userDto.Password);
            userDto.Password = hashedPassword;  

            if (handler is not null)
                return handler.Handle(TParameter);
            else
                return null!;
        }
    }
}
