using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

using ApiTaskManagement.Database;
using ApiTaskManagement.Repositories;
using ApiTaskManagement.Repositories.Interfaces;
using ApiTaskManagement.BL;
using ApiTaskManagement.BL.Interfaces;
using ApiTaskManagement.Middleware;
using ApiTaskManagement.Services;
using ApiTaskManagement.Services.Interfaces;

namespace ApiTaskManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder);
            ConfigureSwagger(builder);
            ConfigureAuthentication(builder);

            var app = builder.Build();
            ConfigureMiddleware(app);

            app.Run();
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            // Controllers & Swagger
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            // Database
            builder.Services.AddDbContext<TaskManagementDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            // AutoMapper
            builder.Services.AddAutoMapper(typeof(Program));

            // Repositories
            builder.Services.AddScoped<ITaskRepository, TaskRepository>();

            // Business Logic
            builder.Services.AddScoped<ITaskBL, TaskBL>();

            // Query Services
            builder.Services.AddScoped<ITaskService, TaskService>();

            // Contextual services
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
        }

        private static void ConfigureSwagger(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Task Management API", Version = "v1" });

                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Provide your Firebase JWT token like: Bearer {token}",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
            });
        }

        private static void ConfigureAuthentication(WebApplicationBuilder builder)
        {
            builder.Services.AddFirebaseAuthentication(builder.Configuration);
        }

        private static void ConfigureMiddleware(WebApplication app)
        {
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
