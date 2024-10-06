using System;
using System.Collections.Generic;

namespace Todo.Domain.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Todos> Todos { get; set; } = new List<Todos>();
}
