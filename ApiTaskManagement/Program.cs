
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

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

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<TaskManagementDbContext>(options=> options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ITaskRepository, TaskRepository>();
            builder.Services.AddScoped<ITaskBL, TaskBL>();
            builder.Services.AddScoped<ITaskService, TaskService>();

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddFirebaseAuthentication(builder.Configuration);

            var app = builder.Build();

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

            app.Run();
        }
    }
}
