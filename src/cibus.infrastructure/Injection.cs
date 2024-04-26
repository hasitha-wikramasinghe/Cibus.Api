using cibus.application.Interfaces.Context;
using cibus.application.Interfaces.Services;
using cibus.domain.Entities;
using cibus.infrastructure.Authentication;
using cibus.infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cibus.infrastructure
{
    public static class Injection
    {
        public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection service, IConfiguration configuration)
        {
            // Dependency injection for Services
            service.AddScoped<ITokenService, TokenService>();
            service.AddScoped<IPasswordSecurityService, PasswordSecurityService>();

            // Dependency injection for contexts
            service.AddSingleton<IDapperContext, DapperContext>();

            // Auth
            service.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
            service.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

            return service;
        }
    }
}
