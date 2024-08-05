using API.Data;
using API.Service.Impl;
using API.Service;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration config) 
        {
            services.AddControllers();
            services.AddDbContext<DataContext>(otp =>
            {
                otp.UseSqlServer(config.GetConnectionString("DefaultConnections"));
            });

            services.AddCors();

            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
