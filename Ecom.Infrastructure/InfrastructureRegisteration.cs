using Ecom.Core.Interfaces;
using Ecom.Infrastructure.Data;
using Ecom.Infrastructure.Repositries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Text;
using Ecom.Infrastructure.Repositries.service;
using Ecom.Core.Services;
using Microsoft.Extensions.FileProviders;

namespace Ecom.Infrastructure
{
    public static class InfrastructureRegisteration
    {
        public static IServiceCollection InfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            {
                //services.AddScoped(typeof(IGenericRepositores<>), typeof(GenericRepositories<>));
                //services.AddScoped<ICategoryRepositores, CategoryRepositores>();
                //services.AddScoped<IProductRepository, ProductRepository>();
                //services.AddScoped<IPhotoRepository, PhotoRepository>();
                //apply UnitOfWork pattern
                services.AddScoped<IUnitOfWork, UnitOfWork>();
                services.AddSingleton<IManageService, ManageService>();
                services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot")));
                //apply DbContext
                services.AddDbContext<AppDbContext>(op =>
                {
                    op.UseSqlServer(configuration.GetConnectionString("EcomDatabase"));
                }); 
                return services;
            }
        }
    }
}
