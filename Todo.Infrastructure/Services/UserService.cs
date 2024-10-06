using Microsoft.EntityFrameworkCore;
using Todo.Application.DTOs;
using Todo.Application.IServices;
using Todo.Domain.Models;
using Todo.Infrastructure.EntityFramework;

namespace Todo.Infrastructure.Services;

public class UserService: IUserService
{
    private readonly TodoDbContext _context;

    public UserService(TodoDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<IEnumerable<Todos>> GetUserTodosAsync(int userId)
    {
        var userTodos = await _context.Todos.Where(t => t.UserId == userId).ToListAsync();
        return userTodos;
    }

    public async Task<User> CreateUserAsync(User newUser)
    {
        // TODO: need to check if it already exists
        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();
        return newUser;
    }

    public async Task<User?> GetUserAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

}