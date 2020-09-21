using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ToDoListAPI.Models
{
    public partial class ToDoListAppContext : DbContext
    {
        public ToDoListAppContext()
        {
        }

        public ToDoListAppContext(DbContextOptions<ToDoListAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bucket> Bucket { get; set; }
        public virtual DbSet<ToDoTask> ToDoTask { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
////#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=TUSHAR-LAPTOP\\SQLEXPRESS;Initial Catalog=ToDoListApp;Persist Security Info=True;User ID=CmsUser;Password=Hello@123");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bucket>(entity =>
            {
                entity.Property(e => e.BucketId).HasColumnName("BucketID");

                entity.Property(e => e.CreateDateTime)
                    .HasColumnName("createDateTime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ToDoTask>(entity =>
            {
                entity.HasKey(e => e.TaskId)
                    .HasName("PK__ToDoTask__7C6949D1BB5EABEF");

                entity.Property(e => e.TaskId).HasColumnName("TaskID");

                entity.Property(e => e.BucketId).HasColumnName("BucketID");

                entity.Property(e => e.CreateDateTime)
                    .HasColumnName("createDateTime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Bucket)
                    .WithMany(p => p.ToDoTask)
                    .HasForeignKey(d => d.BucketId)
                    .HasConstraintName("FK_ToDoBucket");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
