using Todo.Domain.Models;

namespace Todo.Application.DTOs;

public class UserCreatedDTO
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public User? CreatedUser { get; set; }
}