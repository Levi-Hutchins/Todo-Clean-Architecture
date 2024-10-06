using Microsoft.EntityFrameworkCore;
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
    
}