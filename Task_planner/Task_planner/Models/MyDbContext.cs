using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_planner.Models
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tasks>(entity =>
            {
                entity.Property(p => p.TaskId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(p => p.TaskName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(p => p.Detail)
                    /*.HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");*/
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Detail)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
