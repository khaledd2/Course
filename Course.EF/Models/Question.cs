using System;
using System.Collections.Generic;

namespace Course.EF.Models;

public partial class Question
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Order { get; set; }

    public int SurveyId { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual Survey Survey { get; set; } = null!;
}
