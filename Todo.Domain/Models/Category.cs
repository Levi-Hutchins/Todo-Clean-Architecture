using System;
using System.Collections.Generic;

namespace Todo.Domain.Models;

public partial class Category
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Todos> Todos { get; set; } = new List<Todos>();
}
