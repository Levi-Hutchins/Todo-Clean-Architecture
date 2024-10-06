using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.Models;

namespace Todo.Infrastructure.EntityFramework;

public partial class TodoDbContext : DbContext
{
    public TodoDbContext()
    {
    }

    public TodoDbContext(DbContextOptions<TodoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Todos> Todos { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC07D64EDFEB");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Todos>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Todos__3214EC07BE937493");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Category).WithMany(p => p.Todos)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Todos__CategoryI__3E52440B");

            entity.HasOne(d => d.User).WithMany(p => p.Todos)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Todos__UserId__3D5E1FD2");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0753DCC28F");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105345504F32C").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
