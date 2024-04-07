﻿using AutoMapper;
using cibus.application.BusinessLogics;
using cibus.application.Common.Authentication;
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
            service.AddScoped<IUserBL, UserBL>();
            service.AddScoped<IPermissionBL, PermissionBL>();
            #endregion

            #region Repositories
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IPermissionRepository, PermissionRepository>();
            #endregion 

            return service;
        }
    }
}
