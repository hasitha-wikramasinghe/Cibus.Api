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
        private readonly IUserBL _userBL;
        public UserController(IUserBL userBL)
        {
            _userBL = userBL;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<ApplicationUserDto>>> Get()
        {
            return Ok(await _userBL.Get());
        }

        [HttpGet("{id}")]
        [HasPermission(Permission.ReadUser)]
        public async Task<ActionResult<ApplicationUserDto>> Get(int id)
        {
            return Ok(await _userBL.Get(id));
        }
    }
}
