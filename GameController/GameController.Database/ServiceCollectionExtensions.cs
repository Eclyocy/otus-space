﻿using GameController.Database.Interfaces;
using GameController.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GameController.Database
{
    /// <summary>
    /// Extensions for <see cref="IServiceCollection"/> for database.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register database services.
        /// </summary>
        public static IServiceCollection AddDatabase(
            this IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options =>
                options.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=postgresmaster;Database=postgres;"));

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ISessionRepository, SessionRepository>();

            return services;
        }
    }
}