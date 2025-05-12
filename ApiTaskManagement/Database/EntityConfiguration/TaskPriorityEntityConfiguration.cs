using ApiTaskManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaskManagement.Database.EntityConfiguration
{
    public class TaskPriorityEntityConfiguration : IEntityTypeConfiguration<TaskPriorityEntity>
    {
        public void Configure(EntityTypeBuilder<TaskPriorityEntity> builder)
        {
            builder.ToTable("tbl_task_priority");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
