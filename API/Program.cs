using API.Extensions;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddApplicationService(builder.Configuration);
            builder.Services.AddIdentityService(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200"));

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
