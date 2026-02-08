using Entities;
using GradeEvaluation;
using System;

public class ExamGradingService
{
    private readonly IMathEvaluator _mathEvaluator;

    public ExamGradingService(IMathEvaluator mathEvaluator)
    {
        _mathEvaluator = mathEvaluator;
    }

    public void Grade(Teacher teacher)
    {
        foreach (var student in teacher.Students)
        {
            foreach (var exam in student.Exams)
            {
                foreach (var task in exam.Tasks)
                {
                    task.CalculatedResult = _mathEvaluator.Evaluate(task.Expression);
                    task.IsCorrect = AreEqual(
                        task.CalculatedResult,
                        task.ExpectedResult);
                }
            }
        }
    }

    private bool AreEqual(decimal a, decimal b, decimal tolerance = 0.0001m)
    {
        return Math.Abs(a - b) <= tolerance;
    }
}