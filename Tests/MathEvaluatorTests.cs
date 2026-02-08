using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class MathEvaluatorTests
{
    [TestMethod]
    public void Evaluate_ValidExpression_ReturnsCorrectResult()
    {
        var evaluator = new MathEvaluator();
        var result = evaluator.Evaluate("2+3/6-4");

        Assert.AreEqual(-1.5m, result);
    }
}