#nullable enable

using cibus.application.DTOs;
using cibus.domain.Entities;
using cibus.domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cibus.application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<List<ApplicationUser>?> Get();
        Task<ApplicationUser?> Get(int id);
        Task<ApplicationUser?> Get(string email);

        Task<int> CreateUser(ApplicationUser appUser);
        Task<bool> IsEmailAlreadyExists(string email);
    }
}
