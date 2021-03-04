using Assignment.Data.Repository;
using Assignment.Data.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Assignment.Services
{
    public static class Startup
    {

        public static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            services.AddScoped<IPostsRepository, PostsRepository>();
            services.AddSingleton<IMemberService, MemberService>();
            services.AddSingleton<IAcountService, AcountService>();
            return services;
        }
    }
}
