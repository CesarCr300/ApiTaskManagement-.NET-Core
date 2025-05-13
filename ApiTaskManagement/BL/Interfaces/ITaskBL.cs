namespace ApiTaskManagement.BL.Interfaces
{
    using ApiTaskManagement.DTOs;
    using ApiTaskManagement.Entities;

    public interface ITaskBL
    {
        Task<IEnumerable<TaskResponseDTO>> GetAllAsync(string userId);
        Task<TaskResponseDTO?> GetByIdAsync(int id, string userId);
        Task<bool> CreateAsync(TaskEntity task);
        Task<bool> UpdateAsync(TaskEntity task);
        Task<bool> DeleteAsync(int id, string userId);
    }

}
