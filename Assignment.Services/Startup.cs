using Assignment.Data.Repository;
using Assignment.Data.Repository.Interface;
using Assignment.Services.Posts;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Assignment.Services
{
    public static class Startup
    {

        public static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            services.AddScoped<IPostsRepository, PostsRepository>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IAcountService, AcountService>();
            services.AddScoped<IPostsService, PostsService>();
            return services;
        }
    }
}
