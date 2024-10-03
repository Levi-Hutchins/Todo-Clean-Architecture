using Microsoft.EntityFrameworkCore;
using Todo.Domain.Models;

namespace Todo.Infrastructure;

public class TodoDbContext: DbContext
{
    public TodoDbContext(DbContextOptions options) : base(options)
    {
        
    }
    public DbSet<Users> Users { get; set; }
    public DbSet<Todos> Todos { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Users>().ToTable("Users");
        modelBuilder.Entity<Todos>().ToTable("Todos");
        modelBuilder.Entity<Category>().ToTable("Category");

    }
}