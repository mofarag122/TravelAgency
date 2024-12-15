﻿using Microsoft.AspNetCore.Mvc;
using TravelAgency.Core.Application.DTOs.Identity;
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
            _identityService.Login(userDto);
            return Ok("Login Done");
        }

    }
}
