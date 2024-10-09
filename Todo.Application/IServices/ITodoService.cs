using System.Collections;
using Todo.Domain.Models;

namespace Todo.Application.IServices;

public interface ITodoService
{
    Task<Todos> GetTodoByIdAsync(int id);
    Task<IEnumerable<Todos>> GetTodosAsync();
    Task AddTodoAsync(Todos todo);
    Task<Todos?> UpdateTodoAsync(Todos todo);
    Task DeleteTodoAsync(int id);
}