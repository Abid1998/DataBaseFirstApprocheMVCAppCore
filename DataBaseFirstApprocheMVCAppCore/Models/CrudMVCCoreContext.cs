using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataBaseFirstApprocheMVCAppCore.Models
{
    public partial class CrudMVCCoreContext : DbContext
    {
        public CrudMVCCoreContext()
        {
        }

        public CrudMVCCoreContext(DbContextOptions<CrudMVCCoreContext> options)
            : base(options)
        {
        }

     
        public virtual DbSet<Emptbl2> Emptbl2s { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;

     

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Emptbl2>(entity =>
            {
                entity.ToTable("emptbl2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.StudentGender)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.StudentName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
