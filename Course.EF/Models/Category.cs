﻿using System;
using System.Collections.Generic;

namespace Course.EF.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
