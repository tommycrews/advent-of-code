namespace Crews.P2.AdventOfCode.Year2024.Solutions;

public interface ISolution
{
	string Name { get; }
	int Day { get; }
	string ExecutePart1();
	string ExecutePart2();
}
