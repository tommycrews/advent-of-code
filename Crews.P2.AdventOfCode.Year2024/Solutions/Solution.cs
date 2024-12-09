using System.Diagnostics;
using Crews.P2.AdventOfCode.Year2024.Utility;

namespace Crews.P2.AdventOfCode.Year2024.Solutions;

public abstract class Solution
{
	private string InputPath => $"./InputData/Day{DayNumber.ToString().PadLeft(2, '0')}.txt";
	public abstract string Name { get; }
	public int DayNumber => int.Parse(GetType().Name[^2..]);

	public abstract string ExecutePart1();
	public abstract string ExecutePart2();
	public (string, TimeSpan) ExecutePart1Timed() => ExecuteTimed(ExecutePart1);
	public (string, TimeSpan) ExecutePart2Timed() => ExecuteTimed(ExecutePart2);

	public void ExecuteAndPrint()
	{
		(string, TimeSpan) result1 = ExecutePart1Timed();
		(string, TimeSpan) result2 = ExecutePart2Timed();

		string title = $"Solutions for '{Name}' (day {DayNumber})";

		Console.WriteLine();

		Console.WriteLine(title);
		Console.WriteLine(new string('\u2500', title.Length));

		Console.Write($"Part 1 (");
		ConsoleUtilities.WriteExecutionTime(result1.Item2);
		Console.WriteLine($"): {result1.Item1}");

		Console.Write($"Part 2 (");
		ConsoleUtilities.WriteExecutionTime(result2.Item2);
		Console.WriteLine($"): {result2.Item1}");

		Console.WriteLine();
	}

	protected string InputText => File.ReadAllText(InputPath);
	protected string[] InputLines => File.ReadAllLines(InputPath);

	private static (string, TimeSpan) ExecuteTimed(Func<string> func)
	{
		Stopwatch stopwatch = Stopwatch.StartNew();
		string result = func();
		stopwatch.Stop();
		return (result, stopwatch.Elapsed);
	}
}
