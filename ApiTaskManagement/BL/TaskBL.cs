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
        private readonly ICurrentUserService _currentUser;

        public TaskBL(ITaskRepository repo, ICurrentUserService currentUserService)
        {
            _repo = repo;
            _currentUser = currentUserService;
        }

        public Task<bool> CreateAsync(TaskEntity task)
        {
            task.initialize();
            return _repo.CreateAsync(task);
        }

        public async Task<bool> UpdateAsync(TaskEntity task)
        {
            var taskEntity = await _repo.GetByIdAsync(task.Id, _currentUser.UserId);
            if (taskEntity is null) return false;

            taskEntity.update(task);

            return await _repo.UpdateAsync(taskEntity);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return _repo.DeleteAsync(id, _currentUser.UserId);
        }
    }

}
