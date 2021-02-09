using AutoMapper;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Service;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BookStore.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IImageUpload, ImageUploadService>();


            return services;
        }
    }
}
