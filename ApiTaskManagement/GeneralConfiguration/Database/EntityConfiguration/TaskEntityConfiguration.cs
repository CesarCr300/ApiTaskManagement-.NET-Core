using ApiTaskManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaskManagement.GeneralConfiguration.Database.EntityConfiguration
{
    public class TaskEntityConfiguration : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.ToTable("tbl_task");

            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id");
            builder.Property(t => t.Title).IsRequired().HasMaxLength(255).HasColumnName("title");
            builder.Property(t => t.Description).IsRequired().HasColumnName("description");
            builder.Property(t => t.DateClose).HasColumnName("date_close");
            builder.Property(t => t.PriorityId).HasColumnName("priority_id");
            builder.Property(t => t.StateId).HasColumnName("state_id");
            builder.Property(t => t.UserId).IsRequired().HasMaxLength(255).HasColumnName("user_id");

            builder.HasOne(t => t.Priority)
                   .WithMany(p => p.Tasks)
                   .HasForeignKey(t => t.PriorityId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.State)
                   .WithMany(s => s.Tasks)
                   .HasForeignKey(t => t.StateId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }

}
