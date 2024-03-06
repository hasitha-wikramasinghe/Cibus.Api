using cibus.application.Interfaces.Services;
using cibus.domain.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace cibus.infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly string _secretKey;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
            _secretKey = _configuration.GetSection("ApplicationSettings:Jwt_Secret").ToString();
        }

        public string GenerateToken(UserRolesViewModel userRolesViewModel)
        {
            var roleId = userRolesViewModel.RoleId;
            IdentityOptions _options = new IdentityOptions();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new ("UserId", userRolesViewModel.UserId.ToString()),
                    new ("RoleId", userRolesViewModel.RoleId.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)), SecurityAlgorithms.HmacSha256)
            };
            var TokenHandler = new JwtSecurityTokenHandler();
            var securityToken = TokenHandler.CreateToken(tokenDescriptor);
            var token = TokenHandler.WriteToken(securityToken);
            return token;
        }
    }
}
