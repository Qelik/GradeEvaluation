using Entities;
using System.Collections.Generic;

namespace UI.Models
{
    public class StudentExamsViewModel
    {
        public Teacher Teacher { get; set; }
        public List<Exam> Exams { get; set; }
    }
}