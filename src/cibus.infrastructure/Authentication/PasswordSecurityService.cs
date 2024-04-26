using cibus.application.Interfaces.Services;
using cibus.domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace cibus.infrastructure.Authentication
{
    public class PasswordSecurityService : IPasswordSecurityService
    {
        private readonly IConfiguration _configuration;
        private readonly string key;
        public PasswordSecurityService(IConfiguration configuration)
        {
            _configuration = configuration;
            key = _configuration.GetSection("PasswordEncryptionKey").ToString();
        }

        public Dictionary<int, byte[]> GetPasswordHashAndPasswordSalt(string password)
        {
            using var hmac = new HMACSHA512();

            Dictionary<int, byte[]> passwordEncryptionResult = new Dictionary<int, byte[]>();

            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            var passwordSalt = hmac.Key;

            passwordEncryptionResult.Add(1, passwordHash);
            passwordEncryptionResult.Add(2, passwordSalt);

            return passwordEncryptionResult;
        }

        public bool ValidatePassword(ApplicationUser appUser, string password)
        {
            using var hmac = new HMACSHA512(appUser.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != appUser.PasswordHash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
