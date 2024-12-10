using System;
using System.Collections.Generic;

namespace Course.DAL.Models;

public partial class Survey
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Order { get; set; }

    public int UnitId { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual Unit Unit { get; set; } = null!;
}
