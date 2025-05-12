using ApiTaskManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiTaskManagement.Database;
public class TaskManagementDbContext : DbContext
{
    public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options) : base(options) { }

    public DbSet<TaskEntity> Tasks { get; set; }
    public DbSet<TaskPriorityEntity> TaskPriorities { get; set; }
    public DbSet<TaskStateEntity> TaskStates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskManagementDbContext).Assembly);
    }
}
