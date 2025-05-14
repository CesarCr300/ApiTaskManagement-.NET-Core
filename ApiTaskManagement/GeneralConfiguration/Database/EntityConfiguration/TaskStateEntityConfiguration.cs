using ApiTaskManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaskManagement.GeneralConfiguration.Database.EntityConfiguration
{
    public class TaskStateEntityConfiguration : IEntityTypeConfiguration<TaskStateEntity>
    {
        public void Configure(EntityTypeBuilder<TaskStateEntity> builder)
        {
            builder.ToTable("tbl_task_state");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnName("id");
            builder.Property(s => s.Name).IsRequired().HasMaxLength(255).HasColumnName("name");
        }
    }

}
