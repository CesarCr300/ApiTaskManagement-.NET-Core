using ApiTaskManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaskManagement.Database.EntityConfiguration
{
    public class TaskEntityConfiguration : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.ToTable("tbl_task");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.Title)
                .HasColumnName("title")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(t => t.Description)
                .HasColumnName("description")
                .IsRequired();

            builder.Property(t => t.DateClose)
                .HasColumnName("date_close");

            builder.HasOne(t => t.Priority)
                .WithMany()
                .HasForeignKey("priority_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.State)
                .WithMany()
                .HasForeignKey("state_id")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
