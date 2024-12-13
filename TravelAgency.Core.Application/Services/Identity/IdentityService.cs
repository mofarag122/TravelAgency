using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Application.DTOs.Identity;
using TravelAgency.Core.Application.Exceptions;
using TravelAgency.Core.Application.Service_Contracts;
using TravelAgency.Core.Domain.Entities.Identity;
using TravelAgency.Core.Domain.Repository_Contracts;

namespace TravelAgency.Core.Application.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private IIdentityRepository _identityRepository;

        public IdentityService(IIdentityRepository identityRepository)
        {
            _identityRepository = identityRepository;
        }

        public User Register(UserToRegisterDto userDto)
        {
            // To Register We Follow The Business Rules:
            /*
             * 1.Validate The Dto Got From The Request Body(Transfered As UserToRegisterDto)(Done Using Fluent Validation)
             * 2.Check if Email , User Name Does Not Exist Before
             * 3.Hash The Password
             * 4.Mapping From UserDto -> User
             * 4.Save The User Data in Data Storage
             */

            if(userDto is null)
                throw new BadRequest("You have To Enter The Data");

            if (_identityRepository.FindUserByEmail(userDto.Email) is not null)
                throw new BadRequest("Email already Exists");

            if (_identityRepository.FindUserByUserName(userDto.UserName) is not null)
                throw new BadRequest("UserName already Exists");

            
            var passwordHasher = new PasswordHasher<User>();
            string hashedPassword = passwordHasher.HashPassword(null!, userDto.Password);
           
            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                UserName = userDto.UserName,
                Email = userDto.Email,
                HashedPassword = hashedPassword,  
                Address = new Address
                {
                    Country = userDto.Address.Country,
                    City = userDto.Address.City
                },
                Role = userDto.Role 
            };

            if (_identityRepository.AddUser(user))
                return user;
            else
                throw new BadRequest("Invalid Registeration!");

        }
        public bool Login(UserToLoginDto userDto)
        {
            throw new NotImplementedException();
        }
       
    }
}
