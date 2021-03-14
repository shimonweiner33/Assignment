using Assignment.Data.Repository;
using Assignment.Data.Repository.Interface;
using Assignment.Services.Connections;
using Assignment.Services.Posts;
using Assignment.Services.Rooms;
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
            services.AddScoped<IRoomsService, RoomsService>();
            services.AddScoped<IConnectionsService, ConnectionsService>();
            return services;
        }
    }
}
