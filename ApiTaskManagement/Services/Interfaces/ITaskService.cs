using ApiTaskManagement.DTOs;

namespace ApiTaskManagement.Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskResponseDTO>> GetTasks();
        Task<TaskResponseDTO?> GetByIdAsync(int id);
    }
}
