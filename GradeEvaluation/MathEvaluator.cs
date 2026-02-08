using GradeEvaluation;
using System;
using System.Data;

public class MathEvaluator : IMathEvaluator
{
    public decimal Evaluate(string expression)
    {
        var table = new DataTable();
        var value = table.Compute(expression, string.Empty);
        return Convert.ToDecimal(value);
    }
}