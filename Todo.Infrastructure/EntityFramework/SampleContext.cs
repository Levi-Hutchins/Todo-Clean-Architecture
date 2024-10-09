// using Microsoft.EntityFrameworkCore;
// using Todo.Domain.Models;
//
// namespace Todo.Infrastructure.EntityFramework;
//
// public class SampleContext: DbContext
// {
//     public SampleContext()
//     {
//         
//     }
//
//     public SampleContext(DbContextOptions<SampleContext> options) : base(options)
//     {
//         
//     }
//     public DbSet<Category> Categories { get; set; }
//     public DbSet<User> Users { get; set; }
//     public DbSet<Todos> Todos { get; set; }
//
//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         modelBuilder.Entity<Category>().ToTable("Categories");
//         modelBuilder.Entity<User>().ToTable("Users");
//         modelBuilder.Entity<Todos>().ToTable("Todos");
//
//     }
// }
