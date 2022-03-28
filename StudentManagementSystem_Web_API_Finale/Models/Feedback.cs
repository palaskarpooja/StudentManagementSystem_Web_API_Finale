﻿using System;
using System.Collections.Generic;

#nullable disable

namespace StudentManagementSystem_Web_API_Finale.Models
{
    public partial class Feedback
    {
        public byte Id { get; set; }
        public string Description { get; set; }
        public byte? CourseId { get; set; }
        public byte? StudentId { get; set; }
        public DateTime FeedbackDate { get; set; }

        public virtual Course Course { get; set; }
        public virtual StudentRegistration Student { get; set; }
    }
}