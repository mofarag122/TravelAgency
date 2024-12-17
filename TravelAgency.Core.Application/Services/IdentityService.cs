using Microsoft.AspNetCore.Identity;
using TravelAgency.Core.Application.DTOs.Identity;
using TravelAgency.Core.Application.DTOs.Notification;
using TravelAgency.Core.Application.Exceptions;
using TravelAgency.Core.Application.Service_Contracts;
using TravelAgency.Core.Domain.Entities.Identity;
using TravelAgency.Core.Domain.Entities.Notification;
using TravelAgency.Core.Domain.Repository_Contracts;

namespace TravelAgency.Core.Application.Services
{
    public class IdentityService : IIdentityService
    {
        private IIdentityRepository _identityRepository;
        private IAuthenticationRepository _authenticationRepository;
        private INotificationRepository _notificationRepository;
        public IdentityService(IIdentityRepository identityRepository , IAuthenticationRepository authenticationRepository, INotificationRepository notificationRepository)
        {
            _identityRepository = identityRepository;
            _authenticationRepository = authenticationRepository;
            _notificationRepository = notificationRepository;
        }

        public User Register(UserToRegisterDto userDto)
        {
            // To Register We Follow The Business Rules:
            /*
             * 1. Validate The Dto Got From The Request Body (Done Using Fluent Validation)
             * 2. Check if Email, UserName Does Not Exist Before
             * 3. Hash The Password
             * 4. Mapping From UserDto -> User
             * 5. Save The User Data in Data Storage
             */

            if (userDto is null)
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


            if (_identityRepository.AddUser(user))
                return user;
            else
                throw new BadRequest("Invalid Registration!");
        }

        public string Login(UserToLoginDto userDto)
        {
            // To Login We Follow These Business Rules:
            /*
             * 1. Validate The Dto Got From The Request Body (Done Using Fluent Validation)
             * 2. Check if Email with Hashed Password are Matching
             * 3. Mapping From User -> UserDto
             * 4. generate Token(GUID) and return 
             */

            User user = _identityRepository.FindUserByEmail(userDto.Email);

            if (user is null)
                throw new BadRequest("Invalid Login");

            string token = _authenticationRepository.GetTokenByUserId(user.Id)!;

            if(token is not null)
                return $"Token: {token}";

            var passwordHasher = new PasswordHasher<User>();
            PasswordVerificationResult verificationResult = passwordHasher.VerifyHashedPassword(user, user.HashedPassword, userDto.Password);

            if (verificationResult != PasswordVerificationResult.Success)
                throw new BadRequest("Invalid Login");

            token = Guid.NewGuid().ToString();
           
            Authentication authentication  = new Authentication();
            authentication.UserId = user.Id;    
            authentication.Token = token;
            _authenticationRepository.AddAuthentication(authentication);


            return $"Token: {token}";
        }

        public async Task<NotificationToResetPasswordDto> ResetPassword(UserToResetPasswordDto userDto)
        {
            // To reset Password We Follow The Business Rules:
            /*
             * 1. Check if User Send Either Email or Phone Number   
             * 2. Check if Email or Phone Number  Exists 
             * 3. Make a Notification with new Password  
             * 4. Update User Password
             * 5. return The Notification Content as a reponse
             */


            User user;

            if (userDto.Email is null && userDto.PhoneNumber is null)
                throw new BadRequest("Enter Either Email or Your Phone Number.");
            else if (userDto.Email is not null)
            {
                user = _identityRepository.FindUserByEmail(userDto.Email);
                if (user is null)
                    throw new BadRequest("Email is Not Found!");

            }
            else
            {
                user = _identityRepository.FindUserByPhoneNumber(userDto.PhoneNumber!);
                if (user is null)
                    throw new BadRequest("Phone Number is Not Found!");
            }


            string newPassword = Guid.NewGuid().ToString();
            Notification notification = new Notification()
            {
                UserId = user.Id,
                Recipient = userDto.Email is not null ? userDto.Email : userDto.PhoneNumber,
                Content = $"Dear {user.UserName} , your New Password is {newPassword} .",
                TemplateName = Templates.resetPassword,
                Channel = userDto.Email is not null ? Channels.email : Channels.sms,
            };

           await _notificationRepository.AddNotificationAsync(notification);

            var passwordHasher = new PasswordHasher<User>();
            string hashedPassword = passwordHasher.HashPassword(null!, newPassword);
            user.HashedPassword = hashedPassword;

            _identityRepository.UpdateUser(user);
            _authenticationRepository.RemoveAuthentication(user.Id);

            return new NotificationToResetPasswordDto()
            {
                Content = notification.Content,
                Recipient = notification.Recipient,
                Channel = notification.Channel.ToString(),
            };

        }

        public string Logout(string? token)
        {
            if (token is null)
                throw new UnAutherized("You Are Not Autherized!");

            Authentication authentication = _authenticationRepository.FindUserByToken(token)!;

            if(authentication is null)
                throw new UnAutherized("You Are Not Autherized!");

            _authenticationRepository.RemoveAuthentication(authentication.UserId);

            return "Logout Done Successfully";
        }
    }
}
