namespace Todo.Application.DTOs;

public class CreateTodoDTO
{
    public string Title { get; set; }

    public string Description { get; set; }

    public bool IsComplete { get; set; } = false;
    

}