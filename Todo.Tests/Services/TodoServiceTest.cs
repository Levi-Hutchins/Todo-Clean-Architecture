using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Configuration.Annotations;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.Models;
using Todo.Infrastructure.EntityFramework;
using Todo.Infrastructure.Services;
using Xunit;

namespace Todo.Infrastructure.Tests.Services;

public class TodoServiceTests
{
    // in memory db context for local and CI unit tests
    private static TodoDbContext GetInMemoryDbContext()
    {
        // provides the EF context a local in memory database
        // that can be used at run time for unit testing
        var options = new DbContextOptionsBuilder<TodoDbContext>()
            .UseInMemoryDatabase(databaseName: "TodoDatabase")
            .Options;
        var dbContext = new TodoDbContext(options);
        return dbContext;
    }
    
    [Fact]
    public async Task GetTodoByIdAsync_TodoExists_ReturnsTodo()
    {
        var dbContext = GetInMemoryDbContext();
        var service = new TodoService(dbContext);

        var todo = new Todos { Id = 1, 
            Title = "Test Todo",
            Description = "This is test todo",
            IsComplete = false,
            DueDate = new DateTime(DateTime.Now.Year, 12, 31),
            UserId = 1,
            CategoryId = 1
            
        };
        dbContext.Todos.Add(todo);
        await dbContext.SaveChangesAsync();

        var result = await service.GetTodoByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Test Todo", result.Title);
    }

    [Fact]
    public async Task GetTodoByIdAsync_TodoDoesNotExist_ReturnsNull()
    {
        var dbContext = GetInMemoryDbContext();
        var service = new TodoService(dbContext);
        var todo = await service.GetTodoByIdAsync(99);
        // test a id that is not in db and expect appropriate error handling 
        Assert.Null(todo);
    }

    [Fact]
    public async Task GetTodosAsync_ReturnsAllTodos()
    {
        var dbContext = GetInMemoryDbContext();
        var service = new TodoService(dbContext);
        List<Todos> list = new List<Todos>();

        var todo1 = new Todos { Id = 11, 
            Title = "Test Todo 11", 
            IsComplete = false, 
            Description = "test todo 11 added",
            DueDate = new DateTime(DateTime.Now.Year, 12, 31),
            UserId = 1,
            CategoryId = 1 };
        var todo2 = new Todos { Id = 12, 
            Title = "Test Todo 12", 
            IsComplete = false, 
            Description = "test todo 12 added",
            DueDate = new DateTime(DateTime.Now.Year, 12, 31),
            UserId = 1,
            CategoryId = 1 };
        list.Add(todo1);
        list.Add(todo2);

        
        
        dbContext.Todos.AddRange(list);
        await dbContext.SaveChangesAsync();

        var result = await service.GetTodosAsync();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task AddTodoAsync_AddsTodoSuccessfully()
    {
        var dbContext = GetInMemoryDbContext();
        var service = new TodoService(dbContext);

        var newTodo = new Todos { Id = 3, 
            Title = "New Todo", 
            IsComplete = false, 
            Description = "New todo added",
            DueDate = new DateTime(DateTime.Now.Year, 12, 31),
            UserId = 1,
            CategoryId = 1
        };

        await service.AddTodoAsync(newTodo);
        var todoInDb = await dbContext.Todos.FindAsync(3);

        Assert.NotNull(todoInDb);
        Assert.Equal("New Todo", todoInDb.Title);
        Assert.False(todoInDb.IsComplete);
    }

    [Fact (Skip = "ExecuteUpdateAsync not supported in, InMemory Context")]
    public async Task UpdateTodoAsync_UpdatesTodoSuccessfully()
    {
        var dbContext = GetInMemoryDbContext();
        var service = new TodoService(dbContext);

        var existingTodo = new Todos
        {
            Id = 4,
            Title = "Old Todo",
            IsComplete = false,
            Description = "Old todo desc",
            DueDate = new DateTime(DateTime.Now.Year, 12, 31),
            UserId = 1,
            CategoryId = 1
        };

        dbContext.Todos.Add(existingTodo);
        await dbContext.SaveChangesAsync();

        // fetch the tracked entity and update its properties
        existingTodo.Title = "Updated Todo";
        existingTodo.IsComplete = true;
        existingTodo.Description = "Updated todo desc";

        await service.UpdateTodoAsync(existingTodo);

        var todoInDb = await dbContext.Todos.FindAsync(4);

        Assert.NotNull(todoInDb);
        Assert.Equal("Updated Todo", todoInDb.Title);
        Assert.True(todoInDb.IsComplete);
        Assert.Equal("Updated todo desc", todoInDb.Description);
    }



    [Fact]
    public async Task DeleteTodoAsync_TodoIsDeleted()
    {
        var dbContext = GetInMemoryDbContext();
        var service = new TodoService(dbContext);

        var todo = new Todos
        {
            Id = 5, Title = "Todo to Delete", IsComplete = false,
            Description = "Delete me",
            DueDate = new DateTime(DateTime.Now.Year, 12, 31),
            UserId = 1,
            CategoryId = 1
            
        };
        dbContext.Todos.Add(todo);
        await dbContext.SaveChangesAsync();

        await service.DeleteTodoAsync(5);
        var todoInDb = await dbContext.Todos.FindAsync(5);
        // this test expects the method to delete the item
        Assert.Null(todoInDb);  
    }
}