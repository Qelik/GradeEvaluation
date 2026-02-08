using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
    }
}
