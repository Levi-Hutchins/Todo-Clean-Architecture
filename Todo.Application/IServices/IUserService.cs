using Todo.Domain.Models;

namespace Todo.Application.IServices;

public interface IUserService
{
    Task<IEnumerable<User>> GetUsersAsync();

}