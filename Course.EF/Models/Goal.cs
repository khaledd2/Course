using System;
using System.Collections.Generic;

namespace Course.EF.Models;

public partial class Goal
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CourseId { get; set; }

    public virtual Course Course { get; set; } = null!;
}
