using Todo.Application.DTOs;
using Todo.Domain.Models;

namespace Todo.Application.IServices;

public interface IUserService
{
    Task<IEnumerable<User>> GetUsersAsync();
    Task<IEnumerable<Todos>> GetUserTodosAsync(int id);
    Task<User> CreateUserAsync(User newUser);
    Task<User?> GetUserAsync(int id);

}