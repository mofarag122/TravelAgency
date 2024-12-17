using Microsoft.AspNetCore.Mvc;
using TravelAgency.Core.Application.DTOs.Identity;
using TravelAgency.Core.Application.DTOs.Notification;
using TravelAgency.Core.Application.Service_Contracts;
using TravelAgency.Core.Domain.Entities.Identity;

namespace TravelAgency.APIs.Controllers
{
    public class IdentityController : BaseApiController
    {
        private IIdentityService _identityService;


        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        [HttpPost("register")] // POST: /api/Identity/register
        public ActionResult<User> register(UserToRegisterDto userDto)
        {
            User user = _identityService.Register(userDto);
            return Ok(user);
        }
        [HttpGet("login")] // GET: /api/Identity/login
        public ActionResult<string> Login(UserToLoginDto userDto)
        {
            return Ok(_identityService.Login(userDto));
        }
        [HttpGet("reset")] // GET: /api/Identity/reset
        public async Task<ActionResult<NotificationToResetPasswordDto>> ResetPassword(UserToResetPasswordDto userDto)
        {
            return Ok(await _identityService.ResetPassword(userDto));

        }
        [HttpPost("logout")] // POST: /api/Identity/logout
        public ActionResult<string> Logout(string? token)
        {

            return Ok(_identityService.Logout(token));

        }
    }
}
