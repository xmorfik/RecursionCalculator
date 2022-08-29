namespace Calculator;

internal class Program
{
    private static readonly Calculator Calculator = new Calculator();

    private static void Main(string[] args)
    {
        Console.WriteLine("Calculator\n");
        
        while (true)
        {
            Console.WriteLine("Console / file input ?  c/f");

            var choiceInput = Console.ReadLine();

            Console.Clear();

            if (choiceInput == "c")
            {
                EnterFromConsole();
            }
            else if(choiceInput == "f")
            {
                EnterFromFile();
            }
            else
            {
                break;
            }
        }
    }

    private static void EnterFromConsole()
    {
        Console.Write("Input task:");
        var task = Console.ReadLine();

        if (task == null)
        {
            throw new NullReferenceException();
        }

        var originalTask = new string(task);
        Calculator.Solve(ref task);

        Console.Clear();
        Console.WriteLine($"{originalTask} => {task}");
    }

    private static void EnterFromFile()
    {
        try
        {
            Console.Write("Enter path: ");
            var consoleInput = Console.ReadLine();

            if (consoleInput == null)
            {
                throw new NullReferenceException();
            }

            var file = new FileInfo(consoleInput);
            Calculator.SolveFile(file);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found");
        }
    }
}