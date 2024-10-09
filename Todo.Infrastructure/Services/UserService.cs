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

    public async Task<UserCreatedDTO> CreateUserAsync(User newUser)
    {
        if (_context.Users.Any(u => u.Email == newUser.Email))
        {
            return new UserCreatedDTO
            {
                Success = false,
                ErrorMessage = $"A user with the email {newUser.Email} already exists."
            };
        }
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
        return new UserCreatedDTO
        {
            Success = true,
            CreatedUser = newUser
        };
    }

    public async Task<User?> GetUserAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<UserDTO?> UpdateUserAsync(int id, UserDTO updatedUser)
    {

        
        var rowsAffected = await _context.Users.Where(user => user.Id == id)
            .ExecuteUpdateAsync(updates =>
                updates.SetProperty(user => user.Email, updatedUser.Email)
                    .SetProperty(user => user.Name, updatedUser.Name));
        await _context.SaveChangesAsync();
        return rowsAffected == 0 ? null : updatedUser;
    }

    public async Task DeleteUserAsync(int id)
    {
        // TODO: handle this  better
        await _context.Users.Where(u => u.Id == id).ExecuteDeleteAsync();
         
    }

}