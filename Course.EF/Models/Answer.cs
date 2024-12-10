using System;
using System.Collections.Generic;

namespace Course.EF.Models;

public partial class Answer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Order { get; set; }

    public bool IsCorrect { get; set; }

    public int QuestionId { get; set; }

    public virtual Question Question { get; set; } = null!;
}
