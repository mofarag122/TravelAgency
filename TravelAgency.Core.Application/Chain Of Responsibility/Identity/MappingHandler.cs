using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Application.DTOs.Identity;
using TravelAgency.Core.Domain.Entities.Identity;

namespace TravelAgency.Core.Application.Chain_Of_Responsibility.Identity
{
    public class MappingHandler : BaseHandler<User>
    {

        public override User Handle(object? TParameter)
        {
            var userDto = TParameter as UserToRegisterDto;
            if (userDto == null)
            {
                throw new ArgumentException("Invalid parameter type. Expected UserToRegisterDto.");
            }
            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                UserName = userDto.UserName,
                Email = userDto.Email,
                HashedPassword = userDto.Password,
                PhoneNumber = userDto.PhoneNumber,
                Address = new Address
                {
                    Country = userDto.Address.Country,
                    City = userDto.Address.City
                },


            };
            if (userDto.Role.ToLower() == "user")
                user.Role = Roles.user;
            else if (userDto.Role.ToLower() == "admin")
                user.Role = Roles.admin;

            if (handler is not null)
                return handler.Handle(TParameter);
            else
                return user!;
        }
    }
}
