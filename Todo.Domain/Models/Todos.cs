using System.ComponentModel.DataAnnotations;

namespace Todo.Domain.Models;

public class Todos
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Title is required.")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; }
    public bool IsComplete { get; set; } = false;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime DueDate { get; set; } = new DateTime(DateTime.Now.Year, 12, 31);
    [Required (ErrorMessage = "A user ID is required.")]
    public int UserId { get; set; }
    [Required (ErrorMessage = "A category ID is required.")]
    public int CategoryId { get; set; }
    
}