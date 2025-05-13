namespace ApiTaskManagement.Repositories.Interfaces
{
    using ApiTaskManagement.Entities;
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskEntity>> GetAllAsync(string userId);
        Task<TaskEntity?> GetByIdAsync(int id, string userId);
        Task<bool> CreateAsync(TaskEntity task);
        Task<bool> UpdateAsync(TaskEntity task);
        Task<bool> DeleteAsync(int id, string userId);
    }

}
