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
}