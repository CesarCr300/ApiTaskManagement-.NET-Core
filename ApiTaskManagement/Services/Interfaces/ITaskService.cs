using ApiTaskManagement.DTOs;

namespace ApiTaskManagement.Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskResponseDTO>> GetTasksByUserAsync(string userId);
        Task<TaskResponseDTO?> GetByIdAsync(int id, string userId);
    }
}
