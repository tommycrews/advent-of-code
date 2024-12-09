namespace Crews.P2.AdventOfCode.Year2024.Utility;

public static class ConsoleUtilities
{
	public static void WriteWithColor(ConsoleColor color, object value)
	{
		ConsoleColor previousColor = Console.ForegroundColor;
		Console.ForegroundColor = color;
		Console.Write(value);
		Console.ForegroundColor = previousColor;
	}

	public static void WriteExecutionTime(TimeSpan timeSpan)
	{
		double milliseconds = timeSpan.TotalMilliseconds;
		string text = $"{milliseconds:F0}ms";

		switch (milliseconds)
		{
			case > 1000:
				WriteWithColor(ConsoleColor.Red, text);
				break;
			case > 100:
				WriteWithColor(ConsoleColor.Yellow, text);
				break;
			default:
				Console.Write(text);
				break;
		}
	}
}
