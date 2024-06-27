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

namespace cibus.infrastructure.Authentication
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly string _secretKey;
        private readonly string _audience;
        private readonly string _issuer;

        public TokenService(
            IConfiguration configuration)
        {
            _configuration = configuration;
            _secretKey = _configuration.GetSection("Authentication:Jwt_Secret").Value;
            _audience = _configuration.GetSection("Authentication:Audience").Value;
            _issuer = _configuration.GetSection("Authentication:Issuer").Value;
        }

        public string GenerateToken(
            int userId)
        {
            IdentityOptions _options = new IdentityOptions();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("userId", userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                Audience = _audience,
                Issuer = _issuer,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }
    }
}
