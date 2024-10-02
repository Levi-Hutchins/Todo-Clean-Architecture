namespace Todo.Application.IServices;

public interface ITodoService
{
    Task<Domain.Models.Todo> GetTodoByIdAsync(int id);
    Task<IEnumerable<Domain.Models.Todo>> GetTodosAsync();
    Task AddTodoAsync(Domain.Models.Todo todo);
    Task UpdateTodoAsync(Domain.Models.Todo todo);
    Task DeleteTodoAsync(int id);
}