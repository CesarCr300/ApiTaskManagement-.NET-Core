namespace ApiTaskManagement.BL.Interfaces
{
    using ApiTaskManagement.DTOs;
    using ApiTaskManagement.Entities;

    public interface ITaskBL
    {
        Task<IEnumerable<TaskResponseDTO>> GetAllAsync();
        Task<TaskResponseDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(TaskEntity task);
        Task<bool> UpdateAsync(TaskEntity task);
        Task<bool> DeleteAsync(int id);
    }

}
