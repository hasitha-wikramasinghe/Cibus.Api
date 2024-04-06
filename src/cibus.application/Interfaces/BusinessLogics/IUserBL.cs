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
        Task<List<ApplicationUserForRetrieveDto>> Get();
        Task<ApplicationUserForRetrieveDto> Get(int id);
        Task<ApplicationUserForRetrieveDto> Get(string email);

        Task<int> CreateUser(ApplicationUserForCreationDto appUserDTO);
        Task<int> IsEmailAlreadyExists(string email);
        Task<Dictionary<int, string>> Authenticate(SigninDto signinDto);

    }
}
