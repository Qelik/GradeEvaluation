using Entities;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;

public class ExamPersistenceService
{
    public void Save(Teacher teacher)
    {
        using (var db = new SchoolDbContext())
        {
            var teacherEntity = db.Teachers.FirstOrDefault(t => t.TeacherId == teacher.TeacherId);

            if (teacherEntity == null)
            {
                teacherEntity = new Teacher
                {
                    TeacherId = teacher.TeacherId
                };
                db.Teachers.Add(teacherEntity);
                db.SaveChanges();
            }

            foreach (var student in teacher.Students ?? new List<Student>())
            {
                var studentEntity = db.Students.FirstOrDefault(s => s.StudentId == student.StudentId && student.StudentId != 0);

                if (studentEntity == null)
                {
                    studentEntity = new Student
                    {
                        StudentId = student.StudentId,
                        TeacherId = teacherEntity.TeacherId
                    };

                    db.Students.Add(studentEntity);
                    db.SaveChanges();
                }

                foreach (var exam in student.Exams ?? new List<Exam>())
                {
                    var examEntity = db.Exams.Include("Tasks").FirstOrDefault(e => e.Id == exam.Id);

                    if (examEntity == null)
                    {
                        examEntity = new Exam
                        {
                            Id = exam.Id,
                            TeacherId = teacherEntity.TeacherId,
                            StudentId = studentEntity.StudentId,
                            Tasks = new List<Task>()
                        };

                        db.Exams.Add(examEntity);
                    }

                    foreach (var task in exam.Tasks ?? new List<Task>())
                    {
                        examEntity.Tasks.Add(new Task
                        {
                            Expression = task.Expression,
                            ExpectedResult = task.ExpectedResult,
                            CalculatedResult = task.CalculatedResult,
                            IsCorrect = task.IsCorrect
                        });
                    }
                }
            }

            db.SaveChanges();
        }
    }

    public Teacher GetTeacherWithExams(int teacherExternalId)
    {
        using (var db = new SchoolDbContext())
        {
            var exams = db.Exams.Include("Tasks").Where(e => e.TeacherId == teacherExternalId).ToList();

            var students = db.Students.Where(s => s.TeacherId == teacherExternalId).ToList();

            var clonedExams = exams.Select(e => new Exam
            {
                Id = e.Id,
                TeacherId = e.TeacherId,
                StudentId = e.StudentId,
                Tasks = (e.Tasks ?? new List<Task>()).Select(t => new Task
                {
                    Id = t.Id,
                    Expression = t.Expression,
                    ExpectedResult = t.ExpectedResult,
                    CalculatedResult = t.CalculatedResult,
                    IsCorrect = t.IsCorrect,
                    ExamId = t.ExamId
                }).ToList()
            }).ToList();

            var clonedStudents = students.Select(s => new Student
            {
                StudentId = s.StudentId,
                TeacherId = s.TeacherId,
                Exams = clonedExams.Where(ex => ex.StudentId == s.StudentId).ToList()
            }).ToList();

            var result = new Teacher
            {
                TeacherId = teacherExternalId,
                Students = clonedStudents,
                Exams = clonedExams
            };

            return result;
        }
    }

    public List<StudentExamsViewModel> GetExamsForStudent(int studentExternalId)
    {
        using (var db = new SchoolDbContext())
        {

            var student = db.Students.FirstOrDefault(s => s.StudentId == studentExternalId);

            if (student == null)
                return new List<StudentExamsViewModel>();

            var exams = db.Exams.Include("Tasks").Include("Teacher").Where(e => e.StudentId == student.StudentId).ToList();

            if (!exams.Any())
                return new List<StudentExamsViewModel>();

            var grouped = exams.GroupBy(e => e.Teacher)
                .Select(g => new StudentExamsViewModel
                {
                    Teacher = g.Key,
                    Exams = g.ToList()
                })
                .ToList();

            return grouped;
        }
    }
}