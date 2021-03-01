using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Assignment.Data.Repository;
//using Assignment.Data.Repository.Interface;

namespace Assignment.Data
{
    public static class Startup
    {

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            //services.AddSingleton<, >();



            return services;
        }
    }
}
