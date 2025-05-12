using ApiTaskManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaskManagement.Database.EntityConfiguration
{
    public class TaskStateEntityConfiguration : IEntityTypeConfiguration<TaskStateEntity>
    {
        public void Configure(EntityTypeBuilder<TaskStateEntity> builder)
        {
            builder.ToTable("tbl_task_state");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(s => s.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
