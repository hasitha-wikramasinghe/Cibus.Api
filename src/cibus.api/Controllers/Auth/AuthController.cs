using cibus.application.DTOs;
using cibus.application.Interfaces.BusinessLogics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cibus.api.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserBL _userBL;
        public AuthController(IUserBL userBL)
        {
            _userBL = userBL;
        }

        [HttpPost("signup")]
        public async Task<ActionResult> Register(ApplicationUserForCreationDto appUserDto)
        {
            // 0 - Notfound || 1 - found
            int isEmailExists = await _userBL.IsEmailAlreadyExists(appUserDto.Email);
            if (isEmailExists == 1) return BadRequest(new { Message = "Email is already taken, Please try again with another email." });
            int result = await _userBL.CreateUser(appUserDto); // result should be new users's id 
            if (result > 0)
            {
                return Ok(new { CreatedUserId = result });
            }

            return new ObjectResult(new { Message = "Something went wrong, Please try again later." });
        }

        [HttpPost("signin")]
        public async Task<ActionResult> Signin(SigninDto signinDto)
        {

            var validator = await _userBL.Authenticate(signinDto);

            if (validator.ContainsKey(-1)) return BadRequest(new { Message = validator[-1] });
            if (validator.ContainsKey(0)) return BadRequest(new { Message = validator[0] });

            return Ok(new { Token = validator[1] });
        }
    }
}
