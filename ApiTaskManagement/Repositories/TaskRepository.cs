using ApiTaskManagement.Database;
using ApiTaskManagement.Entities;
using Microsoft.EntityFrameworkCore;
using ApiTaskManagement.Repositories.Interfaces;

namespace ApiTaskManagement.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskManagementDbContext _context;

        public TaskRepository(TaskManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskEntity>> GetAllAsync(string userId)
        {
            return await _context.Tasks.ToListAsync();
        }
        public async Task<TaskEntity?> GetByIdAsync(int id, string userId)
        {
            var entity = await _context.Tasks.FindAsync(id);
            if (entity is null) return null;
            if (!entity.UserId.Equals(userId)) return null;
            return entity;
        }

        public async Task<bool> CreateAsync(TaskEntity task)
        {
            _context.Tasks.Add(task);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(TaskEntity task)
        {
            _context.Tasks.Update(task);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id, string userId)
        {
            var task = await GetByIdAsync(id, userId);
            if (task == null) return false;

            _context.Tasks.Remove(task);
            return await _context.SaveChangesAsync() > 0;
        }
    }

}
