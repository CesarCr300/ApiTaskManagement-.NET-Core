namespace ApiTaskManagement.BL.Interfaces
{
    using ApiTaskManagement.DTOs;
    using ApiTaskManagement.Entities;

    public interface ITaskBL
    {
        Task<bool> CreateAsync(TaskEntity task);
        Task<bool> UpdateAsync(TaskEntity task);
        Task<bool> DeleteAsync(int id);
    }

}
