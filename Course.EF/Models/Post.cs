using System;
using System.Collections.Generic;

namespace Course.EF.Models;

public partial class Post
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string Description { get; set; } = null!;

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;
}
