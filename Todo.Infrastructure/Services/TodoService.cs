using System.Text.Json;
using Todo.Application.IServices;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.Models;
using Todo.Infrastructure.EntityFramework;

namespace Todo.Infrastructure.Services;

public class TodoService : ITodoService
{
    private readonly TodoDbContext _context;

    public TodoService(TodoDbContext context)
    {
        _context = context;
    }

    public async Task<Todos> GetTodoByIdAsync(int id)
    {
        var todo = await _context.Todos.FindAsync(id);
    
        return todo;    
    }

    public async Task<IEnumerable<Todos>> GetTodosAsync()
    {
        return await _context.Todos.ToListAsync();
    }

    public async Task AddTodoAsync(Todos todo)
    {
        // TODO: need to check if it already exists

        _context.Todos.Add(todo);
         
        await _context.SaveChangesAsync();
    }

    
    public async Task<Todos?> UpdateTodoAsync(Todos todo)
    {
        var rowsAffected = await _context.Todos.Where(t => t.Id == todo.Id)
            .ExecuteUpdateAsync(updates =>
                updates.SetProperty(t => t.Description, todo.Description)
                    .SetProperty(t => t.IsComplete, todo.IsComplete)
                    .SetProperty(t => t.Title, todo.Title)
                    .SetProperty(t => t.DueDate, todo.DueDate));
        return rowsAffected == 0 ? null : todo;
    }

    public async Task DeleteTodoAsync(int id)
    {
        var deleteTodo = _context.Todos.Where((t => t.Id == id)).First();
        _context.Remove(deleteTodo);
        await _context.SaveChangesAsync();
    }
}