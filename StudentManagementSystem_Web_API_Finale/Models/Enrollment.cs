using System;
using System.Collections.Generic;

#nullable disable

namespace StudentManagementSystem_Web_API_Finale.Models
{
    public partial class Enrollment
    {
        public int Id { get; set; }
        public byte StudentId { get; set; }
        public byte CourseId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Course Course { get; set; }
        public virtual StudentRegistration Student { get; set; }
    }
}
