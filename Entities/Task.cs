using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        public string Expression { get; set; }
        public decimal ExpectedResult { get; set; }
        public decimal CalculatedResult { get; set; }
        public bool IsCorrect { get; set; }

        public int ExamId { get; set; }
        public virtual Exam Exam { get; set; }
    }
}
