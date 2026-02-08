using System.Collections.Generic;

namespace Entities.Models
{
    public class StudentExamsViewModel
    {
        public Teacher Teacher { get; set; }
        public List<Exam> Exams { get; set; }
    }
}