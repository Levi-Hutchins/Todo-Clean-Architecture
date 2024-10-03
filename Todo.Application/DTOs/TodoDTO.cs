namespace Todo.Application.DTOs;

public class TodoDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsComplete { get; set; } = false;
}