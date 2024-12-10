using System;
using System.Collections.Generic;

namespace Course.DAL.Models;

public partial class Course
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public bool HasCertificate { get; set; }

    public string Question { get; set; } = null!;

    public string Answer { get; set; } = null!;

    public int CategoryId { get; set; }

    public bool AllowDownload { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Goal> Goals { get; set; } = new List<Goal>();

    public virtual ICollection<Unit> Units { get; set; } = new List<Unit>();
}
