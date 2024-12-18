using System;
using System.Collections.Generic;

namespace Course.DAL.Models;

public partial class Lesson
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Script { get; set; } = null!;

    public string VideoUrl { get; set; } = null!;

    public int UnitId { get; set; }

    public int Order { get; set; }

    public bool IsLocked { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Unit Unit { get; set; } = null!;
}
