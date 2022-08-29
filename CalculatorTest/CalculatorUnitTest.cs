namespace CalculatorTest;

using Calculator;

[TestClass]
public class CalculatorUnitTest
{
    Calculator calculator = new Calculator();

    [TestMethod]
    public void ReturnCorrect_RealNumber()
    {
        var task = "1/2";
        var expected = "0.5";
        calculator.Solve(ref task);
        var actual = task;
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ReturnCorrect_Int()
    {
        var task = "2/2";
        var expected = "1";
        calculator.Solve(ref task);
        var actual = task;
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ReturnCorrect_NegativeNum()
    {
        var task = "-5";
        var expected = "-5";
        calculator.Solve(ref task);
        var actual = task;
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ReturnCorrect_SimpleTask()
    {
        var task = "4-5";
        var expected = "-1";
        calculator.Solve(ref task);
        var actual = task;
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ReturnCorrect_WrongInput()
    {
        var task = "-1/2 + fdfgd";
        var expected = "Exception. Wrong input";
        calculator.Solve(ref task);
        var actual = task;
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ReturnCorrect_DivideByZero()
    {
        var task = "1+1 - 1/0";
        var expected = "Exception. Divide by zero";
        calculator.Solve(ref task);
        var actual = task;
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ReturnCorrect_LongTask()
    {
        var task = "1+1+2+4-7+7+6";
        var expected = "14";
        calculator.Solve(ref task);
        var actual = task;
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ReturnCorrect_TwoLevelTask()
    {
        var task = "1+(1+2)*5+(4-7)+7*6";
        var expected = "55";
        calculator.Solve(ref task);
        var actual = task;
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ReturnCorrect_TwoLevelOnion()
    {
        var task = "1+((1+2)*5+(4-7))*3+7*6";
        var expected = "79";
        calculator.Solve(ref task);
        var actual = task;
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ReturnCorrect_ThreeLevelOnion()
    {
        var task = "(1+((1+2)*5+(4-7))*3+7*6)*2";
        var expected = "158";
        calculator.Solve(ref task);
        var actual = task;
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ReturnCorrect_ThreeLevelOnion_RealNum()
    {
        var task = "(1+((1+2)*5+(4-7))*3+7*6)*2.5";
        var expected = "197.5";
        calculator.Solve(ref task);
        var actual = task;
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ReturnCorrect_WolframAlfaSimpleTask()
    {
        var task = "(((1+((1+2)*5+(4-7))*3+7*6)*2.5)/4+((((3)))))*6";
        var expected = "314.25";
        calculator.Solve(ref task);
        var actual = task;
        Assert.AreEqual(expected, actual);
    }
}