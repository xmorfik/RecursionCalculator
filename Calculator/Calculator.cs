using System.Text.RegularExpressions;
using System.Globalization;

namespace Calculator;

public class Calculator
{
    private readonly Regex _braskets = new Regex(@"(?<=\()[^()]*(?=\))");
    private readonly Regex _multiplication = new Regex(@"(?<numberA>-?\d*\.{0,1}\d+)\*(?<numberB>-?\d*\.{0,1}\d+)");
    private readonly Regex _division = new Regex(@"(?<numberA>-?\d*\.{0,1}\d+)\/(?<numberB>-?\d*\.{0,1}\d+)");
    private readonly Regex _summation = new Regex(@"(?<numberA>-?\d*\.{0,1}\d+)\+(?<numberB>-?\d*\.{0,1}\d+)");
    private readonly Regex _subtraction = new Regex(@"(?<numberA>-?\d*\.{0,1}\d+)\-(?<numberB>-?\d*\.{0,1}\d+)");

    public Calculator()
    {
        CultureInfo.CurrentCulture = new CultureInfo("en-UK");
    }

    public void Solve(ref string task)
    {
        try
        {
            CallRecursion(ref task);
        }
        catch (DivideByZeroException)
        {
            task = "Exception. Divide by zero";
            return;
        }

        TryParseResult(ref task);
    }

    public void SolveFile(FileInfo file)
    {
        CheckFile(file);
        var solvedLinesOfFile = new List<string>();

        foreach (var readLine in File.ReadLines(file.FullName))
        {
            var task = new string(readLine);
            Solve(ref task);
            solvedLinesOfFile.Add($"{readLine} => {task}");
        }

        var newFileName = "solved-" + file.Name;
        File.WriteAllLines(newFileName, solvedLinesOfFile);

        Console.WriteLine($"Result in {newFileName}:");
        foreach(var str in solvedLinesOfFile)
        {
            Console.WriteLine(str);
        }
    }

    private void CallRecursion(ref string result)
    {
        GoIntoBrackets(ref result);
        Multiply(ref result);
        Divide(ref result);
        Summarize(ref result);
        Subtract(ref result);
    }

    private void Multiply(ref string task)
    {
        var match = _multiplication.Match(task);
        if (match.Success)
        {
            var numA = double.Parse(match.Groups["numberA"].Value);
            var numB = double.Parse(match.Groups["numberB"].Value);
            var result = (numA * numB).ToString("F10");
            task = task.Replace(match.Value, result);
            CallRecursion(ref task);
        }
    }

    private void Divide(ref string task)
    {
        var match = _division.Match(task);
        if (match.Success)
        {
            var numA = double.Parse(match.Groups["numberA"].Value);
            var numB = double.Parse(match.Groups["numberB"].Value);

            if (numB == 0)
            {
                throw new DivideByZeroException();
            }

            var calculatedNum = numA / numB;
            var result = calculatedNum.ToString("F10");

            task = task.Replace(match.Value, result);
            CallRecursion(ref task);
        }
    }

    private void Summarize(ref string task)
    {
        var match = _summation.Match(task);
        if (match.Success)
        {
            var numA = double.Parse(match.Groups["numberA"].Value);
            var numB = double.Parse(match.Groups["numberB"].Value);

            var calculatedNum = numA + numB;
            var result = calculatedNum.ToString("F10");

            if (calculatedNum == 0)
            {
                result = "";
            }
            task = task.Replace(match.Value, result);
            CallRecursion(ref task);
        }
    }

    private void Subtract(ref string task)
    {
        var match = _subtraction.Match(task);
        if (match.Success)
        {
            var numA = double.Parse(match.Groups["numberA"].Value);
            var numB = double.Parse(match.Groups["numberB"].Value);

            var calculatedNum = numA - numB;
            var result = calculatedNum.ToString("F10");

            if (calculatedNum == 0)
            {
                result = "";
            }

            task = task.Replace(match.Value, result);
            CallRecursion(ref task);
        }
    }

    private void GoIntoBrackets(ref string task)
    {
        var match = _braskets.Match(task);
        if (match.Success)
        {
            var result = new string(match.Value);
            CallRecursion(ref result);
            task = task.Replace("(" + match.Value + ")", result);
            CallRecursion(ref task);
        }
    }

    private void TryParseResult(ref string task)
    {
        if(task == "")
        {
            task = "0";
            return;
        }

        try
        {
            var result = double.Parse(task);
            task = Math.Round(result, 10).ToString();
        }
        catch (FormatException)
        {
            task = "Exception. Wrong input";
        }
    }

    private void CheckFile(FileInfo file)
    {
        if (file == null)
        {
            throw new ArgumentNullException();
        }

        if (!file.Exists)
        {
            throw new FileNotFoundException();
        }
    }
}
