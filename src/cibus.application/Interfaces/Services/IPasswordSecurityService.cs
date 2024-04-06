using cibus.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cibus.application.Interfaces.Services
{
    public interface IPasswordSecurityService
    {
        public Dictionary<int, byte[]> GetPasswordHashAndPasswordSalt(string password);
        bool ValidatePassword(ApplicationUser appUser, string password);
    }
}
