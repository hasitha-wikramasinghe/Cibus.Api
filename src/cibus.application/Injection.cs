using AutoMapper;
using cibus.application.BusinessLogics;
using cibus.application.Authentication;
using cibus.application.Interfaces.BusinessLogics;
using cibus.application.Interfaces.Repositories;
using cibus.application.MapProfiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cibus.application
{
    public static class Injection
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection service, IConfiguration configuration)
        {
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DTOProfileMap());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            service.AddSingleton(mapper);

            #region Business Logic
            service.AddScoped<IUserBusinessLogic, UserBusinessLogic>();
            service.AddSingleton<IPermissionBusinessLogic, PermissionBusinessLogic>();
            #endregion

            #region Repositories
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddSingleton<IPermissionRepository, PermissionRepository>();
            #endregion 

            return service;
        }
    }
}
