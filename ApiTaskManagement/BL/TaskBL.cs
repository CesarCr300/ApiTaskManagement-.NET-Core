using ApiTaskManagement.Entities;
using ApiTaskManagement.BL.Interfaces;
using ApiTaskManagement.Repositories.Interfaces;
using ApiTaskManagement.Services.Interfaces;
using ApiTaskManagement.DTOs;

namespace ApiTaskManagement.BL
{
    public class TaskBL : ITaskBL
    {
        private readonly ITaskRepository _repo;
        private readonly ITaskService _service;

        public TaskBL(ITaskRepository repo, ITaskService taskService)
        {
            _repo = repo;
            _service = taskService;
        }

        public Task<IEnumerable<TaskResponseDTO>> GetAllAsync(string userId)
        {
            return _service.GetTasksByUserAsync(userId);
        }

        public Task<TaskResponseDTO?> GetByIdAsync(int id, string userId)
        {
            return _service.GetByIdAsync(id, userId);
        }

        public Task<bool> CreateAsync(TaskEntity task)
        {
            return _repo.CreateAsync(task);
        }

        public Task<bool> UpdateAsync(TaskEntity task)
        {
            return _repo.UpdateAsync(task);
        }

        public Task<bool> DeleteAsync(int id, string userId)
        {
            return _repo.DeleteAsync(id, userId);
        }
    }

}
