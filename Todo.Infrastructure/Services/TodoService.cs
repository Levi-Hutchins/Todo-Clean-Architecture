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
        _context.Todos.Add(todo);
         
        await _context.SaveChangesAsync();
    }

    
    public async Task UpdateTodoAsync(Todos todo)
    {
        // find by id in the db
        var trackedEntity = _context.Todos.Local.FirstOrDefault(t => t.Id == todo.Id);
    
        // handle case of not found
        if (trackedEntity == null)
        {
            _context.Todos.Attach(todo);
        }
    
        // modify the state of the todo
        _context.Entry(todo).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTodoAsync(int id)
    {
        var deleteTodo = _context.Todos.Where((t => t.Id == id)).First();
        _context.Remove(deleteTodo);
        await _context.SaveChangesAsync();
    }
}