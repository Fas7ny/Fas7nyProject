using Fas7ny.Application.Options;
using Fas7ny.Application.ServiceInterfaces;
using Fas7ny.Application.Services.AiService;
using Fas7ny.Application.Services.FileServices;
using Fas7ny.Application.Services.JwtService.Extensions;
using Fas7ny.Application.Services.JwtService.Settings;
using Fas7ny.Application.Services.PaymentService;
using Fas7ny.Application.ServivesInterfaces;
using Fas7ny.Domain.Entities;
using Fas7ny.Domain.Repo;
using Fas7ny.Domain.RepoInterfaces;
using Fas7ny.Infrastructure.Data.SeedData;
using Fas7ny.Infrastructure.ExternalApis;
using Fas7ny.Infrastructure.Repo;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using TourismApp.Data;
using static Fas7ny.Application.ServivesInterfaces.GeoapifyPropertiesIGeoapifySearchService;

namespace Fas7nyProject;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // ================= Controllers =================
        builder.Services.AddControllers()
            .AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.ReferenceHandler =
                    System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                o.JsonSerializerOptions.DefaultIgnoreCondition =
                    System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            });

        // ================= Swagger =================
        builder.Services.AddSwaggerGen(o =>
        {
            o.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Fas7ny API",
                Version = "v1"
            });

            o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header
            });

            o.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        // ================= Cache =================
        builder.Services.AddMemoryCache();

        // ================= External APIs =================
        builder.Services.Configure<GeoapifyOptions>(
            builder.Configuration.GetSection("Geoapify"));
        builder.Services.AddHttpClient<IGeoapifySearchService, GeoapifySearchService>();

        builder.Services.Configure<MapboxOptions>(
            builder.Configuration.GetSection("Mapbox"));
        builder.Services.AddHttpClient<IMapboxSearchService, MapboxSearchService>();

        // ================= Paymob =================
        builder.Services.AddHttpClient<IPaymobService, PaymobService>();

        // ================= AI (Gemini) =================
        builder.Services.Configure<GeminiSettings>(
            builder.Configuration.GetSection("Gemini"));
        builder.Services.AddHttpClient<IAiService, AiService>();

        // ================= Core Services =================
        builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
        builder.Services.AddScoped<IFileService, FileService>();

        // ================= Database =================
        builder.Services.AddDbContext<TourismDbContext>(options =>
        {
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        // ================= Identity =================
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = true;

            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 1;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
        })
        .AddEntityFrameworkStores<TourismDbContext>()
        .AddDefaultTokenProviders();

        // ================= Repositories =================
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        // ================= JWT =================
        builder.Services.Configure<JwtSettings>(
            builder.Configuration.GetSection("Jwt"));
        builder.Services.AddJwtAuthentication(builder.Configuration);

        // ================= CORS =================
        builder.Services.AddCors(o =>
            o.AddPolicy("AllowAll",
                p => p.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()));
        builder.Services.AddHangfire(config =>
        {
            config.UsePostgreSqlStorage(
                builder.Configuration.GetConnectionString("DefaultConnection")
            );
        });

        builder.Services.AddHangfireServer();


        var app = builder.Build();

        // ================= Migrate & Seed =================
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<TourismDbContext>();

            await context.Database.MigrateAsync();
            await SeedData.SeedAsync(
                services.GetRequiredService<UserManager<ApplicationUser>>(),
                services.GetRequiredService<RoleManager<IdentityRole>>());
        }

        // ================= Middleware =================
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
            RequestPath = "/image"
        });

        app.UseHttpsRedirection();
        app.UseCors("AllowAll");

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseHangfireDashboard("/jobs");

        app.MapControllers();
        app.Run();
    }
}
