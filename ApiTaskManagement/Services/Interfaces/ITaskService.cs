using ApiTaskManagement.DTOs;

namespace ApiTaskManagement.Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskResponseDTO>> GetTasks(string? from, string? end);

        Task<TaskResponseDTO?> GetByIdAsync(int id);
    }
}
