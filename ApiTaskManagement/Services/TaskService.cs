using Microsoft.EntityFrameworkCore;

using ApiTaskManagement.Database;
using ApiTaskManagement.DTOs;
using ApiTaskManagement.Services.Interfaces;
using ApiTaskManagement.Entities;

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

        public async Task<IEnumerable<TaskResponseDTO>> GetTasks()
        {
            var userId = _currentUser.UserId;
            var result = await _context
                .Database
                .SqlQueryRaw<TaskResponseDTO>("SELECT * FROM sp_get_tasks_by_user({0})", userId)
                .ToListAsync();

            return result;
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
    }
}
