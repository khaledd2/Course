using System;
using System.Collections.Generic;

namespace Course.DAL.Models;

public partial class Unit
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CourseId { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual ICollection<Survey> Surveys { get; set; } = new List<Survey>();
}
