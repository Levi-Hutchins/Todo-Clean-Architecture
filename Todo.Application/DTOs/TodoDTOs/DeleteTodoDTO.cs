namespace Todo.Application.DTOs;

public class DeleteTodoDTO
{
    public int Id { get; set; }
    public string Title { get; set; }

    public string Description { get; set; }

    public bool IsComplete { get; set; } = false;
    

}