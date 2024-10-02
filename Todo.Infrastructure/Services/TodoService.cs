using Todo.Application.IServices;
using Microsoft.EntityFrameworkCore;

namespace Todo.Infrastructure.Services;

public class TodoService : ITodoService
{
    private readonly TodoDbContext _context;

    public TodoService(TodoDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Models.Todo> GetTodoByIdAsync(int id)
    {
        return await _context.Todos.FindAsync(id);
    }

    public async Task<IEnumerable<Domain.Models.Todo>> GetTodosAsync()
    {
        return await _context.Todos.ToListAsync();
    }

    public async Task AddTodoAsync(Domain.Models.Todo todo)
    {
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTodoAsync(Domain.Models.Todo todo)
    {
        _context.Todos.Update(todo);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTodoAsync(int id)
    {
        return;
    }
}