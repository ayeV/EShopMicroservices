﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Data;
using Ordering.Infraestructure.Data;
using Ordering.Infraestructure.Data.Interceptors;

namespace Ordering.Infraestructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor,DispatchDomainEventsInterceptor>();

            services.AddDbContext<ApplicationDbContext>((serviceProvider,options) => 
            {
                options.AddInterceptors(serviceProvider.GetService<ISaveChangesInterceptor>());

                options.UseSqlServer(connectionString);
            });
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            return services;
        }
    }
}
