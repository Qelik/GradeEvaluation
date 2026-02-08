using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class GradingServiceTests
{
    [TestMethod]
    public void Grade_CorrectAnswer_MarkedAsCorrect()
    {
        var teacher = new Teacher
        {
            Students =
            {
                new Student
                {
                    Exams =
                    {
                        new Exam
                        {
                            Tasks =
                            {
                                new Task
                                {
                                    Expression = "2+2",
                                    ExpectedResult = 4
                                }
                            }
                        }
                    }
                }
            }
        };

        var service = new ExamGradingService(new MathEvaluator());
        service.Grade(teacher);

        Assert.IsTrue(teacher.Students[0].Exams[0].Tasks[0].IsCorrect);
    }
}