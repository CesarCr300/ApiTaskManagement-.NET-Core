using ApiTaskManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiTaskManagement.Database;
public class TaskManagementDbContext : DbContext
{
    public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options) : base(options) { }

    public DbSet<TaskEntity> Tasks => Set<TaskEntity>();
    public DbSet<TaskPriorityEntity> TaskPriorities => Set<TaskPriorityEntity>();
    public DbSet<TaskStateEntity> TaskStates => Set<TaskStateEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskManagementDbContext).Assembly);
    }
}
