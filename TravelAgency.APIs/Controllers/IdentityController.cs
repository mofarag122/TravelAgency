using Microsoft.AspNetCore.Mvc;
using TravelAgency.Core.Application.DTOs.Identity;
using TravelAgency.Core.Application.DTOs.Notification;
using TravelAgency.Core.Application.Service_Contracts;
using TravelAgency.Core.Domain.Entities.Identity;

namespace TravelAgency.APIs.Controllers
{
    public class AccountController : BaseApiController
    {
        private IIdentityService _identityService;
        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }


        [HttpPost("register")] // POST: /api/Account/register
        public ActionResult<User> register(UserToRegisterDto userDto)
        {
            User user = _identityService.Register(userDto);
            return Ok(user);
        }

        [HttpGet("login")] // GET: /api/Account/login
        public ActionResult<string> Login(UserToLoginDto userDto)
        {
            return Ok(_identityService.Login(userDto));
        }
        
        [HttpGet("reset")] // GET: /api/Account/reset
        public async Task<ActionResult<NotificationToResetPasswordDto>> ResetPassword(UserToResetPasswordDto userDto)
        {
            return Ok(await _identityService.ResetPassword(userDto));
        }

        [HttpPost("logout")] // POST: /api/Account/logout
        public ActionResult<string> Logout(string? token)
        {
            return Ok(_identityService.Logout(token));
        }
    }
}
