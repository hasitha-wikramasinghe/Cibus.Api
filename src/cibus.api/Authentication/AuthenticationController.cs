using cibus.application.DTOs;
using cibus.application.Interfaces.BusinessLogics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cibus.api.Authentication
{
    [Route("api/Authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserBL _userBL;
        public AuthenticationController(IUserBL userBL)
        {
            _userBL = userBL;
        }

        [HttpPost("Signup")]
        public async Task<ActionResult> Register(ApplicationUserDto applicationUserDto)
        {
            if (await _userBL.IsEmailAlreadyExists(applicationUserDto.Email)) return BadRequest("Email is already taken, please try with another email.");

            int createdUserId = await _userBL.CreateUser(applicationUserDto);
            if (createdUserId > 0)
            {
                return Ok(new { CreatedUserId = createdUserId });
            }

            return StatusCode(500);
        }

        [HttpPost("Authenticate")]
        public async Task<ActionResult> Authenticate(SigninDto signinDto)
        {

            var validator = await _userBL.Authenticate(signinDto);

            if (validator.ContainsKey(-1) || validator.ContainsKey(0))
            {
                return BadRequest(new
                {
                    Message = "Invalid credentials, try again with valid credentials"
                });
            }

            return Ok(new { Token = validator[1] });
        }
    }
}
