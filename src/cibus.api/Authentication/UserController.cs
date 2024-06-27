using cibus.application.DTOs;
using cibus.application.Interfaces.BusinessLogics;
using cibus.infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cibus.api.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusinessLogic _userBusinessLogic;
        public UserController(IUserBusinessLogic userBusinessLogic)
        {
            _userBusinessLogic = userBusinessLogic;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<ApplicationUserDto>>> Get()
        {
            var users = await _userBusinessLogic.Get();

            if (users is null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        [HttpGet("{id}")]
        [HasPermission(Permission.AccessUser)]
        public async Task<ActionResult<ApplicationUserDto>> Get(int id)
        {
            var user = await _userBusinessLogic.Get(id);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
