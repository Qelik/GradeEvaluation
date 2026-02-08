using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }

        public int TeacherId { get; set; }

        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
        public virtual Teacher Teacher { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
