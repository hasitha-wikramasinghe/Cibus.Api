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

namespace cibus.api.Common.Authentication
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
        public async Task<ActionResult<List<ApplicationUserForRetrieveDto>>> Get()
        {
            return Ok(await _userBL.Get());
        }

        [HasPermission(Permission.ReadUser)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUserForRetrieveDto>> Get(int id)
        {
            return Ok(await _userBL.Get(id));
        }
    }
}
