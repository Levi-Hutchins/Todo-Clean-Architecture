namespace Todo.Application.DTOs;

public class UserTodsDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsComplete { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime DueDate { get; set; }

    
}