﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Shared.DTOs
{
    public class AnswerDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int Order { get; set; }

        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }

    }
}
