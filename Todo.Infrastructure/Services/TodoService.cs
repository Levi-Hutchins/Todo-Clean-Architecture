using Todo.Application.IServices;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.Models;

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
        if (todo == null)
        {
            throw new KeyNotFoundException($"Todo item with id {id} was not found.");
        }
        return todo;    }

    public async Task<IEnumerable<Todos>> GetTodosAsync()
    {
        return await _context.Todos.ToListAsync();
    }

    public async Task AddTodoAsync(Todos todo)
    {
         _context.Todos.Add(todo);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTodoAsync(Todos todo)
    {
        _context.Todos.Update(todo);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTodoAsync(int id)
    {
        return;
    }
}