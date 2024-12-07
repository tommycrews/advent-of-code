namespace Crews.P2.AdventOfCode.Year2024.Solutions;

public abstract class Solution
{
	private string InputPath => $"./InputData/Day{DayNumber.ToString().PadLeft(2, '0')}.txt";
	public abstract string Name { get; }
	public int DayNumber => int.Parse(GetType().Name[^2..]);
	public abstract string ExecutePart1();
	public abstract string ExecutePart2();

	protected string InputText => File.ReadAllText(InputPath);
	protected string[] InputLines => File.ReadAllLines(InputPath);
}
