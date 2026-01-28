using Fas7ny.Application.Options;
using Fas7ny.Application.Services.JwtService.Extensions;
using Fas7ny.Application.Services.JwtService.Settings;
using Fas7ny.Application.Services.OpenAiService;
using Fas7ny.Application.Services.Payment;
using Fas7ny.Application.ServivesInterfaces;
using Fas7ny.Domain.Entities;
using Fas7ny.Domain.Repo;
using Fas7ny.Domain.RepoInterfaces;
using Fas7ny.Infrastructure.ExternalApis;
using Fas7ny.Infrastructure.Repo;
using Fas7nyProject.Presentation.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;
using TourismApp.Data;
using Fas7ny.Infrastructure.Data.SeedData;


namespace Fas7nyProject
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ========================================
            // 1. Controllers
            // ========================================
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                });

            // ========================================
            // 2. Swagger / OpenAPI
            // ========================================
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Fas7ny Tourism API",
                    Version = "v1",
                    Description = "Tourism platform with AI, Payment, and Search integration",
                    Contact = new OpenApiContact
                    {
                        Name = "Yousef Walid",
                        Email = "support@fas7ny.com"
                    }
                });

                // JWT Authentication in Swagger
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your JWT token"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
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

            // ========================================
            // 3. External API Services Configuration
            // ========================================

            // Geoapify Search Service
            builder.Services.Configure<GeoapifyOptions>(
                builder.Configuration.GetSection("Geoapify"));
            builder.Services.AddHttpClient<IGeoapifySearchService, GeoapifySearchService>();

            // Mapbox Search Service
            builder.Services.Configure<MapboxOptions>(
                builder.Configuration.GetSection("Mapbox"));
            builder.Services.AddHttpClient<IMapboxSearchService, MapboxSearchService>();

            

            // OpenAI Service
            builder.Services.Configure<OpenAIOptions>(
                builder.Configuration.GetSection("OpenAI"));
            builder.Services.AddScoped<IAiService, AiService>();

            // Paymob Payment Service
            builder.Services.Configure<PaymobOptions>(
                builder.Configuration.GetSection("Paymob"));
            builder.Services.AddHttpClient<IPaymobService, PaymobService>(client =>
            {
                client.Timeout = TimeSpan.FromSeconds(30);
            });

            // ========================================
            // 4. Database Context
            // ========================================
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("DefaultConnection is missing. Check User Secrets or appsettings.json");
            }

            builder.Services.AddDbContext<TourismDbContext>(options =>
                options.UseNpgsql(connectionString,
                    npgsqlOptions => npgsqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorCodesToAdd: null))
            );

            // ========================================
            // 5. Identity Configuration
            // ========================================
            builder.Services
                .AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    // Password settings
                    options.Password.RequiredLength = 8;
                    options.Password.RequireDigit = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = true;

                    // Lockout settings
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers = true;

                    // User settings
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<TourismDbContext>()
                .AddDefaultTokenProviders();

            // ========================================
            // 6. Repository Pattern
            // ========================================
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // ========================================
            // 7. JWT Configuration
            // ========================================
            builder.Services.Configure<JwtSettings>(
                builder.Configuration.GetSection("Jwt"));

            builder.Services.AddJwtAuthentication(builder.Configuration);

            // ========================================
            // 8. CORS Configuration
            // ========================================
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });

                // Production CORS (more restrictive)
                options.AddPolicy("Production", policy =>
                {
                    policy.WithOrigins("https://yourdomain.com")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
            });

            // ========================================
            // 9. Logging Configuration
            // ========================================
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            // ========================================
            // Build Application
            // ========================================
            var app = builder.Build();

            //Add seed file 
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TourismDbContext>();

                await context.Database.MigrateAsync();

            }


            // ========================================
            // Middleware Pipeline
            // ========================================

            // 1. Exception Handling (Must be FIRST!)
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            // 2. Swagger (Development only)
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Fas7ny API v1");
                    options.RoutePrefix = "swagger";
                });
            }

            // 3. HTTPS Redirection
            app.UseHttpsRedirection();

            // 4. CORS
            if (app.Environment.IsDevelopment())
            {
                app.UseCors("AllowAll");
            }
            else
            {
                app.UseCors("Production");
            }

            // 5. Routing
            app.UseRouting();

            // 6. Authentication & Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            // 7. Map Controllers
            app.MapControllers();

            // ========================================
            // Default Routes
            // ========================================
            app.MapGet("/", () => Results.Ok(new
            {
                message = "Welcome to Fas7ny Tourism API",
                version = "1.0.0",
                documentation = "/swagger",
                status = "Running",
                timestamp = DateTime.UtcNow
            })).AllowAnonymous();

            app.MapGet("/health", () => Results.Ok(new
            {
                status = "Healthy",
                database = "PostgreSQL",
                environment = app.Environment.EnvironmentName,
                timestamp = DateTime.UtcNow
            })).AllowAnonymous();

            // ========================================
            // Database Migration & Seeding
            // ========================================
            if (app.Environment.IsDevelopment())
            {
                using var scope = app.Services.CreateScope();
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<TourismDbContext>();
                    var logger = services.GetRequiredService<ILogger<Program>>();

                    logger.LogInformation("Applying database migrations...");
                    await context.Database.MigrateAsync();
                    logger.LogInformation("Database migrations applied successfully");

                    // Seed data is applied automatically via OnModelCreating
                    logger.LogInformation("Database seeded successfully");
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating or seeding the database");
                    throw;
                }
            }

            // ========================================
            // Run Application
            // ========================================
            app.Run();
        }
    }
}