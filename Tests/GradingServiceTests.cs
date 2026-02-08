using System.Collections.Generic;
using System.Linq;
using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class GradingServiceTests
{
    [TestMethod]
    public void Grade_CorrectAnswer_MarkedAsCorrect()
    {
        // Construct concrete lists to avoid depending on entity constructors / EF proxies
        var teacher = new Teacher
        {
            Students = new List<Student>
            {
                new Student
                {
                    Exams = new List<Exam>
                    {
                        new Exam
                        {
                            Tasks = new List<Task>
                            {
                                new Task
                                {
                                    Expression = "2+2",
                                    ExpectedResult = 4m
                                }
                            }
                        }
                    }
                }
            }
        };

        var service = new ExamGradingService(new MathEvaluator());
        service.Grade(teacher);

        // Verify task was evaluated and marked correct
        var task = teacher.Students.First().Exams.First().Tasks.First();
        Assert.IsNotNull(task, "Task should not be null after setup.");
        Assert.AreEqual(4m, task.CalculatedResult, "CalculatedResult should be 4.");
        Assert.IsTrue(task.IsCorrect, "Task with correct expression should be marked as correct.");
    }
}