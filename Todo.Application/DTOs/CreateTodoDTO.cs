namespace Todo.Application.DTOs;

public class CreateTodoDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsComplete { get; set; } = false;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime DueDate { get; set; } = new DateTime(DateTime.Now.Year, 12, 31);
    public int UserId { get; set; }
    public int CategoryId { get; set; }

}