using Microsoft.EntityFrameworkCore;
using Todo.Domain.Models;

namespace Todo.Infrastructure;

public class TodoDbContext: DbContext
{
    public TodoDbContext(DbContextOptions options) : base(options)
    {
        
    }
    public DbSet<Users> Users { get; set; }
    public DbSet<Domain.Models.Todo> Todos { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Users>().ToTable("Users");
        modelBuilder.Entity<Domain.Models.Todo>().ToTable("Todos");
        modelBuilder.Entity<Category>().ToTable("Category");

    }
}