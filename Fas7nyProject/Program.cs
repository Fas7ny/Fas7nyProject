using Fas7ny.Application.Services.JwtService.Extensions;
using Fas7ny.Application.Services.JwtService.Settings;
using Fas7ny.Domain.Entities;
using Fas7ny.Domain.Repo;
using Fas7ny.Domain.RepoInterfaces;
using Fas7ny.Infrastructure.Repo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TourismApp.Data;

namespace Fas7nyProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Controllers
            builder.Services.AddControllers();

            // Swagger / OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // DbContext
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new Exception("DefaultConnection is missing. Check User Secrets or appsettings.json");
            }

            builder.Services.AddDbContext<TourismDbContext>(options =>
                options.UseNpgsql(connectionString)
            );

            // Identity
            builder.Services
                .AddIdentity<User, IdentityRole<Guid>>(options =>
                {
                    options.Password.RequiredLength = 8;
                    options.Password.RequireDigit = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = true;
                })
                .AddEntityFrameworkStores<TourismDbContext>()
                .AddDefaultTokenProviders();

            // Unit Of Work
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Generic Repository
            builder.Services.AddScoped(
                typeof(IGenericRepository<>),
                typeof(GenericRepository<>)
            );

            // JWT Settings
            builder.Services.Configure<JwtSettings>(
                builder.Configuration.GetSection("Jwt")
            );

            // ✅ JWT Authentication (المهم)
            builder.Services.AddJwtAuthentication(builder.Configuration);

            var app = builder.Build();

            // Middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // ✅ مهم جدًا
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}
