namespace Crews.P2.AdventOfCode.Year2024.Solutions;

public class Day01 : Solution
{
	private readonly List<int> _leftList = [];
	private readonly List<int> _rightList = [];

	public override string Name => "Historian Hysteria";

	public Day01() => PopulateColumns();

	public override string ExecutePart1()
	{
		_leftList.Sort();
		_rightList.Sort();
		
		return _leftList
			.Select((number, index) => Math.Abs(number - _rightList[index]))
			.Sum()
			.ToString();
	}

	public override string ExecutePart2() => _leftList
		.Select(number => _rightList.Count(instance => instance == number) * number)
		.Sum()
		.ToString();

	private void PopulateColumns()
	{
		IEnumerable<string[]> pairs = InputLines
			.Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries));

		foreach (string[] pair in pairs)
		{
			_leftList.Add(int.Parse(pair[0]));
			_rightList.Add(int.Parse(pair[1]));
		}
	}
}
