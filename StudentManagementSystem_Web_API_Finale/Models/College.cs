using System;
using System.Collections.Generic;

#nullable disable

namespace StudentManagementSystem_Web_API_Finale.Models
{
    public partial class College
    {
        public College()
        {
            StudentRegistrations = new HashSet<StudentRegistration>();
        }

        public string Name { get; set; }
        public byte Id { get; set; }

        public virtual ICollection<StudentRegistration> StudentRegistrations { get; set; }
    }
}
