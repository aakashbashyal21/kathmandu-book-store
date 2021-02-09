using BookStore.Application.Common.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
           
            if (configuration.GetValue<bool>("UseSqlLite") == true)
            {
                services.AddDbContext<BookDbContext>(options =>
                        options.UseSqlite(configuration.GetConnectionString("BookStoreInMemory")));
            }
            else
            {
                services.AddDbContext<BookDbContext>(options =>
                            options.UseSqlServer(
                                configuration.GetConnectionString("BookStoreDbConnection"),
                                b => b.MigrationsAssembly(typeof(BookDbContext).Assembly.FullName)));
            }



            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<BookDbContext>());


            services.AddDefaultIdentity<ApplicationUser>()
                    .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<BookDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication();

            services.AddAuthorization();

            //services.AddIdentityServer()
            //    .AddApiAuthorization<ApplicationUser, BookDbContext>();

            services.AddTransient<IDateTime, DateTimeService>();


            return services;


        }
    }
}
