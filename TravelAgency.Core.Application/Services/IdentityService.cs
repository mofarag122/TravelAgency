using Microsoft.AspNetCore.Identity;
using TravelAgency.Core.Application._Common;
using TravelAgency.Core.Application.Chain_Of_Responsibility;
using TravelAgency.Core.Application.Chain_Of_Responsibility.Identity;
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
            // To Register We Follow These Business Rules:
            /*
             * 1. Validate The Dto Got From The Request Body (Done Using Data Annotations)
             * 2. Check if Email, UserName and PhoneNumebr do not Exist Before
             * 3. Hash The Password
             * 4. Mapping From UserDto -> User
             * 5. Save The User Data in Data Storage
             */


            IHandler<User> checkEmailExistance = new CheckEmailExistanceHandler(_identityRepository);
            IHandler<User> checkUserNameExistance = new CheckUserNameExistanceHandler(_identityRepository);
            IHandler<User> checkPhoneNumberExistance = new CheckPhoneNumberExistanceHandler(_identityRepository);
            IHandler<User> hashPassword = new HashPasswordHandler();
            IHandler<User> mapping = new MappingHandler();

            checkEmailExistance.SetNext(checkUserNameExistance);
            checkUserNameExistance.SetNext(checkPhoneNumberExistance);
            checkPhoneNumberExistance.SetNext(hashPassword);
            hashPassword.SetNext(mapping);

            User user = checkEmailExistance.Handle(userDto);
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
             * 4. Generate Token(GUID) and return 
             */

            IHandler<string> checkEmailValidity = new CheckEmailValidityHandler(_identityRepository);
            IHandler<string> passwordVerification = new PasswordVerificationHandler(_identityRepository);
            IHandler<string> returnToken = new GenerateTokenHandler(_authenticationRepository);

            checkEmailValidity.SetNext(passwordVerification);
            passwordVerification.SetNext(returnToken);
            return checkEmailValidity.Handle(userDto);

           
        }
        public async Task<NotificationToResetPasswordDto> ResetPassword(UserToResetPasswordDto userDto)
        {
            // To reset Password We Follow These Business Rules:
            /*
             * 1. Check if User Sent Either Email or Phone Number   
             * 2. Check if Email or PhoneNumber  Exists 
             * 3. Make a Notification with new Password  
             * 4. Update User Password
             * 5. return The Notification Content as a response
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
            // To Logout We Follow Theese Business Rules:
            /*
             * 1. Check if User Authenticated   
             * 2. Remove The Token            
            */

            Authentication authentication =  AuthenticationSchema.CheckAuthentication(_authenticationRepository, token);
            _authenticationRepository.RemoveAuthentication(authentication.UserId);
            return "Logout Done Successfully";
        }
    }
}
