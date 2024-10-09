namespace Todo.Application.DTOs;

public class UpdateTodoDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsComplete { get; set; }
    public DateTime DueDate { get; set; } = new DateTime(DateTime.Now.Year, 12, 31);
}