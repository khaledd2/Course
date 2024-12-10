using System;
using System.Collections.Generic;

namespace Course.Server.Models;

public partial class Comment
{
    public int Id { get; set; }

    public string Comment1 { get; set; } = null!;

    public int? ParentCommentId { get; set; }

    public int LessonId { get; set; }

    public virtual ICollection<Comment> InverseParentComment { get; set; } = new List<Comment>();

    public virtual Lesson Lesson { get; set; } = null!;

    public virtual Comment? ParentComment { get; set; }
}
