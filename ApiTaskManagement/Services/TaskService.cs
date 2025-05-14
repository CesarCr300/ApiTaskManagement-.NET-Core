using Microsoft.EntityFrameworkCore;
using ApiTaskManagement.DTOs;
using ApiTaskManagement.Services.Interfaces;
using ApiTaskManagement.Entities;
using ApiTaskManagement.GeneralConfiguration.Database;
using ApiTaskManagement.Utils.Exceptions;
using System.Net;

namespace ApiTaskManagement.Services
{
    public class TaskService: ITaskService
    {
        private readonly TaskManagementDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public TaskService(TaskManagementDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            this._currentUser = currentUser;
        }

        public async Task<IEnumerable<TaskResponseDTO>> GetTasks(string? from, string? end)
        {
            var userId = _currentUser.UserId;
            var range = TryParseDateRange(from, end);

            if (range.HasValue)
            {
                var (fromDate, toDate) = range.Value;

                return await _context
                    .Database
                    .SqlQueryRaw<TaskResponseDTO>(
                        "SELECT * FROM sp_get_task_by_dates_and_user({0}, {1}, {2})",
                        fromDate.ToString("yyyy-MM-dd HH:mm:ss"),
                        toDate.ToString("yyyy-MM-dd HH:mm:ss"),
                        userId)
                    .ToListAsync();
            }

            return await _context
                .Database
                .SqlQueryRaw<TaskResponseDTO>("SELECT * FROM sp_get_tasks_by_user({0})", userId)
                .ToListAsync();
        }



        public async Task<TaskResponseDTO?> GetByIdAsync(int id)
        {
            var userId = _currentUser.UserId;
            var result = await _context
                .Database
                .SqlQueryRaw<TaskResponseDTO>("SELECT * FROM sp_get_task_by_id_and_user({0}, {1})", id, userId)
                .ToListAsync();

            return result.FirstOrDefault();
        }

        public (DateTime from, DateTime to)? TryParseDateRange(string? from, string? end)
        {
            if (DateTime.TryParse(from, out var fromDate) && DateTime.TryParse(end, out var endDate))
            {
                if (fromDate > endDate)
                {
                    throw new HttpException("La fecha fin debe ser mayor o igual a la fecha de inicio.", 400);
                }

                endDate = endDate.Date.AddDays(1).AddSeconds(-1); // hasta 23:59:59
                return (fromDate, endDate);
            }

            return null;
        }

    }
}
