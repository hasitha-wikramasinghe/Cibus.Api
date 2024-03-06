using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cibus.infrastructure.dbUpdate
{
    public static class Injection
    {
        public static IServiceCollection RegisterDbUpServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
