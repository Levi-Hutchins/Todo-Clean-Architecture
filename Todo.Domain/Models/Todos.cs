using System;
using System.Collections.Generic;

namespace Todo.Domain.Models;

public partial class Todos
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public bool? IsComplete { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? DueDate { get; set; }

    public int? UserId { get; set; }

    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual User? User { get; set; }
}
