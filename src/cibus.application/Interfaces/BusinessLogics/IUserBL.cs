using cibus.application.DTOs;
using cibus.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cibus.application.Interfaces.BusinessLogics
{
    public interface IUserBL
    {
        Task<List<ApplicationUserDto>> Get();
        Task<ApplicationUserDto> Get(int id);
        Task<ApplicationUserDto> Get(string email);
        Task<bool> IsEmailAlreadyExists(string email);

        Task<int> CreateUser(ApplicationUserDto applicationUserDTO);
        Task<Dictionary<int, string>> Authenticate(SigninDto signinDto);

    }
}
