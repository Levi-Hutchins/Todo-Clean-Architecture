namespace Todo.Domain.Models;

public class Todo
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsComplete { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime DueDate { get; set; }
    public int UserId { get; set; }
    public int CategoryId { get; set; }
    
}